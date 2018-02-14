using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTutorialDisappear : MonoBehaviour
{
    GameObject Tutorial;

    static bool on = false;
    // Use this for initialization

    public void CheckHelpMenu()
    {
        if (on)
        {
            Tutorial.SetActive(false);
            on = false;
        }
        else
        {
            Tutorial.SetActive(true);
            on = true;
        }
    }
}

