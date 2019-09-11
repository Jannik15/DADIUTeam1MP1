using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVLoader : MonoBehaviour
{
    public static CSVLoader instance;

    private TextAsset csvFile;
    private char lineSeperator = '\n';
    private char fieldSeperator = ';';

    private string[] lines;

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

    public void Awake()
    {
        CreateCSVLoader();
    }

    void Start()
    {
        LoadCSV();
    }

    void CreateCSVLoader()
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
}
