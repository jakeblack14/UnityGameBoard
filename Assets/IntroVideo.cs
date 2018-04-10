using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroVideo : MonoBehaviour {

    // Use this for initialization
    void Start () {

        StartCoroutine(LoadMainMenu());
    }

    private IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(14.0f);

        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("MainMenu");
        }
	}
}
