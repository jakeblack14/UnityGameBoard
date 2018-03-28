//using UnityEngine;
//using System.Collections;

///// <summary>
///// This script is in charge of playing music in the game
///// </summary>
//public class MusicChanges : MonoBehaviour
//{
//    /// <summary>
//    /// The clip to play in a menu.
//    /// This field is private because it's not designed to be directly
//    /// modified by other scripts, and tagged with [SerializeField] so that
//    /// you can still modify it using the Inspector and so that Unity
//    /// saves its value.
//    /// </summary>
//    [SerializeField]
//    private AudioClip MenuMusic;

//    /// <summary>
//    /// The clip to play outside menus.
//    /// </summary>
//    [SerializeField]
//    private AudioClip AsteroidMusic;

//    [SerializeField]
//    private AudioClip MilkyMusic;

//    [SerializeField]
//    /// <summary>
//    /// The component that plays the music
//    /// </summary>
//    private AudioSource source;

//    /// <summary>
//    /// This class follows the singleton pattern and this is its instance
//    /// </summary>
//    static private MusicChanges instance;

//    /// <summary>
//    /// Awake is not public because other scripts have no reason to call it directly,
//    /// only the Unity runtime does (and it can call protected and private methods).
//    /// It is protected virtual so that possible subclasses may perform more specific
//    /// tasks in their own Awake and still call this base method (It's like constructors
//    /// in object-oriented languages but compatible with Unity's component-based stuff.
//    /// </summary>
//    protected virtual void Awake()
//    {
//        // Singleton enforcement
//        if (instance == null)
//        {
//            // Register as singleton if first
//            instance = this;
//            instance.source.clip = MenuMusic;
//            // DontDestroyOnLoad(instance);
//        }
//        else
//        {
//            // Self-destruct if another instance exists
//            Destroy(this);
//            return;
//        }
//    }

//    protected virtual void Start()
//    {
//        // If the game starts in a menu scene, play the appropriate music
//        if(source.clip == MenuMusic)
//        {
//            PlayMilkyMusic();
//        }
//        if(source.clip == AsteroidMusic)
//        {
//            PlayAsteroidMusic();
//        }
//        if(source.clip == MilkyMusic )
//        {
//            PlayMilkyMusic();
//        }
//    }

//    /// <summary>
//    /// Plays the music designed for the menus
//    /// This method is static so that it can be called from anywhere in the code.
//    /// </summary>
//    static public void PlayMenuMusic()
//    {
//            instance.source.Stop();
//            instance.source.clip = instance.MenuMusic;
//            instance.source.Play();
//    }

//    /// <summary>
//    /// Plays the music designed for outside menus
//    /// This method is static so that it can be called from anywhere in the code.
//    /// </summary>
//    static public void PlayAsteroidMusic()
//    {    
//                instance.source.Stop();
//                instance.source.clip = instance.AsteroidMusic;
//                instance.source.Play();
//    }

//    static public void PlayMilkyMusic()
//    {

//                instance.source.Stop();
//                instance.source.clip = instance.MilkyMusic;
//                instance.source.PlayOneShot();

//    }
//}











using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicChanges : MonoBehaviour
{
    public AudioSource source;
    public Button MyButton;
    public Slider slider;


    void Start()
    {
        Button btn = MyButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        source = GetComponent<AudioSource>();
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
