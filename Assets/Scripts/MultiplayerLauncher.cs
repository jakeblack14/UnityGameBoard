using UnityEngine;
using GameCore;
using UnityEngine.SceneManagement;
using System.Timers;
using System.Net;
using System;

namespace TechPlanet.SpaceRace
{
    public class MultiplayerLauncher : Photon.PunBehaviour
    {
        #region Public Variables
        /// <summary>
        /// The PUN loglevel. 
        /// </summary>
        public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;
        PhotonPlayer OtherNetworkPlayer;
        Timer connection = new Timer();
        bool waiting = true;
        bool initializing = true;
        string passedSceneName;
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

            
            // #Critical
            // we don't join the lobby. There is no need to join a lobby to get the list of rooms.
            PhotonNetwork.autoJoinLobby = false;


            // #Critical
            // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
           // PhotonNetwork.automaticallySyncScene = true;
            // #NotImportant
            // Force LogLevel
            PhotonNetwork.logLevel = Loglevel;
            //Timer connection = new Timer();
           // connection.Interval = (1000) * (1); // Ticks every second
           // connection.Elapsed += new ElapsedEventHandler(OnTimedEvent);
           // connection.Enabled = true;
            
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
            
            
            // progressLabel.SetActive(false);
            //  controlPanel.SetActive(true);
            //Random rand = new Random();
            // if (rand.Next(0, 2))
            //button = GameObject.Find("Start");
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


        public void CreateNewGame(String GameName, String Scene)
        {
            passedSceneName = Scene;
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsVisible = true;
            roomOptions.MaxPlayers = 2;
            roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
            roomOptions.CustomRoomProperties.Add("name", GameName);
            roomOptions.CustomRoomProperties.Add("scene", Scene);
            roomOptions.CustomRoomPropertiesForLobby = new string[] { "name" }; //makes name accessible in a room list in the lobby
            roomOptions.CustomRoomPropertiesForLobby = new string[] { "scene" }; // Makes scene name accessible in a room list in the lobby
            PhotonNetwork.CreateRoom(GameName, roomOptions, null);
            RoomInfo[]arrayOfRooms = PhotonNetwork.GetRoomList();
            int i = arrayOfRooms.Length;
            string Room1Name = arrayOfRooms[i].Name;
            
            Debug.Log("which room number in array");
            Debug.Log(i);
            Debug.Log(Room1Name);
            Debug.Log("check to see if room name matches up from here");

        }
        public void JoinCreatedGame(String GameName, String Scene)
        {
            PhotonNetwork.JoinRoom(GameName);
        }
        public void OnJoinedCreatedGame()
        {
            //loading screen
            //waiting = true;
            if (PhotonNetwork.room.PlayerCount == 2)
            {
               // initializing = false;
               // waiting = false;
            }
            else
            {
                bool order = false;
                StartGame(order, passedSceneName);
                
                PhotonNetwork.RaiseEvent(3, passedSceneName, true, null);

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
                PhotonNetwork.JoinRandomRoom();
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
            PhotonNetwork.JoinRandomRoom();


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
            //player1.setPlayer(identity.X);
           // player1 = new GameCore.Player(identity.X);
            //BoardManager.firstPlayerIdentity = identity.X;

            Debug.Log("Identity is x");
            PhotonNetwork.CreateRoom("Room1", roomOptions, null);
                
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
           if (PhotonNetwork.isMasterClient)
            {
                PhotonNetwork.LoadLevel("MilkyWayScene");
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
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene("MainMenu");
        }
        void StartGame(bool order, string sceneName)
        {
            //Timer connection = new Timer();
            connection.Interval = (1000) * (1); // Ticks every second
            connection.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            connection.Enabled = true;
            GameObject goJeff = GameObject.Find("GameBoard");
            BoardManager jeffGo = goJeff.GetComponent<BoardManager>();
            //if (player1.getIdentity() != identity.X)
            if (!order)
            {
                //player1 = new GameCore.NetworkPlayer(identity.O);
                //BoardManager.firstPlayerIdentity = identity.O;
                Debug.Log("Going Second");

                jeffGo.ChangeFirstPlayer(false);
                String ourName = GameBoardData.Name;
                PhotonNetwork.RaiseEvent(2, ourName, true, null);
                // jeffGo.ReceiveNetworkPlayerName(ourName);
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                jeffGo.ChangeFirstPlayer(true);
                String ourName = GameBoardData.Name;
                PhotonNetwork.RaiseEvent(2, ourName, true, null);
                
            }
            // Load Scene here
            int characterIndexNumber = GameBoardData.CharacterIndexLocal;
            PhotonNetwork.RaiseEvent(4, characterIndexNumber, true, null);
            jeffGo.NetworkWaiting();

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
            GameObject go = GameObject.Find("GameBoard");
            BoardManager jeff = go.GetComponent<BoardManager>();
            if (eventCode == 1)
            {
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
                
                string otherPlayer = (string)content;
                //int character = jeff.characterNumber thing here
                // Pass character with otherPlayer name 
                //int character = GameBoardData.CharacterIndexLocal;
                jeff.ReceiveNetworkPlayerName(otherPlayer);
            }
            else if (eventCode == 3)
            {
                bool first = true;
                StartGame(first, (string)content);
            }
            else if (eventCode == 4)
            {
                GameBoardData.CharacterIndexNetwork = (int)content;
            }
                 
        }
        #endregion
    }
    // Code to populate the server list thing needs to be modified.
    /*  private Transform panel;
      private List<GameObject> serverList;
      private GameObject scroll;
      private GameObject selectedObject;
      private Color unselectedColor;

      public void OnEnable()
      {
          if (serverList == null)
          {
              panel = transform.FindChild("Area/Panel");
              scroll = transform.FindChild("Scrollbar").gameObject;
              serverList = new List<GameObject>();
              unselectedColor = new Color(171 / 255.0f, 174 / 255.0f, 182 / 255.0f, 1);
          }
          InvokeRepeating("PopulateServerList", 0, 2);
      }

      public void OnDisable()
      {
          CancelInvoke();
      }

      public void Update()
      {
          if (Input.GetKeyDown(KeyCode.Mouse0))
          {
              GameObject server = EventSystem.current.currentSelectedGameObject;
              if (server != null)
              {
                  if (server.name == "ServerButton")
                  {
                      if (selectedObject != null)
                          selectedObject.transform.FindChild("Image").GetComponent<Image>().color = unselectedColor;

                      selectedObject = server.transform.parent.gameObject;
                      selectedObject.transform.FindChild("Image").GetComponent<Image>().color = Color.white;
                  }
              }
          }
      }

      public void PopulateServerList()
      {
          int i = 0;
          RoomInfo[] hostData = PhotonNetwork.GetRoomList();

          int selected = serverList.IndexOf(selectedObject);

          for (int j = 0; j < serverList.Count; j++)
          {
              Destroy(serverList[j]);
          }
          serverList.Clear();

          if (null != hostData)
          {
              for (i = 0; i < hostData.Length; i++)
              {
                  if (!hostData[i].open)
                      continue;

                  GameObject text = (GameObject)Instantiate(Resources.Load("ServerObject"));
                  serverList.Add(text);
                  text.transform.SetParent(panel, false);
                  text.transform.FindChild("ServerText").GetComponent<Text>().text = hostData[i].name;
                  text.transform.FindChild("PlayerText").GetComponent<Text>().text = hostData[i].playerCount + "/" + hostData[i].maxPlayers;
                  text.transform.FindChild("MapText").GetComponent<Text>().text = hostData[i].customProperties["map"].ToString();
                  text.transform.FindChild("GMText").GetComponent<Text>().text = hostData[i].customProperties["gm"].ToString();
                  text.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, (i * -25), 0);
              }
          }
          if ((i * -25) < -290)
          {
              panel.GetComponent<RectTransform>().sizeDelta = new Vector2(400, (i * 25) + 30);
              scroll.SetActive(true);
          }
          else
          {
              panel.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 320);
              scroll.SetActive(false);
          }
          if (selected >= 0 && selected < serverList.Count)
          {
              selectedObject = serverList[selected];
              selectedObject.transform.FindChild("Image").GetComponent<Image>().color = Color.white;
          }
      }





      // other things that might help
      void OnGUI(){
          GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

          foreach (RoomInfo game in PhotonNetwork.GetRoomList())
          {
              if(GUILayout.Button (game.name + " " + game.playerCount + "/" + game.maxPlayers)) {
                  PhotonNetwork.JoinRoom(game.name);
              }

          }
    */


}