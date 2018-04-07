
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicChanges : MonoBehaviour
{
    public AudioSource source;
    //public AudioSource MenuMusic;
    public Button MyButton;
    public Slider slider;
    private Text text;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }


    void Start()
    {
        MyButton = MyButton.GetComponent<Button>();
        MyButton.onClick.AddListener(TaskOnClick);
        text = MyButton.GetComponentInChildren<Text>();
    }

    public void Update()
    {
        source.volume = slider.value;

        //if(GameBoardData.MusicIsOn)
        //{
        //    source.mute = false;
        //    text.text = "Turn Off";
        //    //slider.value = .5f;
        //}
        //else
        //{
            if (slider.value > 0)
            {
                source.mute = false;
                text.text = "Turn Off";
                GameBoardData.MusicIsOn = true;
            }
            else
            {
                source.mute = true;
                text.text = "Turn On";
                GameBoardData.MusicIsOn = false;
            }
            //slider.value = 0;
       // }
    }

    void TaskOnClick()
    {
        if(GameBoardData.MusicIsOn)
        {
            source.mute = true;
            text.text = "Turn On";
            slider.value = 0;
            GameBoardData.MusicIsOn = false;
        }
        else
        {
            source.mute = false;
            text.text = "Turn Off";
            slider.value = .5f;
            GameBoardData.MusicIsOn = true;
        }
    }
}
