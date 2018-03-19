using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTutorialDisappear : MonoBehaviour
{
    public GameObject[] ToolPanelMenus;

    public void CheckHelpMenu(int index)
    {
        if (ToolPanelMenus[index].activeSelf)
        {
            ToolPanelMenus[index].SetActive(false);
        }
        else
        {
            ToolPanelMenus[index].SetActive(true);
        }

        for (int i = 0; i < 3; i++)
        {
            if (i != index)
            {
                ToolPanelMenus[i].SetActive(false);
            }
        }
    }
}

