using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChooseCharacter : MonoBehaviour
{

    public Text introText;

    private Sprite[] Astronauts;
    private Sprite[] Aliens;

    public Button[] AstronautButtons;
    public Button[] AlienButtons;
    
    private Sprite currentAstro;
    private Sprite currentAlien;

    public Sprite AlienBackground;
    public Sprite AstroBackground;

    public Sprite AstroDisabledBackground;
    public Sprite AlienDisabledBackground;

    // Use this for initialization
    void Start () {
        Astronauts = Resources.LoadAll<Sprite>("AstronautSprites");
        Aliens = Resources.LoadAll<Sprite>("AlienSprites");

        currentAlien = Aliens[0];
        currentAstro = Astronauts[0];
    }

    void Update()
    {
        introText.text = "Welcome " + GameBoardData.Name + ", choose a team as you prepare to conquer the galaxy!!";
    }

    public void On_Next_Click_Button()
    {
        GameBoardData.Astronaut = currentAstro;
        GameBoardData.Alien = currentAlien;
    }

    public void SelectAstronaut(int index)
    {

        GameBoardData.IsAlien = false;
        //AstronautButtons[index].image.sprite = AstroBackground;
        currentAstro = Astronauts[index];

        for (int i = 0; i < 3; i++)
        {
            if (i != index)
            {
                AstronautButtons[i].image.sprite = AstroDisabledBackground;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            AlienButtons[i].image.sprite = AlienDisabledBackground;
        }
    }

    public void SelectAlien(int index)
    {
        GameBoardData.IsAlien = true;

        //AlienButtons[index].image.sprite = AlienBackground;
        currentAlien = Aliens[index];

        for (int i = 0; i< 3; i++)
        {
            if (i != index)
            {
                AlienButtons[i].image.sprite = AlienDisabledBackground;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            AstronautButtons[i].image.sprite = AstroDisabledBackground;
        }
    }
}
