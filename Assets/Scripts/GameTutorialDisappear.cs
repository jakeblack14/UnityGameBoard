using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTutorialDisappear : MonoBehaviour
{
    public GameObject[] ToolPanelMenus;

    public void CheckHelpMenu(int index)
    {
        if (!GameBoardData.GameOver)
        {
            if (!ToolPanelMenus[index].activeSelf)
            {
                ToolPanelMenus[index].SetActive(true);
            }

            for (int i = 0; i < ToolPanelMenus.Length; i++)
            {
                if (i != index)
                {
                    ToolPanelMenus[i].SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < ToolPanelMenus.Length; i++)
            {
                ToolPanelMenus[i].SetActive(false);
            }
        }
    }
}

