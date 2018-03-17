using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//namespace GameCore


    public class GamePieces : MonoBehaviour
    {

        public int CurrentX { get; set; }
        public int CurrentY { get; set; }
        //public bool isPlayerX;
        public GameCore.identity pieceIdentity;

        public void SetPosition(int x, int y)
        {
            CurrentX = x;
            CurrentY = y;
        }

        public bool isMoveValid(int x, int y)
        {
            return true;
        }
    }
