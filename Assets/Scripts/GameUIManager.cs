using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour {

    public Image Player1;
    public Image Player2;

    public Image Player1Background;
    public Image Player2Background;

    public Sprite AlienBackground;
    public Sprite AstroBackground;

    public Text username;
    public Text player2Username;

    private Sprite[] Astronauts;
    private Sprite[] Aliens;

    //public Text gameOverText;

    // Use this for initialization
    void Start () {

        if (GameCore.BoardManager.againstNetwork)
        {
            Astronauts = Resources.LoadAll<Sprite>("AstronautSprites");
            Aliens = Resources.LoadAll<Sprite>("AlienSprites");

            //1-3 astronaut
            //4-6 alien

            if(GameBoardData.CharacterIndexLocal < 4)
            {
                //player 1 is an astronaut

                Player1.sprite = Astronauts[GameBoardData.CharacterIndexLocal - 1];
                Player2.sprite = Aliens[GameBoardData.CharacterIndexNetwork - 4];

                Player2Background.sprite = AlienBackground;
                Player1Background.sprite = AstroBackground;
            }
            else
            {
                //player 2 is an astronaut

                Player1.sprite = Aliens[GameBoardData.CharacterIndexNetwork - 4];
                Player2.sprite = Astronauts[GameBoardData.CharacterIndexLocal - 1];

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

            username.text = GameBoardData.Name;
            player2Username.text = GameBoardData.Player2Name;
        }
    }

    private void Update()
    {
        
    }
}
