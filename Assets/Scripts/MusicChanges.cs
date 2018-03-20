using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MusicChanges : MonoBehaviour {

    AudioSource Music;
    public Button MyButton;
    public Slider slider;
    
	// Use this for initialization
	void Start () {
        Button btn = MyButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        Music = GetComponent<AudioSource>();
    }

    public void Update()
    {
        Music.volume = slider.value;
    }

    void TaskOnClick()
    {
        if(Music.mute == false)
        {
            Music.mute = true;
        }
        else
        {
            Music.mute = false;
        }
    }

   public void VolumeController()
    {
        Music.volume = slider.value;
    }
}
