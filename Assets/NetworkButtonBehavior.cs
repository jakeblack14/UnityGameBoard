using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NetworkButtonBehavior : MonoBehaviour {

    private Button currentButton;
    private Text[] buttonText;

    private float doubleClickTimeLimit = 0.25f;

    // Use this for initialization
    void Start () {
        currentButton = GetComponent<Button>();
        currentButton.onClick.AddListener(TaskOnClick);

        buttonText = currentButton.GetComponentsInChildren<Text>();
    }

    public void TaskOnClick()
    {
        GameBoardData.NetworkGameSelected = true;
        GameBoardData.CurrentNetworkGameName = buttonText[0].text.ToString();
        GameBoardData.CurrentNetworkGameScene = buttonText[1].text.ToString();
        GameBoardData.CharacterIndexNetwork = Convert.ToInt32(buttonText[2].text);
        GameBoardData.Player2Name = buttonText[3].text.ToString();

        Debug.Log("Join game stuff " + GameBoardData.CurrentNetworkGameName + " " + GameBoardData.CurrentNetworkGameScene);
    }

}

