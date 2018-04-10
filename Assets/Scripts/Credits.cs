using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(LoadMainMenu());
	}

    private IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(11.0f);

        SceneManager.LoadScene("MainMenu");
    }
}
