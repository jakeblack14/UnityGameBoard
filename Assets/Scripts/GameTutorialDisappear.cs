using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTutorialDisappear : MonoBehaviour
{
    public GameObject[] ToolPanelMenus;

    public void CheckHelpMenu(int index)
    {
        if(!ToolPanelMenus[index].activeSelf)
        {
            ToolPanelMenus[index].SetActive(true);
        }

        for (int i = 0; i < 4; i++)
        {
            if (i != index)
            {
                ToolPanelMenus[i].SetActive(false);
            }
        }
    }
}

