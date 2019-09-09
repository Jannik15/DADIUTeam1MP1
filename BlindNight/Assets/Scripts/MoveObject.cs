using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [TextArea]
    public string ImportantNote = "Objects must have both a rigid body and a collider to be detected by the system. Also the 'MoveCollisionCheckerObject' prefab must be located in the resources folder!";

    [Tooltip("Should be the length of the 'tiles'")]
    public float moveLenght = 1f;
    [Tooltip("Modifier if collision checker is too small/large (in case of false positives)")]
    public float colliderSizeModifier = 0f;

    bool isColliding = false;
    GameObject moveCollisionChecker;
    GameObject player;
    Collider col;

    // public GameObject objectToMove; // Used for debugging

    void Start()
    {
        // MoveNorth(objectToMove);    // Used for debugging
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void MoveNorth(GameObject obj)
    {
        
        Vector3 direction = new Vector3(obj.transform.position.x + moveLenght, obj.transform.position.y, obj.transform.position.z);
        col = obj.gameObject.GetComponent<Collider>();
        moveCollisionCheckerInit(direction, obj);

        // bool isColliding = moveCollisionChecker.gameObject.GetComponent<TriggerDetection>().IsColliding();
        // Debug.Log(isColliding);

        StartCoroutine(DelayedMoveObject(obj, direction));
    }

    public void MoveSouth(GameObject obj)
    {
        Vector3 direction = new Vector3(obj.transform.position.x - moveLenght, obj.transform.position.y, obj.transform.position.z);
        col = obj.gameObject.GetComponent<Collider>();
        moveCollisionCheckerInit(direction, obj);

        StartCoroutine(DelayedMoveObject(obj, direction));
    }

    public void MoveWest(GameObject obj)
    {
        Vector3 direction = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z + moveLenght);
        col = obj.gameObject.GetComponent<Collider>();
        moveCollisionCheckerInit(direction, obj);

        StartCoroutine(DelayedMoveObject(obj, direction));
    }

    public void MoveEast(GameObject obj)
    {
        Vector3 direction = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z - moveLenght);
        col = obj.gameObject.GetComponent<Collider>();
        moveCollisionCheckerInit(direction, obj);

        StartCoroutine(DelayedMoveObject(obj, direction));
    }

    private void moveCollisionCheckerInit(Vector3 dir, GameObject obj)
    {
        
        moveCollisionChecker = Instantiate(Resources.Load("Prefabs/MoveCollisionCheckerObject", typeof(GameObject)) as GameObject);
        moveCollisionChecker.transform.position = obj.transform.position;
        moveCollisionChecker.transform.localScale = obj.transform.localScale + (new Vector3(colliderSizeModifier, colliderSizeModifier, colliderSizeModifier));

        moveCollisionChecker.transform.position = dir;

    }

    IEnumerator DelayedMoveObject(GameObject obj, Vector3 direction)    // Coroutine
    {
        yield return new WaitForSeconds(0.3f);

        isColliding = moveCollisionChecker.gameObject.GetComponent<TriggerDetection>().IsColliding();

        if (isColliding == false)
        {
            // Vector3.Lerp(obj.transform.position, moveCollisionChecker.transform.position, 2f);   // Must be in update loop
            obj.transform.position = direction;
            Destroy(moveCollisionChecker);
        }
        else
        {
            Debug.Log("Space occupied, cannot move object!");
            isColliding = false;
            Destroy(moveCollisionChecker);
        }

    }
}
