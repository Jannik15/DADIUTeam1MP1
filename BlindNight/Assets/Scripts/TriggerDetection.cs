using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
    // GameObject tempCallerGameObject;
    Collider callerCol;
    bool isColliding = false;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag != "player" && callerCol != col)
        {
            isColliding = true;
            // Debug.Log(isColliding);
        }

    }

    public void SetCallerCol(Collider caller)
    {
        callerCol = caller;
    }

    public bool IsColliding()
    {
        return isColliding;
    }
}
