using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

    public void LoadByIndex(int sceneIndex)
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
