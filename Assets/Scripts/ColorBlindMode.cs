using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;

public class ColorBlindMode : MonoBehaviour {

    public PostProcessingProfile colorBlindModeProfile;
    private bool inColorBlindMode = false;
    public Text buttonText;

    public void SetHue()
    {
        var HueShiftValue = colorBlindModeProfile.colorGrading.settings;

        if(inColorBlindMode)
        {
            HueShiftValue.basic.hueShift = 0;
            colorBlindModeProfile.colorGrading.settings = HueShiftValue;
            inColorBlindMode = false;
            buttonText.text = "Off";
        }
        else
        {
            HueShiftValue.basic.hueShift = -89;
            colorBlindModeProfile.colorGrading.settings = HueShiftValue;
            inColorBlindMode = true;
            buttonText.text = "On";
        }
    }
}
