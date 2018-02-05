using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsManager : MonoBehaviour {

    private LoadSceneOnClick loadScene = new LoadSceneOnClick();

    public List<Button> locationButtons;
    public List<Button> levelButtons;
    public List<Button> turnButtons;

    private Color32 selectedColor = new Color32(230, 101, 255, 255);

    private Button currentLocationButton;
    private Button currentLevelButton;
    private Button currentTurnButton;

    private string currentLevelSelected;
    private string currentTurnSelected;

    public void locationSelected(int index)
    {
        currentLocationButton = locationButtons[index];

        ColorBlock tempColorBlock = currentLocationButton.colors;
        ColorBlock originalColorBlock = tempColorBlock;

        tempColorBlock.normalColor = selectedColor;
        tempColorBlock.highlightedColor = selectedColor;
        currentLocationButton.colors = tempColorBlock;

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
        currentLevelButton = levelButtons[index];

        ColorBlock tempColorBlock = currentLevelButton.colors;
        ColorBlock originalColorBlock = tempColorBlock;

        tempColorBlock.normalColor = selectedColor;
        tempColorBlock.highlightedColor = selectedColor;
        currentLevelButton.colors = tempColorBlock;

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
        currentTurnButton = turnButtons[index];

        ColorBlock tempColorBlock = currentTurnButton.colors;
        ColorBlock originalColorBlock = tempColorBlock;

        tempColorBlock.normalColor = selectedColor;
        tempColorBlock.highlightedColor = selectedColor;
        currentTurnButton.colors = tempColorBlock;

        for (int i = 0; i < 2; i++)
        {
            if (i != index)
            {
                turnButtons[i].colors = originalColorBlock;
            }
        }
    }

    public void nextButtonClicked()
    {
        //if(currentLevelButton == levelButtons[0])
        //{
        //    currentLevelSelected = "Easy";
        //}

        //Debug.Log(currentLevelSelected);

        if (currentLocationButton == locationButtons[0])
        {
            loadScene.LoadByIndex(2);
        }
        else
        {
            loadScene.LoadByIndex(1);
        }
    }
}
