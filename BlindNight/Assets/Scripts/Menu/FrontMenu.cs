using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontMenu : MonoBehaviour
{
    private bool reloadedOnFirstFrame = false;

    void Update()
    {
        if (!reloadedOnFirstFrame)
        {
            GameMaster.instance.ShowFrontMenu(false);
            GameMaster.instance.ShowFrontMenu(true);
            reloadedOnFirstFrame = true;
        }
    }

    public void ShowOptions()
    {
        GameMaster.instance.ShowFrontMenu(false);
        GameMaster.instance.ShowOptionsMenu(true);
    }

    public void ShowFrontMenu()
    {
        GameMaster.instance.ShowOptionsMenu(false);
        GameMaster.instance.ShowFrontMenu(true);
    }

    public void StartGame()
    {
        GameMaster.instance.ShowFrontMenu(false);
        GameMaster.instance.ShowOptionsMenu(false);
    }

    public void PlayBtnSound()
    {
        AkSoundEngine.PostEvent("Play_Button3_Click", gameObject);
    }

    public void SetLanguage(string language)
    {
        GameMaster.instance.SetLanguage(language);
        GameMaster.instance.ShowOptionsMenu(false);
        GameMaster.instance.ShowOptionsMenu(true);
    }

    public void TranslateThis()
    {
        //Debug.Log("TRANSLATE THIS");
    }
}
