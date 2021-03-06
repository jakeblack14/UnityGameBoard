﻿using System.Collections;
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
        ScreenText[0] = "To make a move, Begin by clicking on the piece you would like to move. The piece will highlight when you have correctly clicked your piece. Click an open square either: Above, Diagonally Left, or Diagonally Right. ";
        ScreenText[1] = "Feel Free to change your choice after you have clicked on one of your pieces by clicking somewhere else or on your new piece twice.";
        ScreenText[2] = "To take an enemy piece move your piece diagonally. You may not take a piece vertically.";
        ScreenText[3] = "To win the game move one of your pieces to the other side of the game board on the enemies' side";
        //////////////////////////////////////////////////////////////////////////////////////////////////////


        // All of the Texts for the Help Menu Title Box
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        TitleText = new string[5];
        TitleText[0] = "Make Move"; 
        TitleText[1] = "Change Move";
        TitleText[2] = "Take Pieces";
        TitleText[3] = "Win Game";
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
