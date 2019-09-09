using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    Renderer volumetricLight;
    private Color lightColor;
    public Color lightColor1;
    public Color lightColor2;
    private void Start()
    {
        volumetricLight = GetComponentInChildren<Renderer>();
        lightColor = lightColor1;
    }

    void Update()
    {
        GetComponent<Light>().color = lightColor;
        volumetricLight.material.color = GetComponent<Light>().color; // Main color
        volumetricLight.material.SetColor("_EmissionColor", GetComponent<Light>().color); // Emissive color
    }

    public void ChangeColor()
    {
        if (lightColor == lightColor1)
            lightColor = lightColor2;
        else
            lightColor = lightColor1;
    }
}
