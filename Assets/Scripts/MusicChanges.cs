
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
        Button btn = MyButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    public void Update()
    {
        source.volume = slider.value;
    }

    void TaskOnClick()
    {
        if (source.mute == false)
        {
            source.mute = true;
        }
        else
        {
            source.mute = false;
        }
    }
}
