using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{

    public List<GameObject> MenuPanels;
    private GameObject BackButton;
    public Text usernameText;
    public InputField inputField;
    public Sprite selectedImage;
    public Sprite originalImage;

    private Button[] locationButtons;
    private Button[] levelButtons;
    private Button[] turnButtons;
    private Button[] networkCharacterButtons;

    public GameObject levelObject;

    private Button currentButton;

    private int locationIndex;

    private bool player1NameInitialized = false;
    private bool player2NameInitialized = false;

    private string networkGameName;
    private string networkNumPlayers;
    private string networkGameLocation;

    private int currentPanelIndex;

    public InputField networkGameInputField;

    public Button networkGameButton;
    public GameObject ParentOfButtons;

    void Start()
    {
        BackButton = GameObject.Find("BackButtonPanel");

        if(!GameBoardData.GameInitialized)
        {
            GameBoardData.GameInitialized = true;
        }
        else
        {
            MenuPanels[0].SetActive(false);
            MenuPanels[1].SetActive(true);
        }
    }

    void Update()
    {
        if (MenuPanels[0].activeSelf && !MenuPanels[8].activeSelf && !MenuPanels[9].activeSelf)
        {
            if (!GameBoardData.IsPlayer2)
            {
                BackButton.SetActive(false);
            }
        }
        else if(MenuPanels[2].activeSelf)
        {
            GameSettingsSetUp();
        }
        else
        {
            BackButton.SetActive(true);
        }

        if(!GameBoardData.IsPlayer2)
        {
            usernameText.enabled = false;
            if(player1NameInitialized)
            {
                inputField.placeholder.GetComponent<Text>().text = GameBoardData.Name;
            }
            else
            {
                inputField.placeholder.GetComponent<Text>().text = "Player 1";
            }
        }
        else
        {
            usernameText.enabled = true;
            if (player2NameInitialized)
            {
                inputField.placeholder.GetComponent<Text>().text = GameBoardData.Player2Name;
            }
            else
            {
                inputField.placeholder.GetComponent<Text>().text = "Player 2";
            }
        }

        if(MenuPanels[8].activeSelf || MenuPanels[9].activeSelf || MenuPanels[10].activeSelf || MenuPanels[11].activeSelf)
        {
            for (int i = 0; i < 7; i++)
            {
                if(MenuPanels[i].activeSelf)
                {
                    currentPanelIndex = i;
                }
            }

            for (int i=0; i<7; i++)
            {
                MenuPanels[i].SetActive(false);
            }
        }
    }

    public void restoreCurrentPanel()
    {
        MenuPanels[currentPanelIndex].SetActive(true);
    }

    public void usernameButtonClick()
    {
        //player 1 just entered their name
        if (!GameBoardData.IsPlayer2)
        {
            if (inputField.text == "")
            {
                GameBoardData.Name = inputField.placeholder.GetComponent<Text>().text;
            }
            else
            {
                GameBoardData.Name = inputField.text;
            }

            player1NameInitialized = true;
            MenuPanels[1].SetActive(true);
            MenuPanels[0].SetActive(false);
        }
        //player 2 just entered their name
        else
        {
            if (inputField.text == "")
            {
                GameBoardData.Player2Name = inputField.placeholder.GetComponent<Text>().text;
            }
            else
            {
                GameBoardData.Player2Name = inputField.text;
            }

            inputField.text = "";
            player2NameInitialized = true;
            MenuPanels[5].SetActive(true);
            MenuPanels[0].SetActive(false);
        }
    }

    public void GameSettingsSetUp()
    {
        locationButtons = GameObject.Find("LocationButtons").GetComponentsInChildren<Button>();
        turnButtons = GameObject.Find("TurnButtons").GetComponentsInChildren<Button>();

        if (!GameCore.BoardManager.againstAI && !GameCore.BoardManager.againstNetwork)
        {
            levelObject.SetActive(false);
        }
        else
        {
            levelButtons = GameObject.Find("LevelButtons").GetComponentsInChildren<Button>();
        }
    }

    public void locationSelected(int index)
    {
        highlightButton(locationButtons, index);

        locationIndex = index;
    }

    public void levelSelected(int index)
    {
        highlightButton(levelButtons, index);
        GameCore.AIPlayer.setMode(index);
    }

    public void turnSelected(int index)
    {
        highlightButton(turnButtons, index);


        if (index == 0)
        {
            GameCore.BoardManager.playerGoingFirst = true;
        }
        else
        {
            GameCore.BoardManager.playerGoingFirst = false;
        }
    }

    private void highlightButton(Button[] currentButtons, int index)
    {
        currentButton = currentButtons[index];
        currentButton.image.sprite = selectedImage;

        for (int i = 0; i < currentButtons.Length; i++)
        {
            if (i != index)
            {
                currentButtons[i].image.sprite = originalImage;
            }
        }
    }

    public void gamePlayButtonClicked()
    {
        MenuPanels[2].SetActive(false);
        MenuPanels[3].SetActive(true);
    }

    public void characterNextButtonClicked()
    {
        if (GameCore.BoardManager.againstAI)
        {
            if (locationIndex == 0)
            {
                SceneManager.LoadScene("AsteroidScene");
            }
            else if (locationIndex == 1)
            {
                SceneManager.LoadScene("MilkyWayScene");
            }
        }
        else
        {
            GameBoardData.IsPlayer2 = true;
            inputField.text = "";
            MenuPanels[3].SetActive(false);
            MenuPanels[0].SetActive(true);
        }
    }

    public void characterForPlayer2NextButtonClicked()
    {
        //local game
        if (!GameCore.BoardManager.againstNetwork)
        {
            if (locationIndex == 0)
            {
                SceneManager.LoadScene("AsteroidScene");
            }
            else if (locationIndex == 1)
            {
                SceneManager.LoadScene("MilkyWayScene");
            }
        }
        else
        {
            MenuPanels[5].SetActive(false);
            MenuPanels[6].SetActive(true);
        }
    }

    public void singlePlayerButtonClicked()
    {
        GameCore.BoardManager.againstAI = true;
        GameCore.BoardManager.againstNetwork = false;
        GameBoardData.Player2Name = "Computer";
    }

    public void localButtonClicked()
    {
        GameCore.BoardManager.againstAI = false;
        GameCore.BoardManager.againstNetwork = false;
    }

    public void networkButtonClicked()
    {
        GameCore.BoardManager.againstAI = false;
        GameCore.BoardManager.againstNetwork = true;
        GameCore.BoardManager.waitForNetwork = true;
        //SceneManager.LoadScene("MilkyWayScene");
    }

    public void highlightNetworkCharacterButton(int index)
    {
        highlightButton(networkCharacterButtons, index);
    }

    public void networkCharacterChosen(bool isAlien)
    {
        if (isAlien)
        {
            GameBoardData.IsAlien = true;
        }
        else
        {
            GameBoardData.IsAlien = false;
        }
    }

    public void startNetworkGame()
    {
        MenuPanels[7].SetActive(true);
        MenuPanels[6].SetActive(false);
        locationButtons = GameObject.Find("NetworkLocationButtons").GetComponentsInChildren<Button>();
        networkCharacterButtons = GameObject.Find("TeamButtons").GetComponentsInChildren<Button>();
        GameBoardData.IsAlien = true;
    }

    public void createNetworkGame()
    {
        networkGameName = networkGameInputField.text;

        if (locationIndex == 0)
        {
            networkGameLocation = "Asteroid Belt";
        }
        else if (locationIndex == 1)
        {
            networkGameLocation = "Milky Way";
        }

        networkNumPlayers = "1/2";

        MenuPanels[7].SetActive(false);
        MenuPanels[5].SetActive(true);

        SpawnNetworkGameButtons(networkGameName, networkNumPlayers, networkGameLocation);
    }

    private void SpawnNetworkGameButtons(string name, string count, string location)
    {
        Button newButton = Instantiate(networkGameButton, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        newButton.transform.SetParent(ParentOfButtons.transform, false);

        Text[] buttonText = newButton.GetComponentsInChildren<Text>();

        buttonText[0].text = name;
        buttonText[1].text = count;
        buttonText[2].text = location;
    }

    public void OnSelect(BaseEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
