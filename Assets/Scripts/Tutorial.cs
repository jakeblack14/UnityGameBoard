using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public Sprite[] s1;
    public GameObject BackButton;
    public GameObject NextButton;
    public Image I1;

    static int count = 0;
    // Drag your second sprite here

    void Start()
    {
        if(count == 0)
        {
            BackButton.SetActive(false);
        }
        s1 = Resources.LoadAll<Sprite>("Sprites");
        
    }

    public void On_Click_Button()
    {
       
            count++;
            I1.sprite = s1[count];
            BackButton.SetActive(true);
        

        if(count == s1.Length - 1)
        {
            NextButton.SetActive(false);
        }
    }
    public void On_Back_Click_Button()
    {
      
            count--;
            I1.sprite = s1[count];
            NextButton.SetActive(true);

        if (count == 0)
        {
            BackButton.SetActive(false);
        }
            
    } 
}
