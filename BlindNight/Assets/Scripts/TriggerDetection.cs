using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetection : MonoBehaviour
{

    bool isColliding = false;

    private void OnTriggerEnter(Collider col)
    {
        isColliding = true;
        Debug.Log(isColliding);
    }

    public bool IsColliding()
    {
        return isColliding;
    }
}
