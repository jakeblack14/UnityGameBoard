using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

    public InputField username;
    public Image character;

	// Use this for initialization
	void Start () {
        
	}

    void Update()
    {
        username.text = GameBoardData.Name;

        if (GameBoardData.IsAlien)
        {
            character.sprite = GameBoardData.Alien;
        }
        else
        {
            character.sprite = GameBoardData.Astronaut;
        }
    }
}
