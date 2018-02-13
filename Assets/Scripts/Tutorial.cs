using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public Sprite[] s1;
    public Button b1;
    public Image I1;

    int count = 0;
    // Drag your second sprite here

    void Start()
    {
        s1 = Resources.LoadAll<Sprite>("Sprites");
    }

    public void On_Click_Button()
    {
        count++;
        I1.sprite = s1[count];
    }
}
