using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public Sprite[] s1;
    public Image I1;
    public Image Next;
    public Image Back;
    public string[] ScreenText;
    public Text TextBox;

    static int count = 0;
    // Drag your second sprite here

    void Start()
    {
        if (count == 0)
        {
            Back.enabled = false;
        }

        ScreenText = new string[5];
        ScreenText[0] = "Whether the scrollbar should automatically be hidden when it isn’t needed, and optionally expand the viewport as well.";
        ScreenText[1] = "TESTIONG ALL THEXSTFADKJFHSDKJF KJHF sdkfjh KJFHsk kjsdf";
        ScreenText[2] = "STILL TESTING THE MESS OUT OF TEXT.";
        ScreenText[3] = "PUTTING THE MESS INTO THIS TEXT";
        ScreenText[4] = "PUTTING THE MESS INTO THIS TEXT";
        s1 = Resources.LoadAll<Sprite>("Sprites");

        TextBox.text = ScreenText[0];

        I1.sprite = s1[count];
    }

    public void On_Click_Button()
    {
        if (count != s1.Length - 1)
        {
            count++;
            I1.sprite = s1[count];
            TextBox.text = ScreenText[count];
            Back.enabled = true;
        }
        

        if(count == s1.Length - 1)
        {
            Next.enabled = false;
        }
    }
    public void On_Back_Click_Button()
    {
        if (count != 0)
        {
            count--;
            I1.sprite = s1[count];
            TextBox.text = ScreenText[count];
            Next.enabled = true;
        }

        if (count == 0)
        {
            Back.enabled = false;
        }
            
    } 
}
