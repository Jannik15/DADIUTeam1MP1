using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Translater : MonoBehaviour
{
    private TextMeshProUGUI text;
    private string key;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        key = text.text;
        Debug.Log("KEY: ");
        Debug.Log(key);
    }

    private void OnEnable()
    {
        Debug.Log("Sumtin");
        GetComponent<TextMeshProUGUI>().text = GameMaster.instance.GetStringFromKey(key);
    }
}
