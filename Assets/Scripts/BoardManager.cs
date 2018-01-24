using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TestChanges
namespace GameCore {
    public class BoardManager : MonoBehaviour {

        public GamePieces[,] GamePiecesArray { set; get; }

        private GamePieces selectedGamePiece;
        private GamePieces removedGamePiece;

        private static GameBoard game;

        private const float TILE_SIZE = 1.0f;
        private const float TILE_OFFSET = 0.5f;


        Player PlayerX = new Player();
        Player PlayerO = new Player();
        Player currentPlayer = null;

        private int selectionX = -1;
        private int selectionY = -1;

        public List<GameObject> gamePieces;
        private List<GameObject> activeGamePieces;

        private Material previousMat;
        public Material selectedMat;

        private Move currentMove;

        private void Start()
        {
            SpawnAllGamePieces();

            PlayerX.setPlayer(identity.X);
            PlayerO.setPlayer(identity.O);
            currentPlayer = PlayerX;

            game = new GameBoard();
            game.newGameBoard(currentPlayer.getIdentity());

            currentMove = new Move();
        }

        private void Update()
        {
            UpdateSelection();
            //DrawChessboard();
            if (!game.gameOver())
            {
                if (Input.GetMouseButtonDown(0))
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
                        }
                        else
                        {
                            MoveGamePiece(selectionX, selectionY);
                        }
                    }
                }
            }
            else
            {
              
            }
        }

        private void SelectGamePiece(int x, int y)
        {
            //checks to see if there is a game piece on that square
            if (GamePiecesArray[x, y] == null)
                return;

            //if (GamePiecesArray[x, y].isPlayerX != isPlayerXTurn)
            //    return;

            //currentPlayer.turn

            if (GamePiecesArray[x, y].pieceIdentity != currentPlayer.getIdentity())
                return;

            
            currentMove.Begin.X = y;
            currentMove.Begin.Y = x;

            selectedGamePiece = GamePiecesArray[x, y];

            previousMat = selectedGamePiece.GetComponent<MeshRenderer>().material;
            selectedMat.mainTexture = previousMat.mainTexture;
            selectedGamePiece.GetComponent<MeshRenderer>().material = selectedMat;

        }

        private void MoveGamePiece(int x, int y)
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

            currentMove.End.X = y;
            currentMove.End.Y = x;

            if(game.movePiece(currentPlayer.getIdentity(), currentMove))
            {
                if(game.pieceLastTaken != null)
                {
                    removedGamePiece = GamePiecesArray[game.pieceLastTaken.Y, game.pieceLastTaken.X];
                    Destroy(removedGamePiece.GetComponent<MeshRenderer>());
                    GamePiecesArray[game.pieceLastTaken.X, game.pieceLastTaken.Y] = null;
                }

                GamePiecesArray[selectedGamePiece.CurrentX, selectedGamePiece.CurrentY] = null;

                selectedGamePiece.transform.position = GetTileCenter(x, y);
                GamePiecesArray[x, y] = selectedGamePiece;

                selectedGamePiece.GetComponent<MeshRenderer>().material = previousMat;
                selectedGamePiece = null;

                if (currentPlayer.getIdentity() == identity.X)
                {
                    currentPlayer.setPlayer(identity.O);
                }
                else
                {
                    currentPlayer.setPlayer(identity.X);
                }
            }

            else
            {
                selectedGamePiece.GetComponent<MeshRenderer>().material = previousMat;
                selectedGamePiece = null;
            }


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

            //Debug.Log(selectionX + " " + selectionY);
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
        }

        //private void DrawChessboard()
        //{
        //    //line pointing to the right
        //    Vector3 widthLine = Vector3.right * 8;

        //    //line pointing forward
        //    Vector3 heightLine = Vector3.forward * 8;

        //    for(int i = 0; i <= 8; i++)
        //    {
        //        Vector3 start = Vector3.forward * i;
        //        Debug.DrawLine(start, start + widthLine);
        //        for (int j = 0; j <= 8; j++)
        //        {
        //            start = Vector3.right * j;
        //            Debug.DrawLine(start, start + heightLine);
        //        }
        //    }

        //    //Draw the selection
        //    if(selectionX >= 0 && selectionY >= 0)
        //    {
        //        Debug.DrawLine( Vector3.forward * selectionY + Vector3.right * selectionX,
        //            Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));

        //        Debug.DrawLine(
        //Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
        //Vector3.forward * selectionY + Vector3.right * (selectionX + 1));
        //    }
        //}
    }
}
