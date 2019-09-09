using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFX_Slider : MonoBehaviour
{
    public AK.Wwise.RTPC Menuslider_SFX;
    public float sfxSliderValue, musicSliderValue;
    // Start is called before the first frame update
    void Start()
    {
        // SetRTPCValue(wwise name, float from slider)
        // Load float from slider, and place it in the float slot of SetRTPCValue.
        AKRESULT effectSound = AkSoundEngine.SetRTPCValue("Menuslider_SoundFX", 70);
        AKRESULT musicSound = AkSoundEngine.SetRTPCValue("Menuslider_Music", 70);
    }

    // Update is called once per frame
    void Update()
    {
        
        sfxSliderValue = GetComponent<Slider>().value;
        musicSliderValue = GetComponent<Slider>().value;
        if(gameObject.name == "SFX_Slider")
        {
            AkSoundEngine.SetRTPCValue("Menuslider_SoundFX", sfxSliderValue);
        }
        
        else if(gameObject.name == "Music_Slider")
        {
            AkSoundEngine.SetRTPCValue("Menuslider_Music", musicSliderValue);
        }


    }
}

