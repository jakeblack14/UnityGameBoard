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
        //currentButton.onClick.AddListener(TaskOnClick);

        buttonText = currentButton.GetComponentsInChildren<Text>();
        StartCoroutine(InputListener());
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

    // Update is called once per frame
    private IEnumerator InputListener()
    {
        while (enabled)
        { //Run as long as this is activ

            if (Input.GetMouseButtonDown(0))
                yield return ClickEvent();

            yield return null;
        }
    }

    private IEnumerator ClickEvent()
    {
        //pause a frame so you don't pick up the same mouse down event.
        yield return new WaitForEndOfFrame();

        float count = 0f;
        while (count < doubleClickTimeLimit)
        {
            if (Input.GetMouseButtonDown(0))
            {
                DoubleClick();
                yield break;
            }
            count += Time.deltaTime;// increment counter by change in time between frames
            yield return null; // wait for the next frame
        }

        TaskOnClick();
    }

    private void DoubleClick()
    {
        Debug.Log("double click");
        TaskOnClick();
        GameObject goJeff = GameObject.Find("Canvas");
        MenuManager jeffGo = goJeff.GetComponent<MenuManager>();
        jeffGo.joinNetworkGame();
    }

}

