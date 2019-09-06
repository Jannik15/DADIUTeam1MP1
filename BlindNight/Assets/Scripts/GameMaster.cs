using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    private int walkType = 4;

    public void Awake()
    {
        CreateGameMaster();
    }

    void Start()
    {

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

    public void SetWalkType(int type)
    {
        walkType = type;
        ToggleMenu();
    }

    public int GetWalkType()
    {
        return walkType;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        GameObject MainMenu = FindChildInParentString("Canvas", "MainMenu");
        MainMenu.SetActive(!MainMenu.activeSelf);

        GameObject Joystick = FindChildInParentString("Canvas", "Joystick");
        Joystick.SetActive(!Joystick.activeSelf);
    }

    public GameObject FindChildInParentString(string parent, string child)
    {
        GameObject result = null;
        Transform[] trans = GameObject.Find(parent).GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trans)
        {
            if (t.gameObject.name == child)
            {
                result = t.gameObject;
            }
        }
        return result;
    }

    public GameObject FindChildInParentObject(GameObject parent, string child)
    {
        GameObject result = null;
        Transform[] trans = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trans)
        {
            if (t.gameObject.name == child)
            {
                result = t.gameObject;
            }
        }
        return result;
    }
}
