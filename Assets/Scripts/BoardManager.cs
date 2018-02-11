﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TechPlanet.SpaceRace;
//TestChanges
namespace GameCore {
    public class BoardManager : MonoBehaviour {

        public static identity firstPlayerIdentity = identity.X;
        public static Boolean againstNetwork = false;
        public static Boolean againstAI = false;
        //MultiplayerLauncher multi = new MultiplayerLauncher();
       // private TechPlanet.SpaceRace.MultiplayerLauncher multi = null;


        public GamePieces[,] GamePiecesArray { set; get; }

        private GamePieces selectedGamePiece;
        private GamePieces removedGamePiece;

        private static GameBoard game;

        private const float TILE_SIZE = 1.0f;
        private const float TILE_OFFSET = 0.5f;


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
        private static bool wasCreated;

        private Material previousMat;
        public Material selectedMat;

        private Move currentMove;


        private void setFirstPlayer()
        {
            if (firstPlayerIdentity == identity.X)
            {
                currentPlayer = PlayerX;
            }
            else
            {
                currentPlayer = PlayerO;
            }
        }

        private void Start()
        {
            //Set the player parameters based on whether we are playing against an AI or network (or the default - against local)
            if (againstNetwork)
            {
                //Set a new network player as the opponent (O)
                PlayerO = new NetworkPlayer(identity.O);

            }
            else if (againstAI)
            {
                //Set a new AI player as the opponent (O)
                PlayerO = new AIPlayer(identity.O);
            }

            wasCreated = false;


            SpawnAllGamePieces();


            setFirstPlayer();

            game = new GameBoard();
            game.newGameBoard(currentPlayer.getIdentity());

            currentMove = new Move();
            //SendTheMove()
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
                        MoveGamePiece(automove.End.col, automove.End.row);

                    }
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    Move jeffsMove = new Move();
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
                            jeffsMove.Begin.col = selectionX;
                            jeffsMove.Begin.row = selectionY;
                        }
                        else
                        {
                            MoveGamePiece(selectionX, selectionY);
                            jeffsMove.Begin.col = selectionX;
                            jeffsMove.Begin.row = selectionY;
                            GameObject goJeff = GameObject.Find("GameBoard");
                            MultiplayerLauncher jeff = goJeff.GetComponent<MultiplayerLauncher>();
                            if (againstNetwork)
                            {
                                jeff.SendTheMove(jeffsMove.Begin.col, jeffsMove.Begin.row, jeffsMove.End.col, jeffsMove.End.row);
                            }

                        }
                    }
                }
            }
            else
            {
                //pull up Game Over Screen
                if (!wasCreated)
                {
                    GameObject gameOverPanel = Instantiate(GameOverPanel) as GameObject;
                    gameOverPanel.transform.SetParent(canvas.transform, false);
                    wasCreated = true;

                    if (currentPlayer == PlayerX)
                    {
                        winnerText.text = "Player X wins!!";
                    }
                    else if (currentPlayer == PlayerO)
                    {
                        winnerText.text = "Player O wins!!";
                    }

                }
            }
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

                selectedGamePiece.transform.localPosition = GetTileCenter(x, y);
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
                if (selectedGamePiece.GetComponent<MeshRenderer>())
                {
                    selectedGamePiece.GetComponent<MeshRenderer>().material = previousMat;
                }
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

        
        public static Board getBoard()
        {
            return game.getBoard();
        }
    }
}
