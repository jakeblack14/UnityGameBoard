using GameCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameBoardData {

    private static bool isNetworkGame;

    private static string name = "";
    private static string player2Name = "";

    private static Sprite AlienImage;
    private static Sprite AstroImage;

    private static bool isAlien;

    private static bool isPlayer2 = false;

    private static bool gameWasInitialized = false;

    public static bool IsAlien
    {
        get
        {
            return isAlien;
        }
        set
        {
            isAlien = value;
        }
    }

    public static bool IsPlayer2
    {
        get
        {
            return isPlayer2;
        }
        set
        {
            isPlayer2 = value;
        }
    }

    public static bool GameInitialized
    {
        get
        {
            return gameWasInitialized;
        }
        set
        {
            gameWasInitialized = value;
        }
    }

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

    public static string Player2Name
    {
        get
        {
            return player2Name;
        }
        set
        {
            player2Name = value;
        }
    }

    public static Sprite Alien
    {
        get
        {
            return AlienImage;
        }

        set
        {
            AlienImage = value;
        }
    }

    public static Sprite Astronaut
    {
        get
        {
            return AstroImage;
        }

        set
        {
            AstroImage = value;
        }
    }
}
