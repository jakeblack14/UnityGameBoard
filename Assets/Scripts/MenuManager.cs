using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TechPlanet.SpaceRace;
using System.Timers;
using System;

public class MenuManager : MonoBehaviour
{

    public List<GameObject> MenuPanels;
    private GameObject BackButton;
    private GameObject HomeButton;
    public Text usernameText;
    public InputField inputField;
    public Sprite selectedImage;
    public Sprite originalImage;

    private Button[] locationButtons;
    private Button[] levelButtons;
    private Button[] turnButtons;
    private Button[] networkCharacterButtons;

    public GameObject levelObject;
    Timer connection = new Timer();

    private Button currentButton;

    private int locationIndex;
    private bool updateLobby = false;
    private bool player1NameInitialized = false;
    private bool player2NameInitialized = false;

    private string networkGameName;
    private string networkGameLocation;

    private int currentPanelIndex;

    public InputField networkGameInputField;

    public Button networkGameButton;
    public GameObject ParentOfButtons;

    void Start()
    {
        inputField.characterLimit = 13;
        networkGameInputField.characterLimit = 15;

        BackButton = GameObject.Find("BackButtonPanel");
        HomeButton = GameObject.Find("HomeButtonPanel");

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
        if (MenuPanels[0].activeSelf) //&& !MenuPanels[8].activeSelf && !MenuPanels[9].activeSelf)
        {
            if (!GameBoardData.IsPlayer2)
            {
                BackButton.SetActive(false);
                HomeButton.SetActive(false);
            }
        }
        else if(MenuPanels[1].activeSelf)
        {
            BackButton.SetActive(true);
            HomeButton.SetActive(false);
        }
        else
        {
            HomeButton.SetActive(true);
            BackButton.SetActive(false);
        }

        if (MenuPanels[2].activeSelf)
        {
            GameSettingsSetUp();
        }

        if (!GameBoardData.IsPlayer2)
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
        if (updateLobby)
        {
            SpawnNetworkGameButtons();
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
        //network game
        else
        {
            if (!GameBoardData.NetworkGameSelected)
            {
                //create network game
                MenuPanels[5].SetActive(false);
                MenuPanels[12].SetActive(true);

                createNetworkGame();
            }
            else
            {
                //join network game
                GameObject goJeff = GameObject.Find("Canvas");
                MultiplayerLauncher jeffGo = goJeff.GetComponent<MultiplayerLauncher>();
                jeffGo.JoinCreatedGame(GameBoardData.CurrentNetworkGameName, GameBoardData.CurrentNetworkGameScene);
            }

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
        //Does the update for the lobby system
        updateLobby = true;
        //SceneManager.LoadScene("MilkyWayScene");
    //    connection.Interval = (1000) * (1); // Ticks every second
    //    connection.Elapsed += new ElapsedEventHandler(OnTimedEvent);
    //    connection.Enabled = true;
    }

    private void OnTimedEvent(object sender, ElapsedEventArgs e)
    {
       // SpawnNetworkGameButtons();
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
        GameBoardData.NetworkGameSelected = false;
    }

    public void joinNetworkGame()
    {
        if (GameBoardData.NetworkGameSelected)
        {
            MenuPanels[5].SetActive(true);
            MenuPanels[6].SetActive(false);
        }
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
        GameBoardData.CurrentNetworkGameScene = networkGameLocation;
        //MenuPanels[7].SetActive(false);
        //MenuPanels[5].SetActive(true);
        GameObject goJeff = GameObject.Find("Canvas");
        MultiplayerLauncher jeffGo = goJeff.GetComponent<MultiplayerLauncher>();
        jeffGo.CreateNewGame(networkGameName, networkGameLocation);
        SpawnNetworkGameButtons();
    }

    public void SpawnNetworkGameButtons()
    {
        
        PhotonNetwork.autoJoinLobby = true;
        //List<List<string>> networkRooms = new List<List<string>>();
        Debug.Log("count how many times this is called");
        //foreach(Transform child in ParentOfButtons.transform)
       // {
      //      GameObject.Destroy(child.gameObject);
       // }

            
            //RoomInfo[] roomsList = PhotonNetwork.GetRoomList();
        Debug.Log(GameBoardData.lobbyRoomInfo.Length);
        Debug.Log(GameBoardData.lobbyRoomInfo[0].Name);
        Debug.Log(GameBoardData.lobbyRoomInfo[0].CustomProperties["index"]);
        Debug.Log(GameBoardData.lobbyRoomInfo[0].CustomProperties["scene"]);
            if (GameBoardData.lobbyRoomInfo.Length != 0)
            {
            Debug.Log("WE did IT");
                foreach (RoomInfo room in GameBoardData.lobbyRoomInfo)
                {
                    if (!GameBoardData.networkGameNames.Contains(room.Name))
                    {

                        Button newButton = Instantiate(networkGameButton, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                        newButton.transform.SetParent(ParentOfButtons.transform, false);

                        Text[] buttonText = newButton.GetComponentsInChildren<Text>();
                        Debug.Log(room.CustomProperties[0]);
                        //name, scene, character index
                        string SceneAndCharacter = (string)room.CustomProperties["joe"];
                        string characterIndex = SceneAndCharacter.Substring(SceneAndCharacter.Length - 1);
                        string sceneName = SceneAndCharacter.TrimEnd(SceneAndCharacter[SceneAndCharacter.Length - 1]);

                        buttonText[0].text = room.Name;
                        buttonText[1].text = sceneName;

                        GameBoardData.CharacterIndexNetwork = Convert.ToInt32(characterIndex);

                        GameBoardData.networkGameNames.Add(room.Name);
                    }
                }
            }
            else
        {
            
        }

    }

    public void OnSelect(BaseEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
