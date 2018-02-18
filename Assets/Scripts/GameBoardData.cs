using GameCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameBoardData {

    //static Player PlayerX = new Player();

    //static Player PlayerO = new Player();

    private static bool isNetworkGame;

    private static string name;

    private static Sprite AlienImage;
    private static Sprite AstroImage;

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
