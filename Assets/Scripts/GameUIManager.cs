using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TechPlanet.SpaceRace;

public class GameUIManager : MonoBehaviour {

    public Image Player1;
    public Image Player2;

    public Image Player1Background;
    public Image Player2Background;

    public Sprite AlienBackground;
    public Sprite AstroBackground;

    public Text username;
    public static Text player2Username;

    private Sprite[] Astronauts;
    private Sprite[] Aliens;

    // Use this for initialization
    void Start () {

        if (GameCore.BoardManager.againstNetwork)
        {
            Astronauts = Resources.LoadAll<Sprite>("AstronautSprites");
            Aliens = Resources.LoadAll<Sprite>("AlienSprites");

            if(GameBoardData.CharacterIndexLocal < 3)
            {
                Player1.sprite = Astronauts[GameBoardData.CharacterIndexLocal];
                Player2.sprite = Aliens[GameBoardData.CharacterIndexNetwork - 3];

                Player2Background.sprite = AlienBackground;
                Player1Background.sprite = AstroBackground;
            }
            else
            {
                Player1.sprite = Aliens[GameBoardData.CharacterIndexLocal - 3];
                Player2.sprite = Astronauts[GameBoardData.CharacterIndexNetwork];

                Player1Background.sprite = AlienBackground;
                Player2Background.sprite = AstroBackground;
            }
        }
        else
        {

            if (GameBoardData.IsAlien)
            {
                Player1.sprite = GameBoardData.Alien;
                Player2.sprite = GameBoardData.Astronaut;

                Player1Background.sprite = AlienBackground;
                Player2Background.sprite = AstroBackground;
            }
            else
            {
                Player1.sprite = GameBoardData.Astronaut;
                Player2.sprite = GameBoardData.Alien;

                Player2Background.sprite = AlienBackground;
                Player1Background.sprite = AstroBackground;
            }
        }

        username.text = GameBoardData.Name;
        player2Username.text = GameBoardData.Player2Name;
    }
    public void SetPlayer2(string name)
    {
        player2Username.text = name;
    }
    private void Update()
    {
       // player2Username.text = GameBoardData.Player2Name;
    }
}
