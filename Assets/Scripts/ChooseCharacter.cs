using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCharacter : MonoBehaviour {

    public Text introText;

    private Sprite[] Astronauts;
    private Sprite[] Aliens;

    public Image Astro;
    public Image Alien;

    public Sprite disabledAlienNext;
    public Sprite disabledAlienBack;

    public Sprite disabledAstroNext;
    public Sprite disabledAstroBack;

    private Sprite originalAlienNext;
    private Sprite originalAlienBack;

    private Sprite originalAstroNext;
    private Sprite originalAstroBack;
    
    private Sprite currentAstro;
    private Sprite currentAlien;

    public Image AlienBackground;
    public Image AstroBackground;

    public Sprite disabledAlienBackground;
    public Sprite disabledAstroBackground;

    private Sprite originalAlienBackground;
    private Sprite originalAstroBackground;

    public Button AstroBack;
    public Button AstroNext;

    public Button AlienBack;
    public Button AlienNext;

    static int AstroCount = 0;
    static int AlienCount = 0;

    private ColorBlock disabledColor;
    private Color32 disabled = new Color32(116, 116, 116, 255);

    // Use this for initialization
    void Start () {
        Astronauts = Resources.LoadAll<Sprite>("AstronautSprites");
        Aliens = Resources.LoadAll<Sprite>("AlienSprites");

        introText.text = "Welcome " + GameBoardData.Name + ", choose a team as you prepare to conquer the galaxy!!";

        currentAlien = Aliens[0];
        currentAstro = Astronauts[0];

        originalAlienBack = AlienBack.image.sprite;
        originalAlienNext = AlienNext.image.sprite;

        originalAstroBack = AstroBack.image.sprite;
        originalAstroNext = AstroNext.image.sprite;

        originalAlienBackground = AlienBackground.sprite;
        originalAstroBackground = AstroBackground.sprite;

        AlienBackground.sprite = disabledAlienBackground;

        AlienBack.image.sprite = disabledAlienBack;
        AlienNext.image.sprite = disabledAlienNext;

        AlienBack.enabled = false;
        AlienNext.enabled = false;
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

    public void SelectAstronaut()
    {
        GameBoardData.IsAlien = false;

        AlienBack.enabled = false;
        AlienNext.enabled = false;

        AstroBack.enabled = true;
        AstroNext.enabled = true;

        AlienBack.image.sprite = disabledAlienBack;
        AlienNext.image.sprite = disabledAlienNext;

        AstroBack.image.sprite = originalAstroBack;
        AstroNext.image.sprite = originalAstroNext;

        AlienBackground.sprite = disabledAlienBackground;
        AstroBackground.sprite = originalAstroBackground;
    }

    public void SelectAlien()
    {
        GameBoardData.IsAlien = true;

        AlienBack.enabled = true;
        AlienNext.enabled = true;

        AstroBack.enabled = false;
        AstroNext.enabled = false;

        AlienBack.image.sprite = originalAlienBack;
        AlienNext.image.sprite = originalAlienNext;

        AstroBack.image.sprite = disabledAstroBack;
        AstroNext.image.sprite = disabledAstroNext;

        AlienBackground.sprite = originalAlienBackground;
        AstroBackground.sprite = disabledAstroBackground;
    }
}
