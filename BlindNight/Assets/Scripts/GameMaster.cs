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
        //Debug.Log("GAME MASTER START");
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
        GameObject MainMenu = FindObjectFromParentName("Canvas", "MainMenu");
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
