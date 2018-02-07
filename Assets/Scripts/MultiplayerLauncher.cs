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
        /// <summary>
        /// The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created.
        /// </summary>   
        [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
        //  public byte MaxPlayersPerRoom = 2;

        // [Tooltip("The Ui Panel to let the user enter name, connect and play")]
        // public GameObject controlPanel;
        // [Tooltip("The UI Label to inform the user that the connection is in progress")]
        //  public GameObject progressLabel;

        
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
            GameCore.NetworkPlayer player1 = new GameCore.NetworkPlayer();

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
            PhotonNetwork.CreateRoom(null, roomOptions, null);
                
          //  PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2 }, null);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
           // Idk what goes in this yet but this seems to be the way to load a scene through photon network PhotonNetwork.InstantiateSceneObject()
           if (PhotonNetwork.isMasterClient)
            {
                PhotonNetwork.LoadLevel("MultiPlayer Scene");
            }
        }

        public void SendTheMove(GameCore.NetworkPlayer player1)
        {
            Move move = new Move();
            move = player1.getMove();
            PhotonNetwork.RaiseEvent(0, move, true, null);
        }
        public void MakeMove(GameCore.NetworkPlayer player1)
        {

        }
        void OnEvent(byte eventCode, object content, int senderId)
        {
            Debug.Log("it works now you hoe");
            PhotonPlayer sender = PhotonPlayer.Find(senderId); // This shows who sent the message
            Move =
        }
        #endregion
    }
}