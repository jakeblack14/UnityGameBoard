using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChooseCharacterForPlayer2 : MonoBehaviour {

    public Text introText;

    public Text typeOfCharacter;
    private Sprite[] Characters;
    public Button[] CharacterButtons;

    public Image[] CharacterImages;

    private Sprite currentCharacter;


    public Sprite AlienBackground;
    public Sprite AstroBackground;

    public Sprite AstroDisabledBackground;
    public Sprite AlienDisabledBackground;

    private Sprite currentBackground;
    private Sprite currentDisabledBackground;

    // Use this for initialization
    void Start()
    {
        if(GameBoardData.IsAlien)
        {
            //Player 1 is an alien so player 2 will be an astronaut

            Characters = Resources.LoadAll<Sprite>("AstronautSprites");
            typeOfCharacter.text = "ASTRONAUT";

            currentBackground = AstroBackground;
            currentDisabledBackground = AstroDisabledBackground;

            CharacterButtons[0].image.sprite = AstroBackground;

            for (int i=1; i<3; i++)
            {
                CharacterButtons[i].image.sprite = AstroDisabledBackground;
            }

        }
        else
        {
            //player 2 is an alien
            Characters = Resources.LoadAll<Sprite>("AlienSprites");
            typeOfCharacter.text = "ALIEN";

            currentBackground = AlienBackground;
            currentDisabledBackground = AlienDisabledBackground;

            CharacterButtons[0].image.sprite = AlienBackground;

            for (int i = 1; i < 3; i++)
            {
                CharacterButtons[i].image.sprite = AlienDisabledBackground;
            }
        }

        currentCharacter = Characters[0];
        for (int i = 0; i < 3; i++)
        {
            CharacterImages[i].sprite = Characters[i];
        }
    }

    void Update()
    {
        introText.text = "Hello " + GameBoardData.Player2Name + ", please choose a character!";
    }

    public void On_Next_Click_Button()
    {
        if(GameBoardData.IsAlien)
        {
            GameBoardData.Astronaut = currentCharacter;
        }
        else
        {
            GameBoardData.Alien = currentCharacter;
        }
    }

    public void SelectCharacter(int index)
    {
        currentCharacter = Characters[index];

        for (int i = 0; i < 3; i++)
        {
            if (i != index)
            {
                CharacterButtons[i].image.sprite = currentDisabledBackground;
            }
        }
    }

    //public void SelectAstronaut(int index)
    //{

    //    GameBoardData.IsAlien = false;
    //    //AstronautButtons[index].image.sprite = AstroBackground;
    //    currentAstro = Astronauts[index];

    //    for (int i = 0; i < 3; i++)
    //    {
    //        if (i != index)
    //        {
    //            AstronautButtons[i].image.sprite = AstroDisabledBackground;
    //        }
    //    }

    //    for (int i = 0; i < 3; i++)
    //    {
    //        AlienButtons[i].image.sprite = AlienDisabledBackground;
    //    }
    //}

    //public void SelectAlien(int index)
    //{
    //    GameBoardData.IsAlien = true;

    //    //AlienButtons[index].image.sprite = AlienBackground;
    //    currentAlien = Aliens[index];

    //    for (int i = 0; i < 3; i++)
    //    {
    //        if (i != index)
    //        {
    //            AlienButtons[i].image.sprite = AlienDisabledBackground;
    //        }
    //    }

    //    for (int i = 0; i < 3; i++)
    //    {
    //        AstronautButtons[i].image.sprite = AstroDisabledBackground;
    //    }
    //}
}
