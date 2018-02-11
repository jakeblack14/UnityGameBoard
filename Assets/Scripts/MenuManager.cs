using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public List<GameObject> MenuPanels;
    private GameObject BackButton;
    public InputField inputField;

    private Button[] locationButtons;
    private Button[] levelButtons;
    private Button[] turnButtons;

    private Color32 selectedColor = new Color32(255, 255, 255, 255);
    private Color32 originalColor = new Color32(139, 207, 255, 255);

    private Button currentButton;

    private int locationIndex;

    void Start()
    {
        BackButton = GameObject.Find("BackButtonPanel");
    }

    void Update()
    {
        if (MenuPanels[0].activeSelf)
        {
            BackButton.SetActive(false);
        }
        else if(MenuPanels[2].activeSelf)
        {
            GameSettingsSetUp();
        }
        else
        {
            BackButton.SetActive(true);
        }
    }

    public void usernameButtonClick()
    {
        GameBoardData.Name = inputField.text;
    }

    public void GameSettingsSetUp()
    {
        locationButtons = GameObject.Find("LocationButtons").GetComponentsInChildren<Button>();
        levelButtons = GameObject.Find("LevelButtons").GetComponentsInChildren<Button>();
        turnButtons = GameObject.Find("TurnButtons").GetComponentsInChildren<Button>();
    }

    public void locationSelected(int index)
    {
        highlightButton(locationButtons, index);

        locationIndex = index;
    }

    public void levelSelected(int index)
    {
        highlightButton(levelButtons, index);
    }

    public void turnSelected(int index)
    {
        highlightButton(turnButtons, index);


        if (index == 0)
        {
            GameCore.BoardManager.firstPlayerIdentity = GameCore.identity.X;
        }
        else
        {
            GameCore.BoardManager.firstPlayerIdentity = GameCore.identity.O;
        }

        //currentButton = turnButtons[index];

        //ColorBlock tempColorBlock = currentButton.colors;
        //ColorBlock originalColorBlock = tempColorBlock;

        //tempColorBlock.normalColor = selectedColor;
        //tempColorBlock.highlightedColor = selectedColor;
        //currentButton.colors = tempColorBlock;

        //for (int i = 0; i < 2; i++)
        //{
        //    if (i != index)
        //    {
        //        turnButtons[i].colors = originalColorBlock;
        //    }
        //}
    }

    public void backButtonClicked()
    {
        int index = 0;

        for (int i = 0; i < 5; i++)
        {
            if (MenuPanels[i].activeSelf)
            {
                index = i;
            }
        }

        if (index == 1)
        {
            MenuPanels[index].SetActive(false);
            MenuPanels[0].SetActive(true);
        }
        else
        {
            MenuPanels[index].SetActive(false);
            MenuPanels[1].SetActive(true);
        }
    }

    private void highlightButton(Button[] currentButtons, int index)
    {
        currentButton = currentButtons[index];
        currentButton.image.color = selectedColor;

        for (int i = 0; i < currentButtons.Length; i++)
        {
            if (i != index)
            {
                currentButtons[i].image.color = originalColor;
            }
        }
    }

    public void gamePlayButtonClicked()
    {
        GameCore.BoardManager.againstAI = true;
        GameCore.BoardManager.againstNetwork = false;
        SceneManager.LoadScene(locationIndex + 1);
    }

    public void localButtonClicked()
    {
        GameCore.BoardManager.againstAI = false;
        GameCore.BoardManager.againstNetwork = false;
        SceneManager.LoadScene(locationIndex + 1);

    }

    public void networkButtonClicked()
    {
        GameCore.BoardManager.againstAI = false;
        GameCore.BoardManager.againstNetwork = true;
        SceneManager.LoadScene("MultiPlayer Scene");
    }

}
