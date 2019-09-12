using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsSideChecker : MonoBehaviour
{
    // public GameObject testObject; // ONLY used for debugging
    [Tooltip("Depends on scaling of objects. This value should be 0")]
    public float scalerValue = 0.2f;
    public float distanceToDetection = 2.4f;

    GameObject player;
    GameObject tempIndicatorObject;

    List<GameObject> indicatorObjectObj;
    List<Vector3> indicatorObjectsLoc;
    int indicatorListIndex = 0;

    bool[] m_IsHorizontal;
    Vector3 relativePosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        m_IsHorizontal = new bool[2] {false,false};
        indicatorObjectsLoc = new List<Vector3>();
        indicatorObjectObj = new List<GameObject>();
    }

    void Update()
    {
        // bool[] test = IsHorizontal(testObject);
        // Debug.DrawRay(player.gameObject.transform.position, testObject.gameObject.transform.position - player.gameObject.transform.position, Color.green);
        // Vector3 playerToObjectDirection = testObject.gameObject.transform.position - player.gameObject.transform.position;
        // Debug.Log(playerToObjectDirection);

        // InteractionIndicatorChecker();
        
    }

    void InteractionIndicatorChecker()
    {
        RaycastHit hit1;
        RaycastHit hit2;

        if (Physics.Raycast(player.transform.position, player.transform.right, out hit1, distanceToDetection) || Physics.Raycast(player.transform.position, player.transform.forward, out hit1, distanceToDetection))
        {
            if (hit1.collider.gameObject.tag == "interactable")
            {
                if (!indicatorObjectsLoc.Contains(hit1.collider.gameObject.transform.position))
                {
                    tempIndicatorObject = Instantiate(Resources.Load("Prefabs/InteractionIndicator", typeof(GameObject)) as GameObject);
                    tempIndicatorObject.transform.position = hit1.collider.gameObject.transform.position;
                    indicatorObjectsLoc.Add(tempIndicatorObject.transform.position);
                    indicatorObjectObj.Add(tempIndicatorObject);
                    Debug.Log("Added object to list!!");
                }
            }

        }

        foreach (var item in indicatorObjectsLoc)
        {
            Physics.Raycast(player.transform.position, player.transform.right, out hit1, distanceToDetection);
            Physics.Raycast(player.transform.position, player.transform.forward, out hit2, distanceToDetection);

            if (item == hit1.collider.gameObject.transform.position || item == hit2.collider.gameObject.transform.position)
            {
                Debug.Log("The object got hit");
            } else
            {
                Debug.Log("Destroyed from list!");
                indicatorObjectsLoc.Remove(item);
                tempIndicatorObject = indicatorObjectObj[indicatorListIndex];
                indicatorObjectObj.Remove(indicatorObjectObj[indicatorListIndex]);
                Destroy(indicatorObjectObj[indicatorListIndex]);
            }

            indicatorListIndex++;

        }

        indicatorListIndex = 0;
    }


    public bool[] IsHorizontal (GameObject thisObject)
    {
        if (thisObject.GetComponent<Collider>())
        {
            // Vector3 objectSize = thisObject.GetComponent<Collider>().bounds.size;
            // Debug.Log(objectSize.x + " " + objectSize.y + " " + objectSize.z);

            Vector3 playerToObjectDirection = thisObject.gameObject.transform.position - player.gameObject.transform.position;  // Calculates the direction vector
            relativePosition = new Vector3(Mathf.Abs(playerToObjectDirection.x), 0, Mathf.Abs(playerToObjectDirection.z));
            
            if (relativePosition.x <= 0.6f + scalerValue && relativePosition.z >= 0.2f + scalerValue)
            {
                if (relativePosition.z <= distanceToDetection + scalerValue)
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
            else if (relativePosition.x >= 0.2f + scalerValue && relativePosition.z <= 0.6f + scalerValue)
            {
                if (relativePosition.x <= distanceToDetection + scalerValue)
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
