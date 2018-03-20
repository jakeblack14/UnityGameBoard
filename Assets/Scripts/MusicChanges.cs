using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MusicChanges : MonoBehaviour {

    AudioSource Music;
    public Button MyButton;
    public Slider slider;
    public Text MyText;
    
	// Use this for initialization
	void Start () {
        Button btn = MyButton.GetComponent<Button>();
        Text text = MyText.GetComponent<Text>();
        btn.onClick.AddListener(TaskOnClick);
        Music = GetComponent<AudioSource>();
    }

    void TaskOnClick()
    {
        if(Music.mute == false)
        {
            Music.mute = true;
            MyText.text = "Off";
        }
        else
        {
            Music.mute = false;
            MyText.text = "On";
        }
    }

   public void VolumeController()
    {
        Music.volume = slider.value;
    }
}
