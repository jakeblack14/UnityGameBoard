using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePieces : MonoBehaviour {
    //test
    public int CurrentX { get; set; }
    public int CurrentY { get; set; }
    public bool isPlayerX;

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
