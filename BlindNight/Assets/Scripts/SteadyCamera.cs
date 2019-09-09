using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteadyCamera : MonoBehaviour
{
    public GameObject target;

    private Vector3 offset;

    void Start()
    {
        offset = new Vector3(-22, 17, -22);
    }

    void Update()
    {
        transform.position = target.transform.position + offset;
    }
}
