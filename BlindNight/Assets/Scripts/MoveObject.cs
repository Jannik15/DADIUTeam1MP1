using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [TextArea]
    public string ImportantNote = "Objects must have both a rigid body and a collider to be detected by the system. Also the 'MoveCollisionCheckerObject' prefab must be located in the resources folder!";

    [Tooltip("Should be the length of the 'tiles'")]
    public float moveLength = 2f;
    float tempMoveLength;
    [Tooltip("Modifier if collision checker is too small/large (in case of false positives)")]
    public float colliderSizeModifier = -0.2f;

    private float delayTime = 0.15f;

    bool isColliding = false;

    GameObject clickedArrow;
    GameObject moveCollisionChecker;
    GameObject player;

    // public GameObject objectToMove; // Used for debugging

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        tempMoveLength = moveLength;

        // MoveWest(objectToMove);    // Used for debugging
    }

    public void MoveNorth(GameObject obj)
    {
        Vector3 playerToObjectDirection = obj.gameObject.transform.position - player.gameObject.transform.position;  // Calculates the direction vector

        if (Mathf.Sign(playerToObjectDirection.x) == -1)
        {
            tempMoveLength = moveLength * 2;
        }

        Vector3 direction = new Vector3(obj.transform.position.x + tempMoveLength, obj.transform.position.y, obj.transform.position.z);
        Vector3 pDirection = new Vector3(player.transform.position.x + moveLength, player.transform.position.y, player.transform.position.z);
        moveCollisionCheckerInit(direction, obj);

        direction = new Vector3(obj.transform.position.x + moveLength, obj.transform.position.y, obj.transform.position.z);
        StartCoroutine(DelayedMoveObject(obj, direction, pDirection));
    }

    public void MoveSouth(GameObject obj)
    {
        Vector3 playerToObjectDirection = obj.gameObject.transform.position - player.gameObject.transform.position;  // Calculates the direction vector

        if (Mathf.Sign(playerToObjectDirection.x) == +1)
        {
            tempMoveLength = moveLength * 2;
        }

        Vector3 direction = new Vector3(obj.transform.position.x - tempMoveLength, obj.transform.position.y, obj.transform.position.z);
        Vector3 pDirection = new Vector3(player.transform.position.x - moveLength, player.transform.position.y, player.transform.position.z);
        moveCollisionCheckerInit(direction, obj);

        direction = new Vector3(obj.transform.position.x - moveLength, obj.transform.position.y, obj.transform.position.z);
        StartCoroutine(DelayedMoveObject(obj, direction, pDirection));
    }

    public void MoveWest(GameObject obj)
    {
        Vector3 playerToObjectDirection = obj.gameObject.transform.position - player.gameObject.transform.position;  // Calculates the direction vector

        if (Mathf.Sign(playerToObjectDirection.z) == -1)
        {
            tempMoveLength = moveLength * 2;
        }

        Vector3 direction = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z + tempMoveLength);
        Vector3 pDirection = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + moveLength);
        moveCollisionCheckerInit(direction, obj);

        direction = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z + moveLength);
        StartCoroutine(DelayedMoveObject(obj, direction, pDirection));
    }

    public void MoveEast(GameObject obj)
    {
        Vector3 playerToObjectDirection = obj.gameObject.transform.position - player.gameObject.transform.position;  // Calculates the direction vector

        if (Mathf.Sign(playerToObjectDirection.z) == 1)
        {
            tempMoveLength = moveLength * 2;
        }

        Vector3 direction = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z - tempMoveLength);
        Vector3 pDirection = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - moveLength);
        moveCollisionCheckerInit(direction, obj);

        direction = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z - moveLength);
        StartCoroutine(DelayedMoveObject(obj, direction, pDirection));
    }

    private void moveCollisionCheckerInit(Vector3 dir, GameObject obj)
    {
        
        moveCollisionChecker = Instantiate(Resources.Load("Prefabs/MoveCollisionCheckerObject", typeof(GameObject)) as GameObject);
        moveCollisionChecker.gameObject.GetComponent<TriggerDetection>().SetCallerCol(obj.gameObject.GetComponent<Collider>());
        // moveCollisionChecker.transform.position = obj.transform.position;
        // moveCollisionChecker.transform.localScale = obj.transform.localScale + (new Vector3(colliderSizeModifier, colliderSizeModifier, colliderSizeModifier));
        moveCollisionChecker.transform.localScale = obj.GetComponent<Collider>().bounds.size + (new Vector3(colliderSizeModifier, colliderSizeModifier, colliderSizeModifier));
        moveCollisionChecker.transform.position = obj.transform.position;
        moveCollisionChecker.transform.position = dir;

    }

    IEnumerator DelayedMoveObject(GameObject obj, Vector3 objDirection, Vector3 playerDirection)    // Coroutine
    {
        yield return new WaitForSeconds(delayTime);

        isColliding = moveCollisionChecker.gameObject.GetComponent<TriggerDetection>().IsColliding();

        if (isColliding == false)
        {
            clickedArrow.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
            // Vector3.Lerp(obj.transform.position, moveCollisionChecker.transform.position, 2f);   // Must be in update loop
            obj.transform.position = objDirection;
            player.transform.position = playerDirection;
        }
        else
        {
            clickedArrow.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
            Debug.Log("Space occupied, cannot move object!");
            isColliding = false;
        }

        tempMoveLength = moveLength;
        Destroy(moveCollisionChecker);

    }

    public float getDelayTime()
    {
        return delayTime;
    }

    public void setClickedGameObject(GameObject clickedObject)
    {
        clickedArrow = clickedObject;
    }
}
