using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseAstro : MonoBehaviour {

    public Text introText;

    public Text typeOfCharacter;
    private Sprite[] Characters;
    public Button[] CharacterButtons;

    private Sprite currentCharacter;

    public Sprite currentDisabledBackground;

    private int currentIndex;

    // Use this for initialization
    void Start()
    {
        Characters = Resources.LoadAll<Sprite>("AstronautSprites");
        typeOfCharacter.text = "ASTRONAUT";
    }

    void Update()
    {
        introText.text = "Hello! Please choose a character!";
    }

    public void On_Next_Click_Button()
    {
        if (!GameBoardData.NetworkGameSelected)
        {
            if (GameBoardData.LocalGamePlayer1IsAlien)
            {
                GameBoardData.Astronaut = currentCharacter;
            }
            else
            {
                GameBoardData.Alien = currentCharacter;
            }
        }
    }

    public void SelectCharacter(int index)
    {
        currentCharacter = Characters[index];

        if (GameBoardData.NetworkGameLocalPlayerIsAstronaut || GameBoardData.CharacterIndexNetwork > 2)
        {
            GameBoardData.CharacterIndexLocal = index;
        }
        else
        {
            GameBoardData.CharacterIndexLocal = index + 3;
        }

        for (int i = 0; i < 3; i++)
        {
            if (i != index)
            {
                CharacterButtons[i].image.sprite = currentDisabledBackground;
            }
        }
    }
}
