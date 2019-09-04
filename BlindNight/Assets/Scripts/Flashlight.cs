using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject volumetricLight;

    void Update()
    {
        Debug.Log(volumetricLight.GetComponent<Material>().name);
        //volumetricLight.GetComponent<Material>().color = gameObject.GetComponent<Light>().color;
        //volumetricLight.GetComponent<Material>() = gameObject.GetComponent<Light>().color;
    }
}
