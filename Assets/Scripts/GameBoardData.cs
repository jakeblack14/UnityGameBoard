using GameCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameBoardData {

    public static RoomInfo[] lobbyRoomInfo = new RoomInfo[10];
    public static List<string> networkGameNames = new List<string>();

    public static bool transitionToNewCharactersDone = false;

    private static bool isNetworkGame;

    private static bool networkGameSelected = false;
    public static bool goingFirst = false;
    private static string currentNetworkGameName;
    private static string currentNetworkGameScene;

    private static string name = "";
    private static string player2Name = "";

    private static Sprite AlienImage;
    private static Sprite AstroImage;

    private static bool musicIsOn = true;

    //character this computer chose
    private static int characterIndexLocal = 0;

    //character other computer chose
    private static int characterIndexNetwork = 0;

    private static bool singlePlayerIsAlien = false;

    private static bool localGamePlayer1IsAlien = false;

    private static bool networkGameLocalPlayerIsAstro = false;

    private static bool isPlayer2 = false;

    private static bool gameWasInitialized = false;

    public static bool SinglePlayerIsAlien
    {
        get
        {
            return singlePlayerIsAlien;
        }
        set
        {
            singlePlayerIsAlien = value;
        }
    }

    public static bool LocalGamePlayer1IsAlien
    {
        get
        {
            return localGamePlayer1IsAlien;
        }
        set
        {
            localGamePlayer1IsAlien = value;
        }
    }

    public static bool NetworkGameLocalPlayerIsAstronaut
    {
        get
        {
            return networkGameLocalPlayerIsAstro;
        }
        set
        {
            networkGameLocalPlayerIsAstro = value;
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

    public static int CharacterIndexLocal
    {
        get
        {
            return characterIndexLocal;
        }

        set
        {
            characterIndexLocal = value;
        }
    }

    public static int CharacterIndexNetwork
    {
        get
        {
            return characterIndexNetwork;
        }

        set
        {
            characterIndexNetwork = value;
        }
    }

    public static bool NetworkGameSelected
    {
        get
        {
            return networkGameSelected;
        }
        set
        {
            networkGameSelected = value;
        }
    }

    public static string CurrentNetworkGameName
    {
        get
        {
            return currentNetworkGameName;
        }
        set
        {
            currentNetworkGameName = value;
        }
    }

    public static string CurrentNetworkGameScene
    {
        get
        {
            return currentNetworkGameScene;
        }
        set
        {
            currentNetworkGameScene = value;
        }
    }

    public static bool MusicIsOn
    {
        get
        {
            return musicIsOn;
        }
        set
        {
            musicIsOn = value;
        }
    }
}
