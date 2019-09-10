using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
}
