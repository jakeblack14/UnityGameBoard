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
    public Button JoinNetworkGameButton;

    private List<Button> networkButtons;

    void Start()
    {
        GameCore.BoardManager.againstAI = false;
        GameCore.BoardManager.againstNetwork = false;

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

        if (MenuPanels[0].activeSelf)
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

            GameBoardData.LocalGamePlayer1IsAlien = false;
            GameBoardData.SinglePlayerIsAlien = false;
            GameBoardData.IsPlayer2 = false;
            GameBoardData.NetworkGameLocalPlayerIsAstronaut = false;
            GameBoardData.CharacterIndexLocal = 0;
            GameBoardData.CharacterIndexNetwork = 0;
            GameCore.BoardManager.playerGoingFirst = true;
            GameBoardData.GameOver = false;
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

        if(MenuPanels[12].activeSelf || MenuPanels[9].activeSelf || MenuPanels[10].activeSelf || MenuPanels[11].activeSelf)
        {
            for (int i = 0; i < 9; i++)
            {
                if(MenuPanels[i].activeSelf)
                {
                    currentPanelIndex = i;
                }
            }

            for (int i=0; i<9; i++)
            {
                MenuPanels[i].SetActive(false);
            }
        }
        if (updateLobby)
        {
            SpawnNetworkGameButtons();
        }

        if(GameBoardData.NetworkGameSelected)
        {
            JoinNetworkGameButton.interactable = true;
        }

        destroyButtonAfterRoomDisconnected();
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
                //SceneManager.LoadScene("AsteroidScene");
                StartCoroutine(GameObject.FindObjectOfType<FadeEffect>().EffectsAndLoadScene("AsteroidScene"));
            }
            else if (locationIndex == 1)
            {
                //SceneManager.LoadScene("MilkyWayScene");
                StartCoroutine(GameObject.FindObjectOfType<FadeEffect>().EffectsAndLoadScene("MilkyWayScene"));
            }
        }
        else
        {
            if(GameBoardData.SinglePlayerIsAlien)
            {
                GameBoardData.LocalGamePlayer1IsAlien = true;
            }
            else
            {
                GameBoardData.LocalGamePlayer1IsAlien = false;
            }

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
                StartCoroutine(GameObject.FindObjectOfType<FadeEffect>().EffectsAndLoadScene("AsteroidScene"));
            }
            else if (locationIndex == 1)
            {
                StartCoroutine(GameObject.FindObjectOfType<FadeEffect>().EffectsAndLoadScene("MilkyWayScene"));
            }
        }
        //network game
        else
        {
            if (!GameBoardData.NetworkGameSelected)
            {
                //create network game
                MenuPanels[5].SetActive(false);
                MenuPanels[8].SetActive(true);

                createNetworkGame();
            }
            else
            {
                //join network game
                GameObject goJeff = GameObject.Find("Canvas");
                MultiplayerLauncher jeffGo = goJeff.GetComponent<MultiplayerLauncher>();
                jeffGo.JoinCreatedGame(GameBoardData.CurrentNetworkGameName, GameBoardData.CurrentNetworkGameScene);

                destroyNetworkLobby();
            }
        }
    }

    public void destroyButtonAfterRoomDisconnected()
    {
        bool found = false;
        foreach (Transform b in ParentOfButtons.transform)
        {
            Text[] text = b.GetComponentsInChildren<Text>();

            foreach (RoomInfo room in GameBoardData.lobbyRoomInfo)
            {
                if (text[0].text.ToString() == room.Name)
                {
                    found = true;
                }

            }
            if (!found)
            {
                Destroy(b.gameObject);
            }
            found = false;
        }
    }

    public void destroyNetworkLobby()
    {
        foreach (Transform b in ParentOfButtons.transform)
        {
            Text[] text = b.GetComponentsInChildren<Text>();

            if (text[0].text.ToString() == GameBoardData.CurrentNetworkGameName)
            {
                Destroy(b.gameObject);
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
        JoinNetworkGameButton.interactable = false;
    }

    public void highlightNetworkCharacterButton(int index)
    {
        highlightButton(networkCharacterButtons, index);
    }

    public void networkCharacterChosen(bool isAstro)
    {
        if (isAstro)
        {
            GameBoardData.NetworkGameLocalPlayerIsAstronaut = true;
        }
        else
        {
            GameBoardData.NetworkGameLocalPlayerIsAstronaut = false;
        }
    }

    public void startNetworkGame()
    {
        MenuPanels[7].SetActive(true);
        MenuPanels[6].SetActive(false);
        locationButtons = GameObject.Find("NetworkLocationButtons").GetComponentsInChildren<Button>();
        networkCharacterButtons = GameObject.Find("TeamButtons").GetComponentsInChildren<Button>();
        networkGameInputField.placeholder.GetComponent<Text>().text = GameBoardData.Name + "'s Game";
        GameBoardData.NetworkGameLocalPlayerIsAstronaut = true;
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
        if(networkGameInputField.text == "")
        {
            networkGameName = networkGameInputField.placeholder.GetComponent<Text>().text;
        }
        else
        {
            networkGameName = networkGameInputField.text;
        }

        if (locationIndex == 0)
        {
            networkGameLocation = "Asteroid Belt";
        }
        else if (locationIndex == 1)
        {
            networkGameLocation = "Milky Way";
        }
        GameBoardData.CurrentNetworkGameScene = networkGameLocation;
        GameObject goJeff = GameObject.Find("Canvas");
        MultiplayerLauncher jeffGo = goJeff.GetComponent<MultiplayerLauncher>();
        jeffGo.CreateNewGame(networkGameName, networkGameLocation);
        SpawnNetworkGameButtons();
    }

    public void SpawnNetworkGameButtons()
    {
        
        PhotonNetwork.autoJoinLobby = true;
       
            if (GameBoardData.lobbyRoomInfo.Length != 0)
            {
            
                foreach (RoomInfo room in GameBoardData.lobbyRoomInfo)
                { 
                   
                
                    if (!GameBoardData.networkGameNames.Contains(room.Name))
                    {

                        Button newButton = Instantiate(networkGameButton, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                        newButton.transform.SetParent(ParentOfButtons.transform, false);

                        Text[] buttonText = newButton.GetComponentsInChildren<Text>();
                        
                        //name, scene, character index
                        string SceneAndCharacter = (string)room.CustomProperties["joe"];
                        string characterIndex = SceneAndCharacter.Substring(1, 1);
                        string sceneIndex = SceneAndCharacter.Substring(0, 1);
                        string player2Name = SceneAndCharacter.Substring(2);
                        string sceneName;
                        if (sceneIndex == "M")
                        {
                             sceneName = "Milky Way";
                        }
                        else
                        {
                            sceneName = "Asteroid Belt";
                        }
                         
                        buttonText[0].text = room.Name;
                        buttonText[1].text = sceneName;
                        buttonText[2].text = characterIndex;
                        buttonText[2].enabled = false;
                        buttonText[3].text = player2Name;
                        buttonText[3].enabled = false;

                        //GameBoardData.CharacterIndexNetwork = Convert.ToInt32(characterIndex);

                        GameBoardData.networkGameNames.Add(room.Name);
                    }
                }
            }
            else
            {
            
            }
        }
}
