using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTutorialDisappear : MonoBehaviour
{
    public GameObject Tutorial;

    public void CheckHelpMenu()
    {
        if (Tutorial.activeSelf)
        {
            Tutorial.SetActive(false);
        }
        else
        {
            Tutorial.SetActive(true);
        }
    }
}

