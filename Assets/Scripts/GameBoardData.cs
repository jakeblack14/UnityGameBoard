using GameCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameBoardData {

    //static Player PlayerX = new Player();

    //static Player PlayerO = new Player();

    private static bool isNetworkGame;

    private static string name;

    public static string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }
}
