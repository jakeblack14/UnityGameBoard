using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkButtonBehavior : MonoBehaviour {

    private Button currentButton;
    private Text[] buttonText;

    // Use this for initialization
    void Start () {
        currentButton = GetComponent<Button>();
        currentButton.onClick.AddListener(TaskOnClick);

        buttonText = currentButton.GetComponentsInChildren<Text>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TaskOnClick()
    {
        GameBoardData.NetworkGameSelected = true;
        GameBoardData.CurrentNetworkGameName = buttonText[0].text.ToString();
        GameBoardData.CurrentNetworkGameScene = buttonText[1].text.ToString();

        Debug.Log("Join game stuff " + GameBoardData.CurrentNetworkGameName + " " + GameBoardData.CurrentNetworkGameScene);
    }
}
