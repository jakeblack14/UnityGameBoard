using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroVideo : MonoBehaviour {

    public VideoClip[] ClipArray;
    public VideoPlayer videoPlayer1;
    public VideoPlayer videoPlayer2;
    public GameObject Vid1;
    public GameObject Vid2;

    public Text text;

    private bool firstVideoPlayed = false;

    // Use this for initialization
    void Start () {

        text.enabled = false;
        ClipArray = Resources.LoadAll<VideoClip>("IntroVideos");
        Vid1.GetComponent<Renderer>().material.mainTexture = videoPlayer1.texture; 
        Vid2.GetComponent<Renderer>().material.mainTexture = videoPlayer2.texture;


        videoPlayer1.clip = ClipArray[0];
        videoPlayer2.clip = ClipArray[1];

        Vid2.SetActive(false);

        StartCoroutine(PlayFirstClip());
    }

    private IEnumerator PlayFirstClip()
    {
        if (!videoPlayer1.isPlaying)
        {
            videoPlayer1.Play();
            yield return new WaitForSeconds(3.5f);

            StartCoroutine(PlaySecondClip());
        }
    }

    private IEnumerator PlaySecondClip()
    {
        firstVideoPlayed = true;
        Vid2.SetActive(true);
        Vid1.SetActive(false);
        text.enabled = true;

        if (!videoPlayer2.isPlaying)
        {
            videoPlayer2.Play();
            yield return new WaitForSeconds(10.0f);

            SceneManager.LoadScene("MainMenu");
        }
    }

    // Update is called once per frame
    void Update () {

        if (firstVideoPlayed)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
	}
}
