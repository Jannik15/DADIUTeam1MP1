using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    public int walkType = 0;

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
        } else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetWalkType(int type)
    {
        walkType = type;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject filenamefld = null;
            Transform[] trans = GameObject.Find("MainCanvas").GetComponentsInChildren<Transform>(true);
            foreach (Transform t in trans)
            {
                if (t.gameObject.name == "MainMenu")
                {
                    filenamefld = t.gameObject;
                    filenamefld.SetActive(!filenamefld.gameObject.activeSelf);
                }
            }
        }
    }
}
