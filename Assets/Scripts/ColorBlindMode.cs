using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class ColorBlindMode : MonoBehaviour {

    public PostProcessingProfile colorBlindModeProfile;
    private bool inColorBlindMode = false;

    public void SetHue()
    {
        var HueShiftValue = colorBlindModeProfile.colorGrading.settings;

        if(inColorBlindMode)
        {
            HueShiftValue.basic.hueShift = 0;
            colorBlindModeProfile.colorGrading.settings = HueShiftValue;
            inColorBlindMode = false;
        }
        else
        {
            HueShiftValue.basic.hueShift = -89;
            colorBlindModeProfile.colorGrading.settings = HueShiftValue;
            inColorBlindMode = true;
        }
    }
}
