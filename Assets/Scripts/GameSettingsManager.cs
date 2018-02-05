using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsManager : MonoBehaviour {

    public List<Button> locationButtons;
    public List<Button> levelButtons;
    public List<Button> turnButtons;

    private Color32 selectedColor = new Color32(230, 101, 255, 255);

    private Button currentButton;

    public void locationSelected(int index)
    {
        currentButton = locationButtons[index];

        ColorBlock tempColorBlock = currentButton.colors;
        ColorBlock originalColorBlock = tempColorBlock;

        tempColorBlock.normalColor = selectedColor;
        tempColorBlock.highlightedColor = selectedColor;
        currentButton.colors = tempColorBlock;

        for (int i = 0; i < 2; i++)
        {
            if (i != index)
            {
                locationButtons[i].colors = originalColorBlock;
            }
        }
    }

    public void levelSelected(int index)
    {
        currentButton = levelButtons[index];

        ColorBlock tempColorBlock = currentButton.colors;
        ColorBlock originalColorBlock = tempColorBlock;

        tempColorBlock.normalColor = selectedColor;
        tempColorBlock.highlightedColor = selectedColor;
        currentButton.colors = tempColorBlock;

        for (int i = 0; i < 3; i++)
        {
            if (i != index)
            {
                levelButtons[i].colors = originalColorBlock;
            }
        }
    }

    public void turnSelected(int index)
    {
        currentButton = turnButtons[index];

        ColorBlock tempColorBlock = currentButton.colors;
        ColorBlock originalColorBlock = tempColorBlock;

        tempColorBlock.normalColor = selectedColor;
        tempColorBlock.highlightedColor = selectedColor;
        currentButton.colors = tempColorBlock;

        for (int i = 0; i < 2; i++)
        {
            if (i != index)
            {
                turnButtons[i].colors = originalColorBlock;
            }
        }
    }
}
