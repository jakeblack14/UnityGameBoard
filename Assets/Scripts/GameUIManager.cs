using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour {

    public Image Player1;
    public Image Player2;

	// Use this for initialization
	void Start () {

        Player1.sprite = GameBoardData.Astronaut;
        Player2.sprite = GameBoardData.Alien;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
