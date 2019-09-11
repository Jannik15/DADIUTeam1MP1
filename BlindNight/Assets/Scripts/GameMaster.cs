using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    private int walkType = 4;
    private bool canPlay = false;
    private bool menuUp = true;
    private string lang = "EN";

    private int musicLevel = 70;
    private int sfxLevel = 70;

    public void Awake()
    {
        CreateGameMaster();
    }

    void Start()
    {
        if (menuUp)
        {
            ShowFrontMenu(true);
        }
    }

    void CreateGameMaster()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetMusicLevel()
    {
        return musicLevel;
    }

    public void SetMusicLevel(int music_level)
    {
        musicLevel = music_level;
    }

    public int GetSFXLevel()
    {
        return sfxLevel;
    }

    public void SetSFXLevel(int sfx_level)
    {
        sfxLevel = sfx_level;
    }

    public void SetWalkType(int type)
    {
        walkType = type;
        ToggleMenu();
    }

    public int GetWalkType()
    {
        return walkType;
    }

    public void SetCanPlay(bool value)
    {
        canPlay = value;
    }

    public bool GetCanPlay()
    {
        return canPlay;
    }

    public void SetLanguage(string language)
    {
        lang = language;
        Debug.Log("Language set to " + lang);
    }

    public string GetLanguage()
    {
        return lang;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowOptionsMenu(false);
            if (menuUp)
            {
                ShowFrontMenu(false);
            }
            else
            {
                ShowFrontMenu(!menuUp);
            }
        }
    }

    public void ShowMenu(bool show)
    {
        menuUp = show;
        GameObject Menu = FindObjectFromParentName("Canvas", "Menu");
        Menu.SetActive(show);
        GameObject SettingsBtn = FindObjectFromParentName("Canvas", "SettingsBtn");
        SettingsBtn.SetActive(!show);
        canPlay = !menuUp;
    }

    public void ShowFrontMenu(bool show)
    {
        ShowMenu(show);
        GameObject Menu = FindObjectFromParentName("Canvas", "Menu");
        GameObject FrontMenu = FindObjectFromParentObject(Menu, "FrontMenu");
        FrontMenu.SetActive(show);
    }

    public void ShowOptionsMenu(bool show)
    {
        ShowMenu(show);
        GameObject Menu = FindObjectFromParentName("Canvas", "Menu");
        GameObject OptionsMenu = FindObjectFromParentObject(Menu, "Options");
        OptionsMenu.SetActive(show);
    }

    public void ToggleMenu()
    {
        //GameObject MainMenu = FindObjectFromParentName("Canvas", "MainMenu");
        GameObject MainMenu = FindObjectFromParentName("Canvas", "FrontMenu");
        MainMenu.SetActive(!MainMenu.gameObject.activeSelf);
    }

    public GameObject FindObjectFromParentName(string parent, string targetName)
    {
        GameObject obj = null;
        Transform[] objChildren = GameObject.Find(parent).GetComponentsInChildren<Transform>(true);
        foreach (Transform child in objChildren)
        {
            if (child.gameObject.name == targetName)
            {
                obj = child.gameObject;
            }
        }
        return obj;
    }

    public GameObject FindObjectFromParentObject(GameObject parent, string targetName)
    {
        GameObject obj = null;
        Transform[] objChildren = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in objChildren)
        {
            if (child.gameObject.name == targetName)
            {
                obj = child.gameObject;
            }
        }
        return obj;
    }
}
