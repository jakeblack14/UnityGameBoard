using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Tutorial : MonoBehaviour {

    public VideoClip[] ClipArray;
    public VideoPlayer videoPlayer;
    public GameObject Vid;
    public Image Next;
    public Image Back;
    public string[] ScreenText;
    public string[] TitleText;
    public Text TextBox;
    public Text TitleBox;

    static int count = 0;
    // Drag your second sprite here

    void Start()
    {
        if (count == 0)
        {
            Back.enabled = false;
        }

        
        
        // All of the Texts for the Help Menu Text Box
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        ScreenText = new string[5];
        ScreenText[0] = "Welcome to the Help Menu. Click the Large Question Mark at any point to get some Help!";
        ScreenText[1] = "To take a peice click on the piece you want to move. In order to take an enemy piece, your piece must be able to move diagonally left or diagonally right onto" +
            "an enemy piece.";
        ScreenText[2] = "To win the game either take all the enemy pieces or get to the very end of the board on the enemies side.";
        ScreenText[3] = "Teach the user how to do other things";
        ScreenText[4] = "teach the user how to use the settings page at any point";
        //////////////////////////////////////////////////////////////////////////////////////////////////////


        // All of the Texts for the Help Menu Title Box
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        TitleText = new string[5];
        TitleText[0] = "Help Menu";
        TitleText[1] = "Make Move";
        TitleText[2] = "Take Peices";
        TitleText[3] = "Other";
        TitleText[4] = "Other1";
        //////////////////////////////////////////////////////////////////////////////////////////////////////
        videoPlayer = Vid.GetComponent<VideoPlayer>();
        //videoPlayer.source = VideoSource.VideoClip;
        ClipArray = Resources.LoadAll<VideoClip>("Sprites");
        Vid.GetComponent<Renderer>().material.mainTexture = videoPlayer.texture; // THIS WAS THE PROBLEM!!! I HATE TEXTURES
        videoPlayer.clip = ClipArray[0];
        videoPlayer.Play();
        Debug.Log("Playing Video");

        TextBox.text = ScreenText[0];
        TitleBox.text = TitleText[0];

        

        
       
    }

    public void On_Click_Button()
    {
        if (count != ClipArray.Length - 1)
        {
            count++;
            videoPlayer.clip = ClipArray[count];
            videoPlayer.Play();
            TextBox.text = ScreenText[count];
            TitleBox.text = TitleText[count];
            Back.enabled = true;
        }
        

        if(count == ClipArray.Length - 1)
        {
            Next.enabled = false;
        }
    }
    public void On_Back_Click_Button()
    {
        if (count != 0)
        {
            count--;
            videoPlayer.clip = ClipArray[count];
            videoPlayer.Play();
            TextBox.text = ScreenText[count];
            TitleBox.text = TitleText[count];
            Next.enabled = true;
        }

        if (count == 0)
        {
            Back.enabled = false;
        }
            
    } 
}
