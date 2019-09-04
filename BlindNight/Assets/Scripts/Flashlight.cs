using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    Renderer volumetricLight;
    public Color lightColor;
    private void Start()
    {
        volumetricLight = GetComponentInChildren<Renderer>();
        lightColor = GetComponent<Light>().color;
    }

    void Update()
    {
        GetComponent<Light>().color = lightColor;
        volumetricLight.material.color = GetComponent<Light>().color; // Main color
        volumetricLight.material.SetColor("_EmissionColor", GetComponent<Light>().color); // Emissive color
    }
}
