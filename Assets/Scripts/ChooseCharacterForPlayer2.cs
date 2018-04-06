using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChooseCharacterForPlayer2 : MonoBehaviour {

    private Sprite[] Characters;
    private Button[] CharacterButtons;

    private Sprite currentCharacter;

    public GameObject Astros;
    public GameObject Aliens;

    public Sprite AstroDisabledBackground;
    public Sprite AlienDisabledBackground;

    private Sprite currentBackground;
    private Sprite currentDisabledBackground;

    private bool isActivated = false;

    private int currentIndex;

    // Use this for initialization
    void Start()
    {
        if (GameBoardData.LocalGamePlayer1IsAlien || GameBoardData.NetworkGameLocalPlayerIsAstronaut)
        {
            Characters = Resources.LoadAll<Sprite>("AstronautSprites");

            Astros.SetActive(true);
            Aliens.SetActive(false);

            CharacterButtons = Astros.GetComponentsInChildren<Button>();

            currentDisabledBackground = AstroDisabledBackground;

            for (int i = 0; i < 3; i++)
            {
                CharacterButtons[i].image.sprite = AstroDisabledBackground;
            }
        }
        else
        {
            Characters = Resources.LoadAll<Sprite>("AlienSprites");

            Astros.SetActive(false);
            Aliens.SetActive(true);

            CharacterButtons = Aliens.GetComponentsInChildren<Button>();

            currentDisabledBackground = AlienDisabledBackground;

            for (int i = 0; i < 3; i++)
            {
                CharacterButtons[i].image.sprite = AlienDisabledBackground;
            }
        }

        currentCharacter = Characters[0];
    }

    void Activate()
    {
        if (!GameBoardData.NetworkGameSelected)
        {

            if (GameBoardData.LocalGamePlayer1IsAlien || GameBoardData.NetworkGameLocalPlayerIsAstronaut)
            {
                Characters = Resources.LoadAll<Sprite>("AstronautSprites");

                Astros.SetActive(true);
                Aliens.SetActive(false);

                CharacterButtons = Astros.GetComponentsInChildren<Button>();

                currentDisabledBackground = AstroDisabledBackground;

                for (int i = 0; i < 3; i++)
                {
                    CharacterButtons[i].image.sprite = AstroDisabledBackground;
                }
            }
            else
            {
                Characters = Resources.LoadAll<Sprite>("AlienSprites");

                Astros.SetActive(false);
                Aliens.SetActive(true);

                CharacterButtons = Aliens.GetComponentsInChildren<Button>();

                currentDisabledBackground = AlienDisabledBackground;

                for (int i = 0; i < 3; i++)
                {
                    CharacterButtons[i].image.sprite = AlienDisabledBackground;
                }
            }
        }
        else
        {
            if (GameBoardData.CharacterIndexNetwork > 2)
            {
                Characters = Resources.LoadAll<Sprite>("AstronautSprites");

                Astros.SetActive(true);
                Aliens.SetActive(false);

                CharacterButtons = Astros.GetComponentsInChildren<Button>();

                currentDisabledBackground = AstroDisabledBackground;

                for (int i = 0; i < 3; i++)
                {
                    CharacterButtons[i].image.sprite = AstroDisabledBackground;
                }
            }
            else
            {
                Characters = Resources.LoadAll<Sprite>("AlienSprites");

                Astros.SetActive(false);
                Aliens.SetActive(true);

                CharacterButtons = Aliens.GetComponentsInChildren<Button>();

                currentDisabledBackground = AlienDisabledBackground;

                for (int i = 0; i < 3; i++)
                {
                    CharacterButtons[i].image.sprite = AlienDisabledBackground;
                }
            }
        }

        currentCharacter = Characters[0];
    }

    void Update()
    {
        if (!isActivated)
        {
            Activate();
            isActivated = true;
        }
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

        isActivated = false;
        GameBoardData.transitionToNewCharactersDone = false;
    }

    public void SelectCharacter(int index)
    {
        currentCharacter = Characters[index];

        if (GameCore.BoardManager.againstNetwork)
        {
            if (GameBoardData.NetworkGameLocalPlayerIsAstronaut || GameBoardData.CharacterIndexNetwork > 2)
            {
                GameBoardData.CharacterIndexLocal = index;
            }
            else
            {
                GameBoardData.CharacterIndexLocal = index + 3;
            }
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
