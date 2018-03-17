using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TechPlanet.SpaceRace;
using System.Threading;
using System.Timers;



//TestChanges
namespace GameCore {
    public class BoardManager : MonoBehaviour {

        public static Boolean againstNetwork = false;
        public static Boolean waitForNetwork = false;
        public static Boolean againstAI = false;
        //MultiplayerLauncher multi = new MultiplayerLauncher();
        // private TechPlanet.SpaceRace.MultiplayerLauncher multi = null;
        public static int beginCol = 0;
        public static int beginRow = 0;
        public static int endRow = 0;
        public static int endCol = 0;
        public static Boolean playerGoingFirst = true;

        public GamePieces[,] GamePiecesArray { set; get; }

        static private GamePieces selectedGamePiece;
        private GamePieces removedGamePiece;

        private static GameBoard game;

        private const float TILE_SIZE = 1.0f;
        private const float TILE_OFFSET = 0.5f;

        public float speed;
      
        public static Move networkMove = new Move();
        Player PlayerX = new Player(identity.X);
        Player PlayerO = new Player(identity.O);
        Player currentPlayer =  null;

        private int selectionX = -1;
        private int selectionY = -1;

        public List<GameObject> gamePieces;
        private List<GameObject> activeGamePieces;

        public GameObject canvas;
        public GameObject GameOverPanel;
        public Text winnerText;
        public Text turnText;
        //public Image turnImage;
        //public Sprite[] choicesForTurnImage;
        public GameObject Rocket;
        Animator animator;
        private static bool wasCreated;

        private Material previousMat;
        public Material selectedMat;

        private Move currentMove;


        private void setFirstPlayer()
        {
            //set the current player to whichever player is going first
            if (playerGoingFirst)
            {
                currentPlayer = PlayerX;
                Debug.Log("PlayerX going first");

            }
            else
            {
                currentPlayer = PlayerO;
                Debug.Log("PlayerO going first");
            }
            //update the game core
            game.newGameBoard(currentPlayer.getIdentity());
        }

        private void Start()
        {

            networkMove = null;
            //Set the player parameters based on whether we are playing against an AI or network (or the default - against local)
            if (againstNetwork)
            {
                //Set a new network player as the opponent (O)
                PlayerO = new NetworkPlayer(identity.O);
                networkMove = null;
            }
            else if (againstAI)
            {
                //Set a new AI player as the opponent (O)
                //second parameter is true if the player is not going first, meaning the AI is
                PlayerO = new AIPlayer(identity.O, !playerGoingFirst);
            }


            //Create the gameboard
            game = new GameBoard();

            //Set the initial first player
            setFirstPlayer();

            wasCreated = false;

            SpawnAllGamePieces();          

            currentMove = new Move();
            //SendTheMove()

            animator = Rocket.GetComponent<Animator>();
            animator.speed = 2.25f; // Changes how fast the rocket will fly across the screen

            animator = Rocket.GetComponent<Animator>();
        }

        private void Update()
        {
            UpdateSelection();
            //DrawChessboard();
            if (!game.gameOver())
            {
                if (currentPlayer.isAI())
                {
                    if (!currentPlayer.hasRequestedMove())
                    {
                        currentPlayer.requestMove();
                    }
                    else if (currentPlayer.hasMove())
                    {
                        Move automove = currentPlayer.getMove();
                        SelectGamePiece(automove.Begin.col, automove.Begin.row);
                        MoveGamePiece(  automove.End.col, automove.End.row);

                    }
                }
                else if (againstNetwork && waitForNetwork)
                {
                    // To stop from going into the next iteration of the else if 
                }
                else if(currentPlayer.isNetwork())
                {
                    if (networkMove != null )
                    {
                        if (networkMove.Begin.row != networkMove.End.row)
                        {
                            SelectGamePiece(networkMove.Begin.col, networkMove.Begin.row);
                            MoveGamePiece(networkMove.End.col, networkMove.End.row);   
                        }
                        networkMove = null;
                    }
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    
                    //selectionX and selectionY correspond to what square on the board the mouse is currently on
                    if (selectionX >= 0 && selectionY >= 0)
                    {

                        //if(GamePiecesArray[selectionX, selectionY] != null)
                        //{
                        //    SelectGamePiece(selectionX, selectionY);
                        //}
                        //else
                        //{
                        //    MoveGamePiece(selectionX, selectionY);
                        //}

                        if (selectedGamePiece == null)
                        {
                            SelectGamePiece(selectionX, selectionY);
                            beginCol = selectionY;
                            beginRow = selectionX;
                            //networkMove.Begin.row = selectionX;
                          //  networkMove.Begin.col = selectionY;
                        }
                        else
                        {
                            MoveGamePiece(selectionX, selectionY);
                            endCol = selectionY;
                            endRow = selectionX;
                            if (beginCol != endCol)
                            {
                                // networkMove.End.row = selectionX;
                                //  networkMove.End.col = selectionY;
                                GameObject goJeff = GameObject.Find("GameBoard");
                                MultiplayerLauncher jeff = goJeff.GetComponent<MultiplayerLauncher>();
                                if (againstNetwork)
                                {
                                    jeff.SendTheMove(7 - beginRow, 7 - beginCol, 7 - endRow, 7 - endCol);
                                }
                            }

                        }
                    }
                }

                if (currentPlayer == PlayerX)
                {
                    //turnImage.sprite = choicesForTurnImage[0];
                    turnText.text = "Your turn!";
                    animator.SetBool("Player1Turn", true);
                    animator.SetBool("Player2Turn", false);


                }
                else if (currentPlayer == PlayerO)
                {
                    //turnImage.sprite = choicesForTurnImage[1];
                    turnText.text = "Computer's turn!";
                    animator.SetBool("Player2Turn", true);
                    animator.SetBool("Player1Turn", false);
                }
            }
            else
            {
                //set playerGoingFirst back to default
                playerGoingFirst = true;

                //pull up Game Over Screen
                if (!wasCreated)
                {
                    if (currentPlayer == PlayerO)
                    {
                        winnerText.text = "You win!!";
                    }
                    else if (currentPlayer == PlayerX)
                    {
                        winnerText.text = "Computer Wins!!";
                    }

                    GameObject gameOverPanel = Instantiate(GameOverPanel) as GameObject;
                    gameOverPanel.transform.SetParent(canvas.transform, false);
                    wasCreated = true;

                }
            }
        }

        private void GetNetworkMove()
        {

            

        }

        public void SelectGamePiece(int x, int y)
        {

            //checks to see if there is a game piece on that square
            if (GamePiecesArray[x, y] == null)
                return;

            //if (GamePiecesArray[x, y].isPlayerX != isPlayerXTurn)
            //    return;

            //currentPlayer.turn 

            if (GamePiecesArray[x, y].pieceIdentity != currentPlayer.getIdentity())
                return;

            
            currentMove.Begin.row = y;
            currentMove.Begin.col = x;

            selectedGamePiece = GamePiecesArray[x, y];
            
            //DO NOT DEBUG NEXT LINE
            previousMat = selectedGamePiece.GetComponent<MeshRenderer>().material;
            selectedMat.mainTexture = previousMat.mainTexture;
            selectedGamePiece.GetComponent<MeshRenderer>().material = selectedMat;

        }

        public void MoveGamePiece(int x, int y)
        {
            //if (selectedGamePiece.isMoveValid(x, y))
            //{
            //    GamePiecesArray[selectedGamePiece.CurrentX, selectedGamePiece.CurrentY] = null;

            //    //move the piece to the new location
            //    if (GamePiecesArray[x, y] != null)
            //        return;
            //    selectedGamePiece.transform.position = GetTileCenter(x, y);
            //    GamePiecesArray[x, y] = selectedGamePiece;
            //    isPlayerXTurn = !isPlayerXTurn;
            //}

            currentMove.End.row = y;
            currentMove.End.col = x;

            if(game.movePiece(currentPlayer.getIdentity(), currentMove))
            {
                if(game.pieceLastTaken != null)
                {
                    removedGamePiece = GamePiecesArray[game.pieceLastTaken.col, game.pieceLastTaken.row];

                    Destroy(removedGamePiece.GetComponent<MeshRenderer>());
                    //activeGamePieces.Remove();
                    GamePiecesArray[game.pieceLastTaken.col, game.pieceLastTaken.row] = selectedGamePiece;

                    Destroy(removedGamePiece.GetComponent<GamePieces>());
                    //GamePiecesArray[game.pieceLastTaken.X, game.pieceLastTaken.Y] = null;

                }
                
                GamePiecesArray[currentMove.Begin.col,currentMove.Begin.row] = null;
                //GamePiecesArray[currentMove.End.col, currentMove.End.row] = selectedGamePiece;

                Debug.Log(selectedGamePiece);

                StartCoroutine(AnimatePiece(selectedGamePiece, GetTileCenter(x,y).x, GetTileCenter(x,y).z));

                //selectedGamePiece.transform.localPosition = GetTileCenter(x, y);
                GamePiecesArray[x,y] = selectedGamePiece;

                selectedGamePiece.GetComponent<MeshRenderer>().material = previousMat;
                selectedGamePiece = null;


                if (currentPlayer.getIdentity() == identity.X)
                {
                    currentPlayer = PlayerO;
                }
                else
                {
                    currentPlayer = PlayerX;
                }
            }

            else
            {
                //Unhighlight the piece if we are playing locally
                if (!currentPlayer.isAI() && !currentPlayer.isNetwork())
                {
                    if (selectedGamePiece.GetComponent<MeshRenderer>())
                    {
                        selectedGamePiece.GetComponent<MeshRenderer>().material = previousMat;
                    }
                    selectedGamePiece = null;
                }
            }
        }

        IEnumerator AnimatePiece(GamePieces piece, float x, float z)
        {
            float waitTime = 0.04f;
            Vector3 targetPosition = new Vector3(x, 0, z);

            while(true)
            {
                yield return new WaitForSeconds(waitTime);

                float step = speed * waitTime;
                piece.transform.position = Vector3.MoveTowards(piece.transform.position, targetPosition, step);

                if(piece.transform.position == targetPosition)
                {
                    break;
                }
            }
        }

        public void ChangeFirstPlayer(Boolean whoIsFirst)
        {
            if (whoIsFirst)
            {
                playerGoingFirst = true;
                Debug.Log("Going First!");
            }
            else
            {
                playerGoingFirst = false;
                Debug.Log("Not Going First");
            }
            setFirstPlayer();
        }

        public void ReceiveNetworkMove(int beginRow, int beginCol, int endRow, int endCol)
        {
            networkMove = new Move();
            networkMove.Begin.row = beginRow;
            networkMove.Begin.col = beginCol;
            networkMove.End.row = endRow;
            networkMove.End.col = endCol;
        }

        private void UpdateSelection()
        {
            if (!Camera.main)
                return;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50.0f, LayerMask.GetMask("GameBoardPlane")))
            {
                selectionX = (int)hit.point.x;
                selectionY = (int)hit.point.z;
            }
            else
            {
                selectionX = -1;
                selectionY = -1;
            }
        }

        private void SpawnGamePieces(int index, int x, int y)
        {
            GameObject go = Instantiate(gamePieces[index], GetTileCenter(x, y), Quaternion.identity) as GameObject;
            go.transform.SetParent(transform);
            GamePiecesArray[x, y] = go.GetComponent<GamePieces>();
            GamePiecesArray[x, y].SetPosition(x, y);
            activeGamePieces.Add(go);
        }
        

        private Vector3 GetTileCenter(int x, int y)
        {
            Vector3 origin = Vector3.zero;
            origin.x += (TILE_SIZE * x) + TILE_OFFSET;
            origin.z += (TILE_SIZE * y) + TILE_OFFSET;
            return origin;
        }

        private void SpawnAllGamePieces()
        {
            activeGamePieces = new List<GameObject>();
            GamePiecesArray = new GamePieces[8, 8];

            //if(GameBoardData.IsAlien)
            //{
            //    for (int i = 0; i < 2; i++)
            //    {
            //        for (int j = 0; j < 8; j++)
            //        {
            //            SpawnGamePieces(1, j, i);
            //        }
            //    }

            //    for (int i = 6; i < 8; i++)
            //    {
            //        for (int j = 0; j < 8; j++)
            //        {
            //            SpawnGamePieces(0, j, i);
            //        }
            //    }
            //}
            //else
            //{
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        SpawnGamePieces(0, j, i);
                    }
                }

                for (int i = 6; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        SpawnGamePieces(1, j, i);
                    }
                }
            //}
        }

        
        public static Board getBoard()
        {
            return game.getBoard();
        }
        public void NetworkWaiting()
        {
            waitForNetwork = false;
        }
    }
}
