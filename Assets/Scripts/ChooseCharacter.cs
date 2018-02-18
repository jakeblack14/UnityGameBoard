using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCharacter : MonoBehaviour {

    private Sprite[] Astronauts;
    private Sprite[] Aliens;
    public Image Astro;
    public Image Alien;
    
    private Sprite currentAstro;
    private Sprite currentAlien;

    public Button AstroBack;
    public Button AstroNext;

    public Button AlienBack;
    public Button AlienNext;

    static int AstroCount = 0;
    static int AlienCount = 0;

    // Use this for initialization
    void Start () {
        Astronauts = Resources.LoadAll<Sprite>("AstronautSprites");
        Aliens = Resources.LoadAll<Sprite>("AlienSprites");
    }

    void Update()
    {
        if(AstroCount == 0)
        {
            AstroBack.image.enabled = false;
            AstroNext.image.enabled = true;
        }
        else if(AstroCount == Astronauts.Length - 1)
        {
            AstroNext.image.enabled = false;
            AstroBack.image.enabled = true;
        }
        else
        {
            AstroNext.image.enabled = true;
            AstroBack.image.enabled = true;
        }

        if (AlienCount == 0)
        {
            AlienBack.image.enabled = false;
            AlienNext.image.enabled = true;
        }
        else if (AlienCount == Aliens.Length - 1)
        {
            AlienNext.image.enabled = false;
            AlienBack.image.enabled = true;
        }
        else
        {
            AlienNext.image.enabled = true;
            AlienBack.image.enabled = true;
        }
    }

    public void On_Astro_Click_Button()
    {
        if (AstroCount != Astronauts.Length - 1)
        {
            AstroCount++;
            Astro.sprite = Astronauts[AstroCount];
            AstroBack.enabled = true;
        }

        if (AstroCount == Astronauts.Length - 1)
        {
            AstroNext.enabled = false;
        }

        currentAstro = Astronauts[AstroCount];
    }

    public void On_Alien_Click_Button()
    {
        if (AlienCount != Aliens.Length - 1)
        {
            AlienCount++;
            Alien.sprite = Aliens[AlienCount];
            AlienBack.enabled = true;
        }

        if (AlienCount == Astronauts.Length - 1)
        {
            AlienNext.enabled = false;
        }

        currentAlien = Aliens[AlienCount];
   }

    public void On_Astro_Back_Click_Button()
    {
        if (AstroCount != 0)
        {
            AstroCount--;
            Astro.sprite = Astronauts[AstroCount];
            AstroNext.enabled = true;
        }

        if (AstroCount == 0)
        {
            AstroBack.enabled = false;
        }

        currentAstro = Astronauts[AstroCount];

    }

    public void On_Alien_Back_Click_Button()
    {
        if (AlienCount != 0)
        {
            AlienCount--;
            Alien.sprite = Aliens[AlienCount];
            AlienNext.enabled = true;
        }

        if (AlienCount == 0)
        {
            AlienBack.enabled = false;
        }

        currentAlien = Aliens[AlienCount];
    }

    public void On_Next_Click_Button()
    {
        GameBoardData.Astronaut = currentAstro;
        GameBoardData.Alien = currentAlien;
    }
}
