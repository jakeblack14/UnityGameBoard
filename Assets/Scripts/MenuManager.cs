using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public List<GameObject> MenuPanels;
    private GameObject BackButton;
    public InputField inputField;
    public Sprite selectedImage;
    public Sprite originalImage;

    private Button[] locationButtons;
    private Button[] levelButtons;
    private Button[] turnButtons;

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
        if(inputField.text == "")
        {
            GameBoardData.Name = "Player 1";
        }
        else
        {
            GameBoardData.Name = inputField.text;
        }
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
            GameCore.BoardManager.playerGoingFirst = true;
        }
        else
        {
            GameCore.BoardManager.playerGoingFirst = false;
        }
    }

    public void backButtonClicked()
    {
        int index = 0;

        for (int i = 0; i < 6; i++)
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
        currentButton.image.sprite = selectedImage;

        for (int i = 0; i < currentButtons.Length; i++)
        {
            if (i != index)
            {
                currentButtons[i].image.sprite = originalImage;
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
        GameCore.BoardManager.waitForNetwork = true;
        SceneManager.LoadScene("MultiPlayer Scene");
    }

}
