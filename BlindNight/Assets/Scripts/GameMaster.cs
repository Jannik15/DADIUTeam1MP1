using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    private int walkType = 4;
    private bool canPlay = false;
    private bool menuUp = true;
    private bool frontMenuUp = true;
    private string lang = "EN";

    private int musicLevel = 70;
    private int sfxLevel = 70;

    private TextAsset csvFile;
    private char lineSeperator = '\n';
    private char fieldSeperator = ';';

    private string[] lines;

    public void Awake()
    {
        CreateGameMaster();
    }

    void Start()
    {
        LoadCSV();
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
        TranslateMenu();
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

        if (menuUp)
        {
            frontMenuUp = !FindObjectFromParentObject(Menu, "Options").activeSelf;
            //TranslateMenu();
        }
    }

    public void TranslateMenu()
    {
        if (frontMenuUp)
        {
            TranslateMenuFront();
        } else
        {
            TranslateMenuOptions();
        }
    }

    public void TranslateMenuFront()
    {
        Debug.Log("Translate front menu");

        GameObject Menu = FindObjectFromParentName("Canvas", "Menu");
        if (!Menu) return;

        GameObject FrontMenu = FindObjectFromParentObject(Menu, "FrontMenu");
        if (!FrontMenu) return;
    }

    public void TranslateMenuOptions()
    {
        Debug.Log("Translate options");

        GameObject Menu = FindObjectFromParentName("Canvas", "Menu");
        if (!Menu) return;

        GameObject OptionsMenu = FindObjectFromParentObject(Menu, "Options");
        if (!OptionsMenu) return;

        OptionsMenu.GetComponentInChildren<Text>();
        Debug.Log(OptionsMenu);

        /*GameObject TutorialText01 = FindObjectFromParentObject(OptionsMenu, "hold and drag");
        if (TutorialText01)
        {
            Debug.Log("Changing text");
            //TutorialText01.GetComponent<Text>().text = "hello";
            //TutorialText01.Text = "yo";
            //TutorialText01.SetActive(false);
        } else
        {
            Debug.Log("Tutorial text does not exist");
        }*/
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

    public void LoadCSV()
    {
        csvFile = Resources.Load<TextAsset>("languageFile");
        lines = csvFile.text.Split(lineSeperator);

        Debug.Log(GetStringFromKey("sumtin"));
    }

    public string GetStringFromKey(string key)
    {
        int index = 0;

        switch (GameMaster.instance.GetLanguage())
        {
            case "EN":
                index = 1;
                break;
            case "DK":
                index = 2;
                break;
            default:
                return key;
        }

        for (int i = 0; i < lines.Length; i++)
        {
            string[] txt = lines[i].Split(fieldSeperator);
            if (txt[0] == key)
            {
                return txt[index];
            }
        }
        return key;
    }
}
