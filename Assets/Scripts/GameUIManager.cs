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

    //public Text gameOverText;

	// Use this for initialization
	void Start () {

        if(GameBoardData.IsAlien)
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
    }

    private void Update()
    {
        
    }
}
