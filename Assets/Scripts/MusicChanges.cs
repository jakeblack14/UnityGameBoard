
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicChanges : MonoBehaviour
{
    public AudioSource source;
    public AudioSource MenuMusic;
    public Button MyButton;
    public Slider slider;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }


    void Start()
    {
        MyButton = MyButton.GetComponent<Button>();
        MyButton.onClick.AddListener(TaskOnClick);
    }

    public void Update()
    {
        source.volume = slider.value;
    }

    void TaskOnClick()
    {
        Text text = MyButton.GetComponentInChildren<Text>();
        if (source.mute == false)
        {
            source.mute = true;
            text.text = "Off";
            slider.value = 0;
            
        }
        else
        {
            source.mute = false;
            text.text = "On";
            slider.value = .5f;
        }
    }
}
