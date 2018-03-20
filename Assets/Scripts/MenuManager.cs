﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public List<GameObject> MenuPanels;
    private GameObject BackButton;
    public Text usernameText;
    public InputField inputField;
    public Sprite selectedImage;
    public Sprite originalImage;

    private Button[] locationButtons;
    private Button[] levelButtons;
    private Button[] turnButtons;

    private Button currentButton;

    private int locationIndex;

    private bool player1NameInitialized = false;
    private bool player2NameInitialized = false;

    void Start()
    {
        BackButton = GameObject.Find("BackButtonPanel");

        if(!GameBoardData.GameInitialized)
        {
            GameBoardData.GameInitialized = true;
        }
        else
        {
            MenuPanels[0].SetActive(false);
            MenuPanels[1].SetActive(true);
        }

    }

    void Update()
    {
        if (MenuPanels[0].activeSelf && !MenuPanels[5].activeSelf && !MenuPanels[6].activeSelf)
        {
            if (!GameBoardData.IsPlayer2)
            {
                BackButton.SetActive(false);
            }
        }
        else if(MenuPanels[2].activeSelf)
        {
            GameSettingsSetUp();
        }
        else
        {
            BackButton.SetActive(true);
        }

        if(!GameBoardData.IsPlayer2)
        {
            usernameText.enabled = false;
            if(player1NameInitialized)
            {
                inputField.placeholder.GetComponent<Text>().text = GameBoardData.Name;
            }
            else
            {
                inputField.placeholder.GetComponent<Text>().text = "Player 1";
            }
        }
        else
        {
            usernameText.enabled = true;
            if (player2NameInitialized)
            {
                inputField.placeholder.GetComponent<Text>().text = GameBoardData.Player2Name;
            }
            else
            {
                inputField.placeholder.GetComponent<Text>().text = "Player 2";
            }
        }
    }

    public void usernameButtonClick()
    {
        //player 1 just entered their name
        if (!GameBoardData.IsPlayer2)
        {
            if (inputField.text == "")
            {
                GameBoardData.Name = inputField.placeholder.GetComponent<Text>().text;
            }
            else
            {
                GameBoardData.Name = inputField.text;
            }

            player1NameInitialized = true;
            MenuPanels[1].SetActive(true);
            MenuPanels[0].SetActive(false);
        }
        //player 2 just entered their name
        else
        {
            if (inputField.text == "")
            {
                GameBoardData.Player2Name = inputField.placeholder.GetComponent<Text>().text;
            }
            else
            {
                GameBoardData.Player2Name = inputField.text;
            }

            inputField.text = "";
            player2NameInitialized = true;
            MenuPanels[5].SetActive(true);
            MenuPanels[0].SetActive(false);
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
        if (MenuPanels[6].activeSelf || MenuPanels[7].activeSelf)
        {
            MenuPanels[6].SetActive(false);
            MenuPanels[7].SetActive(false);
        }
        else
        {

            int index = 0;

            for (int i = 0; i < 6; i++)
            {
                if (MenuPanels[i].activeSelf)
                {
                    index = i;
                }
            }

            if (index == 4)
            {
                MenuPanels[index].SetActive(false);
                MenuPanels[1].SetActive(true);
            }
            else if(index == 5)
            {
                MenuPanels[index].SetActive(false);
                MenuPanels[0].SetActive(true);
            }
            else if(index == 0)
            {
                MenuPanels[index].SetActive(false);
                MenuPanels[3].SetActive(true);
                GameBoardData.IsPlayer2 = false;
            }
            else
            {
                MenuPanels[index].SetActive(false);
                MenuPanels[index - 1].SetActive(true);
            }
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
        MenuPanels[2].SetActive(false);
        MenuPanels[3].SetActive(true);
    }

    public void characterNextButtonClicked()
    {
        if (GameCore.BoardManager.againstAI)
        {
            if (locationIndex == 0)
            {
                SceneManager.LoadScene("AsteroidScene");
            }
            else if (locationIndex == 1)
            {
                SceneManager.LoadScene("MilkyWayScene");
            }
        }
        else
        {
            GameBoardData.IsPlayer2 = true;
            inputField.text = "";
            MenuPanels[3].SetActive(false);
            MenuPanels[0].SetActive(true);
        }
    }

    public void characterForPlayer2NextButtonClicked()
    {
        //local game
        if (!GameCore.BoardManager.againstNetwork)
        {
            if (locationIndex == 0)
            {
                SceneManager.LoadScene("AsteroidScene");
            }
            else if (locationIndex == 1)
            {
                SceneManager.LoadScene("MilkyWayScene");
            }
        }
        //else network game
    }

    public void singlePlayerButtonClicked()
    {
        GameCore.BoardManager.againstAI = true;
        GameCore.BoardManager.againstNetwork = false;
        GameBoardData.Player2Name = "Computer";
    }

    public void localButtonClicked()
    {
        GameCore.BoardManager.againstAI = false;
        GameCore.BoardManager.againstNetwork = false;
    }

    public void networkButtonClicked()
    {
        GameCore.BoardManager.againstAI = false;
        GameCore.BoardManager.againstNetwork = true;
        GameCore.BoardManager.waitForNetwork = true;
        SceneManager.LoadScene("MilkyWayScene");
    }

}
