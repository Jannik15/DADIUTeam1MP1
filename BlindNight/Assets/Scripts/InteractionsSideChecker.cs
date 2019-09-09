﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsSideChecker : MonoBehaviour
{
    // public GameObject testObject; // ONLY used for debugging
    GameObject player;

    bool[] m_IsHorizontal;
    Vector3 relativePosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        m_IsHorizontal = new bool[2] {false,false};

    }

    void Update()
    {
        // bool[] test = IsHorizontal(testObject);
        // Debug.DrawRay(player.gameObject.transform.position, testObject.gameObject.transform.position - player.gameObject.transform.position, Color.green);
        // Vector3 playerToObjectDirection = testObject.gameObject.transform.position - player.gameObject.transform.position;

    }

    public bool[] IsHorizontal (GameObject thisObject)
    {
        if (thisObject.GetComponent<Collider>())
        {
            Vector3 objectSize = thisObject.GetComponent<Collider>().bounds.size;
            // Debug.Log(objectSize.x + " " + objectSize.y + " " + objectSize.z);

            Vector3 playerToObjectDirection = thisObject.gameObject.transform.position - player.gameObject.transform.position;  // Calculates the direction vector
            relativePosition = new Vector3(Mathf.Abs(playerToObjectDirection.x), 0, Mathf.Abs(playerToObjectDirection.z));
            
            if (relativePosition.x < 1f && relativePosition.z > 1f)
            {
                if (relativePosition.z < 2.2f)
                {
                    m_IsHorizontal[0] = false;
                    m_IsHorizontal[1] = true;
                    // Debug.Log("Vertical Axis");
                }
                else
                {
                    m_IsHorizontal[1] = false;
                    // Debug.Log("Out of Bounds");
                }
            }
            else if (relativePosition.x > 1f && relativePosition.z < 1f)
            {
                if (relativePosition.x < 2.2f)
                {
                    // Debug.Log("Horizontal Axis");
                    m_IsHorizontal[0] = true;
                    m_IsHorizontal[1] = true;
                }
                else
                {
                    m_IsHorizontal[1] = false;
                    // Debug.Log("Out of Bounds");
                }
            }
            else
            {
                // Debug.Log("Out of bounds");
                m_IsHorizontal[1] = false;
            }

            // Debug.Log(relativePosition.x + " " + relativePosition.y + " " + relativePosition.z);
            // Debug.DrawRay(player.gameObject.transform.position, thisObject.gameObject.transform.position - player.gameObject.transform.position, Color.green);
        }
        else
        {
            Debug.Log("Interaction Error: This object is not interactable or no collider attached");

        }

        return m_IsHorizontal;
    }

}