﻿using UnityEngine;
using GameCore;
using UnityEngine.SceneManagement;
using System.Timers;
using System.Net;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

namespace TechPlanet.SpaceRace
{
   
    public class MultiplayerLauncher : Photon.PunBehaviour
    {
        #region Public Variables
        /// <summary>
        /// The PUN loglevel. 
        /// </summary>
        public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;

        //PhotonPlayer OtherNetworkPlayer;
        Timer connection = new Timer();
        bool waiting = true;
        static int passedCharacter = 0;
        //bool initializing = true;
        string passedSceneName;
        public static string delete;
        RoomInfo[] roomsList =  new RoomInfo[10];
        string random;
        private static bool created = false;
        /// <summary>
        /// The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created.
        /// </summary>   
        [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
        //  public byte MaxPlayersPerRoom = 2;

        // [Tooltip("The Ui Panel to let the user enter name, connect and play")]
        // public GameObject controlPanel;
        // [Tooltip("The UI Label to inform the user that the connection is in progress")]
        //  public GameObject progressLabel;
        //GameObject button;
        public GameCore.NetworkPlayer player1 = null;
        //FadeEffect jeffGoesFading;
        //public GameObject g;
        #endregion


        #region Private Variables


        /// <summary>
        /// This client's version number. Users are separated from each other by gameversion (which allows you to make breaking changes).
        /// </summary>
        string _gameVersion = "1";


        #endregion


        #region MonoBehaviour CallBacks


        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        /// </summary>
        void Awake()
        {
            //if (!created)
            //{
            //    DontDestroyOnLoad(g);
            //    created = true;
            //    Debug.Log("Awake: " + this.gameObject);
            //}
           // jeffGoesFading = new FadeEffect();
            //DontDestroyOnLoad.((this)gameObject);
            // #Critical
            // we don't join the lobby. There is no need to join a lobby to get the list of rooms.
            PhotonNetwork.autoJoinLobby = true;


            // #Critical
            // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
           // PhotonNetwork.automaticallySyncScene = true;
            // #NotImportant
            // Force LogLevel
            PhotonNetwork.logLevel = Loglevel;
           
         
            PhotonNetwork.OnEventCall += OnEvent;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
          if(  CheckForInternetConnection())
            {
                //You have internet do nothing
            }
            else
            {
                connection.Enabled = false;
                LeaveGame();
                Debug.Log("Game should of left");
           
            }
        }


        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase.
        /// </summary>
        void Start()
        {
            
            
            
        }


        private void Update()
       {
         
        }
        #endregion


        #region Public Methods
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (client.OpenRead("http://clients3.google.com/generate_204"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
                
            }
        
        }
        public override void OnReceivedRoomListUpdate()
        {
            GameBoardData.lobbyRoomInfo = PhotonNetwork.GetRoomList();

        }
        

       
        public void CreateNewGame(String GameName, String Scene)
        {
            passedSceneName = Scene;
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsVisible = true;
            roomOptions.MaxPlayers = 2;
            roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
            string oneLetterScene;
            if (Scene.StartsWith("M"))
            {
                oneLetterScene = "M";
            }
            else
            {
                oneLetterScene = "A";
            }
            string testing = GameBoardData.CharacterIndexLocal.ToString();
            string cat = oneLetterScene + testing + GameBoardData.Name;
            roomOptions.CustomRoomProperties.Add("index", GameBoardData.CharacterIndexLocal.ToString());
            roomOptions.CustomRoomProperties.Add("scene", Scene);
            roomOptions.CustomRoomProperties.Add("joe", cat);
            Debug.Log(roomOptions.CustomRoomProperties["index"]);
            
            //roomOptions.CustomRoomPropertiesForLobby = new string[] { "index" }; //makes name accessible in a room list in the lobby
            //roomOptions.CustomRoomPropertiesForLobby = new string[] { "scene" }; // Makes scene name accessible in a room list in the lobby
            roomOptions.CustomRoomPropertiesForLobby = new string[] { "joe" };
            Debug.Log("testing 123");
            Debug.Log(roomOptions.CustomRoomProperties["index"]);
            
            PhotonNetwork.CreateRoom(GameName, roomOptions, TypedLobby.Default);
            Debug.Log(GameName);
            Debug.Log("GameCreated");
        }
        public void JoinCreatedGame(String GameName, String Scene)
        {
            passedSceneName = Scene;
            PhotonNetwork.JoinRoom(GameName);

        }
        
        public void OnJoinedCreatedGame()
        {
            delete = PhotonNetwork.room.Name;
            Debug.Log("setting delete");
            Debug.Log(delete);
            Debug.Log("OnJoinedCreatedGame is called");
            if (PhotonNetwork.room.PlayerCount == 2)
            {
                bool order = false;
                StartGame(order, passedSceneName);
                PhotonNetwork.RaiseEvent(3, passedSceneName, true, null);
                PhotonNetwork.RaiseEvent(4, GameBoardData.CharacterIndexLocal, true, null);
            }
            else
            {
              //  SceneManager.LoadScene("AsteroidScene");
               // bool order = false;
               // StartGame(order, passedSceneName);
               // Debug.Log("You Joined CreatedGAME");
                //PhotonNetwork.RaiseEvent(3, passedSceneName, true, null);

            }
            //show the loading popup thing here

        }
        /// <summary>
        /// Start the connection process. 
        /// - If already connected, we attempt joining a random room
        /// - if not yet connected, Connect this application instance to Photon Cloud Network
        /// </summary>
        public void Connect()
        {
            
          //  progressLabel.SetActive(true);
           // controlPanel.SetActive(false);
            // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
            if (PhotonNetwork.connected)
            {
                // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnPhotonRandomJoinFailed() and we'll create one.
               // PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                // #Critical, we must first and foremost connect to Photon Online Server.
                PhotonNetwork.ConnectUsingSettings(_gameVersion);
            }
            //button.SetActive(false);
        }


        #endregion

        #region Photon.PunBehaviour CallBacks

       
        public override void OnConnectedToMaster()
        {
            Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN");
            // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnPhotonRandomJoinFailed()  
           // PhotonNetwork.JoinRandomRoom();


        }

        
        public override void OnDisconnectedFromPhoton()
        {
           // progressLabel.SetActive(false);
           // controlPanel.SetActive(true);
            Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");
            LeaveGame();
        }

        public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            Debug.Log("DemoAnimator/Launcher:OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
            // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsVisible = true;
            roomOptions.MaxPlayers = 2;
            Debug.Log("THis shoulD NOT BE CALLED");
            //player1.setPlayer(identity.X);
           // player1 = new GameCore.Player(identity.X);
            //BoardManager.firstPlayerIdentity = identity.X;

            Debug.Log("Identity is x");
          //  PhotonNetwork.CreateRoom("Room1", roomOptions, null);
                
          //  PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2 }, null);
        }
        public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
        {
            Debug.Log("Player Disconnected"+ otherPlayer.NickName);
            // Do something where it basically calls the exit game function
            //base.OnPhotonPlayerDisconnected(otherPlayer);
            LeaveGame();
        }
        public override void OnJoinedRoom()
        {
            Debug.Log("DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
            // Idk what goes in this yet but this seems to be the way to load a scene through photon network PhotonNetwork.InstantiateSceneObject()
            OnJoinedCreatedGame();
           if (PhotonNetwork.isMasterClient)
            {
                //PhotonNetwork.LoadLevel("MilkyWayScene");
            }
           if (PhotonNetwork.room.PlayerCount == 2)
            {
              //  StartGame();
            }
            else
            {
                //StartGame();
            }
            
        }

        public void LeaveGame()
        {
            
            


                //string delete = PhotonNetwork.room.Name;
                int x = 0;
                int indexItIsAt = 0;
                bool testingIt = false;

                foreach (string nameToDelete in GameBoardData.networkGameNames)
                {
                Debug.Log("Gets in the foreach");
                Debug.Log(nameToDelete);
                Debug.Log(delete);
                    if (nameToDelete == delete)
                    {
                    Debug.Log("Finds the thing to delete");
                        testingIt = true;
                        indexItIsAt = x;
                        //GameBoardData.networkGameNames.RemoveAt(x);
                    }
                    x++;
                }
            Debug.Log(testingIt);
                if (testingIt)
                {
                Debug.Log(GameBoardData.networkGameNames.IndexOf(delete));
                Debug.Log(indexItIsAt);
                    GameBoardData.networkGameNames.RemoveAt(indexItIsAt);
                Debug.Log(GameBoardData.networkGameNames.IndexOf(delete));
                }
            testingIt = false;
            
            PhotonNetwork.Disconnect();
            GameBoardData.NetworkGameSelected = false;
            SceneManager.LoadScene("MainMenu");
        }
        public bool LoadOurGame(string name)
        {
            //GameObject go = GameObject.Find("Canvas");
            //FadeEffect jeff = go.GetComponent<FadeEffect>();
            //if (jeff == null)
            //{
            //    SceneManager.LoadScene(name);
            //}
            //else
            //{
            //    jeffGoesFading.EffectsAndLoadScene(name);
            //}
           // StartCoroutine(jeff.EffectsAndLoadScene(name));
            //StartCoroutine(GameObject.FindObjectOfType<FadeEffect>().EffectsAndLoadScene(name));
            SceneManager.LoadScene(name);
            return true;
        }
        void StartGame(bool order, string sceneName)
        {
            waiting = false;
            string ourScene;
            //Timer connection = new Timer();
            connection.Interval = (1000) * (1); // Ticks every second
            connection.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            connection.Enabled = true;
            if (GameBoardData.CurrentNetworkGameScene == "Asteroid Belt")
            {
                ourScene = "AsteroidScene";
            }
            else
            {
                ourScene = "MilkyWayScene";
            }
           if (LoadOurGame(ourScene))
            {

            }
            
            //if (player1.getIdentity() != identity.X)
            if (!order)
            {
                
                //player1 = new GameCore.NetworkPlayer(identity.O);
                //BoardManager.firstPlayerIdentity = identity.O;
                Debug.Log("Going Second");

               // jeffGo.ChangeFirstPlayer(false);
                String ourName = GameBoardData.Name;
                BoardManager.playerGoingFirst = false;
                PhotonNetwork.RaiseEvent(2, ourName, true, null);
                // jeffGo.ReceiveNetworkPlayerName(ourName);
               // SceneManager.LoadScene(sceneName);
            }
            else
            {
                BoardManager.playerGoingFirst = true;
                //jeffGo.ChangeFirstPlayer(true);
                String ourName = GameBoardData.Name;
               // PhotonNetwork.RaiseEvent(2, ourName, true, null);
                
            }
            // Load Scene here
           // GameObject goJeff = GameObject.Find("GameBoard");
            //BoardManager jeffGo = goJeff.GetComponent<BoardManager>();
            if (!order)
            {
                GameBoardData.goingFirst = false;
                // jeffGo.ChangeFirstPlayer(false);
                BoardManager.waitForNetwork = false;
            }
            else
            {
                GameBoardData.goingFirst = true;
                //jeffGo.ChangeFirstPlayer(true);
                BoardManager.waitForNetwork = true;
            }
            int characterIndexNumber = GameBoardData.CharacterIndexLocal;

           // PhotonNetwork.RaiseEvent(4, characterIndexNumber, true, null);
            BoardManager.waitForNetwork = false;
            //jeffGo.NetworkWaiting();

        }

        public void SendTheMove(int temp1, int temp2, int temp3, int temp4)
        {
            Move sentMove = new Move();
            sentMove.Begin.col = temp1;
            sentMove.Begin.row = temp2;
            sentMove.End.col = temp3;
            sentMove.End.row = temp4;
            Debug.Log("Move is being sent");
            int[] moveArray = new int[4];
            moveArray[0] = sentMove.Begin.row;
            moveArray[1] = sentMove.Begin.col;
            moveArray[2] = sentMove.End.row;
            moveArray[3] = sentMove.End.col;
            PhotonNetwork.RaiseEvent(1, moveArray, true, null);
        }
       
        void OnEvent(byte eventCode, object content, int senderId)
        {
           
            if (eventCode == 1)
            {
                GameObject go = GameObject.Find("GameBoard");
                BoardManager jeff = go.GetComponent<BoardManager>();
                Debug.Log("it works now you hoe");
                PhotonPlayer sender = PhotonPlayer.Find(senderId); // This shows who sent the message
                int[] receivedArray = new int[4];
                receivedArray = (int[])content;
                //GameCore.Move move = (GameCore.Move)content;
                //  MakeMove(tempPlayer.getMove);
              //  GameObject go = GameObject.Find("GameBoard");
               // BoardManager jeff = go.GetComponent<BoardManager>();
                Debug.Log("ReceivedArray");
                Debug.Log(receivedArray[0]);
                Debug.Log(receivedArray[1]);
                Debug.Log(receivedArray[2]);
                Debug.Log(receivedArray[3]);
                jeff.ReceiveNetworkMove(receivedArray[0], receivedArray[1], receivedArray[2], receivedArray[3]);
            }
            else if (eventCode == 2)
            {
                //GameObject go = GameObject.Find("GameBoard");
               // BoardManager jeff = go.GetComponent<BoardManager>();
               string otherPlayer = (string)content;
                 GameBoardData.Player2Name = (string)content;
            
                //GameObject goJeffGo = GameObject.Find("GameUIManager");
                //GameUIManager jeffGoes = goJeffGo.GetComponent<GameUIManager>();
                //jeffGoes.SetPlayer2(otherPlayer);
                //int character = jeff.characterNumber thing here
                // Pass character with otherPlayer name 
                //int character = GameBoardData.CharacterIndexLocal;
                // jeff.ReceiveNetworkPlayerName(otherPlayer);
            }
            else if (eventCode == 3)
            {
                string fixIT;
                bool first = true;
                if ((string)content == "Asteroid Belt")
                {
                    fixIT = "AsteroidScene";
                }
                else
                {
                    fixIT = "MilkyWayScene";
                }
                StartGame(first, fixIT);
            }
            else if (eventCode == 4)
            {
                GameBoardData.CharacterIndexNetwork = (int)content;
            }
            else if (eventCode == 5)
            {
                GameObject goJeff = GameObject.Find("Canvas");
                MenuManager jeffGo = goJeff.GetComponent<MenuManager>();
               jeffGo.SpawnNetworkGameButtons();
                Debug.Log("sends the new game to add");
            }
                 
        }
        #endregion
    }

    //public class FadeEffect : MonoBehaviour
    //{
    //    private GameObject fadeStuff;
    //    private Image[] characters;
    //    private Image mainImage;
    //    private Text text;

    //    Animator animator;

    //    private void Awake()
    //    {
    //        fadeStuff = GameObject.Find("FadeEffect");
    //        mainImage = fadeStuff.GetComponentInChildren<Image>();
    //        text = fadeStuff.GetComponentInChildren<Text>();
    //        characters = mainImage.GetComponentsInChildren<Image>();
    //    }

    //    private void OnEnable()
    //    {
    //        for (int i = 0; i < 6; i++)
    //        {
    //            characters[i].enabled = false;
    //        }

    //        text.enabled = false;
    //        mainImage.enabled = false;

    //    }

    //    public IEnumerator FadeIn()
    //    {
    //        mainImage.enabled = true;
    //        text.enabled = true;

    //        for (int i = 0; i < 6; i++)
    //        {
    //            AnimateCharacters(characters[i]);
    //            yield return new WaitForSecondsRealtime(0.750f);
    //        }
    //    }

    //    private void AnimateCharacters(Image character)
    //    {
    //        animator = character.GetComponent<Animator>();
    //        animator.SetBool("characterEnabled", false);

    //        character.enabled = true;

    //        animator.SetBool("characterEnabled", true);
    //    }

    //    public IEnumerator EffectsAndLoadScene(string sceneToLoad)
    //    {
    //        yield return FadeIn();
    //        SceneManager.LoadScene(sceneToLoad);
    //    }
    //}


}