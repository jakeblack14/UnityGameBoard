using UnityEngine;
using GameCore;

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
            PhotonNetwork.automaticallySyncScene = true;
            // #NotImportant
            // Force LogLevel
            PhotonNetwork.logLevel = Loglevel;
            
            
            PhotonNetwork.OnEventCall += OnEvent;
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
        


        #endregion


        #region Public Methods


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
                StartGame();
            }
            else
            {
                StartGame();
            }
            
        }

        public void LeaveGame()
        {
            PhotonNetwork.Disconnect();
        }
        void StartGame()
        {

            GameObject goJeff = GameObject.Find("GameBoard");
            BoardManager jeffGo = goJeff.GetComponent<BoardManager>();
            //if (player1.getIdentity() != identity.X)
            if (PhotonNetwork.room.PlayerCount == 2)
            {
                //player1 = new GameCore.NetworkPlayer(identity.O);
                //BoardManager.firstPlayerIdentity = identity.O;
                Debug.Log("Going Second");

                jeffGo.ChangeFirstPlayer(false);
               // if (OtherNetworkPlayer.NickName == null)
               // {
               //
               // }
            }
            else
            {
                jeffGo.ChangeFirstPlayer(true);
            }
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
            Debug.Log("it works now you hoe");
            PhotonPlayer sender = PhotonPlayer.Find(senderId); // This shows who sent the message
            int[] receivedArray = new int[4];
            receivedArray = (int[])content;
            //GameCore.Move move = (GameCore.Move)content;
            //  MakeMove(tempPlayer.getMove);
            GameObject go = GameObject.Find("GameBoard");
            BoardManager jeff = go.GetComponent<BoardManager>();
            Debug.Log("ReceivedArray");
            Debug.Log(receivedArray[0]);
            Debug.Log(receivedArray[1]);
            Debug.Log(receivedArray[2]);
            Debug.Log(receivedArray[3]);
            jeff.ReceiveNetworkMove(receivedArray[0], receivedArray[1], receivedArray[2], receivedArray[3]);
            //jeff.SelectGamePiece(receivedArray[0], receivedArray[1]);
           // jeff.MoveGamePiece(receivedArray[2], receivedArray[3]);
        }
        #endregion
    }
}