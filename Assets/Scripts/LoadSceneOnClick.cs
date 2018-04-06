using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TechPlanet.SpaceRace;

public class LoadSceneOnClick : MonoBehaviour {

    public void LoadByIndex(int sceneIndex)
    {
        if (GameCore.BoardManager.againstNetwork)
        {
            TechPlanet.SpaceRace.MultiplayerLauncher.LeaveGame();
        }
        else
        {
            if (!GameBoardData.GameInitialized)
            {
                StartCoroutine(GameObject.FindObjectOfType<FadeEffect>().EffectsAndLoadScene("MainMenu"));
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
