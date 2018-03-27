using UnityEngine;
using System.Collections;

/// <summary>
/// This script is in charge of playing music in the game
/// </summary>
public class MusicChanges : MonoBehaviour
{
    /// <summary>
    /// The clip to play in a menu.
    /// This field is private because it's not designed to be directly
    /// modified by other scripts, and tagged with [SerializeField] so that
    /// you can still modify it using the Inspector and so that Unity
    /// saves its value.
    /// </summary>
    [SerializeField]
    private AudioClip MenuMusic;

    /// <summary>
    /// The clip to play outside menus.
    /// </summary>
    [SerializeField]
    private AudioClip AsteroidMusic;

    [SerializeField]
    private AudioClip MilkyMusic;

    [SerializeField]
    /// <summary>
    /// The component that plays the music
    /// </summary>
    private AudioSource source;

    /// <summary>
    /// This class follows the singleton pattern and this is its instance
    /// </summary>
    static private MusicChanges instance;

    /// <summary>
    /// Awake is not public because other scripts have no reason to call it directly,
    /// only the Unity runtime does (and it can call protected and private methods).
    /// It is protected virtual so that possible subclasses may perform more specific
    /// tasks in their own Awake and still call this base method (It's like constructors
    /// in object-oriented languages but compatible with Unity's component-based stuff.
    /// </summary>
    protected virtual void Awake()
    {
        // Singleton enforcement
        if (instance == null)
        {
            // Register as singleton if first
            instance.source = source;
            DontDestroyOnLoad(instance);
        }
        else
        {
            // Self-destruct if another instance exists
            Destroy(this);
            return;
        }
    }

    protected virtual void Start()
    {
        // If the game starts in a menu scene, play the appropriate music
        if(source.clip == MenuMusic)
        {
            PlayAsteroidMusic();
        }
    }

    /// <summary>
    /// Plays the music designed for the menus
    /// This method is static so that it can be called from anywhere in the code.
    /// </summary>
    static public void PlayMenuMusic()
    {
        if (instance != null)
        {
            if (instance.source != null)
            {
                instance.source.Stop();
                instance.source.clip = instance.MenuMusic;
                instance.source.Play();
            }
        }
        else
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }

    /// <summary>
    /// Plays the music designed for outside menus
    /// This method is static so that it can be called from anywhere in the code.
    /// </summary>
    static public void PlayAsteroidMusic()
    {
        if (instance != null)
        {
            if (instance.source != null)
            {
                instance.source.Stop();
                instance.source.clip = instance.AsteroidMusic;
                instance.source.Play();
            }
        }
        else
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }

    static public void PlayMilkyMusic()
    {
        if (instance != null)
        {
            if (instance.source != null)
            {
                instance.source.Stop();
                instance.source.clip = instance.MilkyMusic;
                instance.source.Play();
            }
        }
        else
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }
}











//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;

//public class MusicChanges : MonoBehaviour {

//    public AudioSource MenuMusic;
//    public AudioSource AsteroidMusic;
//    public AudioSource MilkyMusic;
//    public AudioSource source;
//    public Button MyButton;
//    public Slider slider;

//	// Use this for initialization
//	void Start () {
//        Button btn = MyButton.GetComponent<Button>();
//        btn.onClick.AddListener(TaskOnClick);
//        source = GetComponent<AudioSource>();

//        if(source == MenuMusic)
//        {
//            DestroyObject(source);
//        }

//        if (source == AsteroidMusic)
//        {

//        }

//        if (source == MilkyMusic)
//        {

//        }

//    }

//    public void Update()
//    {
//        source.volume = slider.value;
//    }

//    void TaskOnClick()
//    {
//        if(Music.mute == false)
//        {
//            Music.mute = true;
//        }
//        else
//        {
//            Music.mute = false;
//        }
//    }

//   public void VolumeController()
//    {
//        Music.volume = slider.value;
//    }
//}
