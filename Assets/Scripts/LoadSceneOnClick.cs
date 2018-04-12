using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TechPlanet.SpaceRace;

public class LoadSceneOnClick : MonoBehaviour {

    string sceneName;

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        sceneName = currentScene.name;

        Debug.Log(sceneName);
    }

    public void LoadByIndex()
    {
        if (GameCore.BoardManager.againstNetwork)
        {
            if(sceneName == "MainMenu")
            {
                SceneManager.LoadScene("MainMenu");
            }
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

    public void LoadCredits()
    {
        SceneManager.LoadScene("CreditsVideo");
    }
}
