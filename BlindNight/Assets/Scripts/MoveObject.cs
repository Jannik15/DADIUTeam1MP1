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

    bool isColliding1 = false;
    bool isColliding2 = false;

    GameObject clickedArrow;
    GameObject moveCollisionChecker1;
    GameObject moveCollisionChecker2;
    GameObject player;

    // public GameObject objectToMove; // Used for debugging

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        tempMoveLength = moveLength;

        // MoveWest(objectToMove);    // Used for debugging
    }

    public void MoveNorth(GameObject obj, bool check)
    {
        if (!check)
        {
            Vector3 pDirection = new Vector3(player.transform.position.x + moveLength, player.transform.position.y, player.transform.position.z);
            Vector3 direction = new Vector3(obj.transform.position.x + moveLength, obj.transform.position.y, obj.transform.position.z);

            obj.transform.position = direction;
            player.transform.position = pDirection;
        }
        else
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
        
    }

    public void MoveSouth(GameObject obj, bool check)
    {
        if (!check)
        {
            Vector3 pDirection = new Vector3(player.transform.position.x - moveLength, player.transform.position.y, player.transform.position.z);
            Vector3 direction = new Vector3(obj.transform.position.x - moveLength, obj.transform.position.y, obj.transform.position.z);

            obj.transform.position = direction;
            player.transform.position = pDirection;
        }
        else
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
        
    }

    public void MoveWest(GameObject obj, bool check)
    {
        if (!check)
        {
            Vector3 pDirection = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + moveLength);
            Vector3 direction = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z + moveLength);

            obj.transform.position = direction;
            player.transform.position = pDirection;
        }
        else
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
        
    }

    public void MoveEast(GameObject obj, bool check)
    {
        if (!check)
        {
            Vector3 pDirection = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - moveLength);
            Vector3 direction = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z - moveLength);

            obj.transform.position = direction;
            player.transform.position = pDirection;
        }
        else
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

    }

    public void CheckNorthSouth(GameObject obj)
    {
        Debug.Log("Checking North South!");
        Vector3 playerToObjectDirection = obj.gameObject.transform.position - player.gameObject.transform.position;  // Calculates the direction vector
        Vector3 direction1 = new Vector3(obj.transform.position.x + moveLength, obj.transform.position.y, obj.transform.position.z);
        Vector3 direction2 = new Vector3(obj.transform.position.x - moveLength * 2, obj.transform.position.y, obj.transform.position.z);
        Vector3 pDirection = new Vector3(player.transform.position.x - moveLength, player.transform.position.y, player.transform.position.z);

        if (Mathf.Sign(playerToObjectDirection.x) == -1)
        {
            direction1 = new Vector3(obj.transform.position.x + moveLength * 2, obj.transform.position.y, obj.transform.position.z);
            direction2 = new Vector3(obj.transform.position.x - moveLength, obj.transform.position.y, obj.transform.position.z);
            pDirection = new Vector3(player.transform.position.x + moveLength, player.transform.position.y, player.transform.position.z);
        }

        moveCollisionCheckerInit(direction1, direction2, obj);

        StartCoroutine(CheckAxisCollisons(obj, false));
    }

    public void CheckWestEast(GameObject obj)
    {
        Debug.Log("Checking West East!");
        Vector3 playerToObjectDirection = obj.gameObject.transform.position - player.gameObject.transform.position;  // Calculates the direction vector
        Vector3 direction1 = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z + moveLength);
        Vector3 direction2 = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z - moveLength * 2);
        Vector3 pDirection = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - moveLength);

        if (Mathf.Sign(playerToObjectDirection.x) == -1)
        {
            direction1 = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z - moveLength * 2);
            direction2 = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z + moveLength);
            pDirection = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + moveLength);
        }

        moveCollisionCheckerInit(direction1, direction2, obj);

        StartCoroutine(CheckAxisCollisons(obj, true));
    }

        private void moveCollisionCheckerInit(Vector3 dir1, Vector3 dir2, GameObject obj)
    {      
        moveCollisionChecker1 = Instantiate(Resources.Load("Prefabs/MoveCollisionCheckerObject", typeof(GameObject)) as GameObject);
        moveCollisionChecker1.gameObject.GetComponent<TriggerDetection>().SetCallerCol(obj.gameObject.GetComponent<Collider>());
        moveCollisionChecker2 = Instantiate(Resources.Load("Prefabs/MoveCollisionCheckerObject", typeof(GameObject)) as GameObject);
        moveCollisionChecker2.gameObject.GetComponent<TriggerDetection>().SetCallerCol(obj.gameObject.GetComponent<Collider>());

        moveCollisionChecker1.transform.localScale = obj.GetComponent<Collider>().bounds.size + (new Vector3(colliderSizeModifier, colliderSizeModifier, colliderSizeModifier));
        moveCollisionChecker1.transform.position = obj.transform.position;
        moveCollisionChecker1.transform.position = dir1;
        moveCollisionChecker2.transform.localScale = obj.GetComponent<Collider>().bounds.size + (new Vector3(colliderSizeModifier, colliderSizeModifier, colliderSizeModifier));
        moveCollisionChecker2.transform.position = obj.transform.position;
        moveCollisionChecker2.transform.position = dir2;
    }

    private void moveCollisionCheckerInit(Vector3 dir1, GameObject obj)
    {
        moveCollisionChecker1 = Instantiate(Resources.Load("Prefabs/MoveCollisionCheckerObject", typeof(GameObject)) as GameObject);
        moveCollisionChecker1.gameObject.GetComponent<TriggerDetection>().SetCallerCol(obj.gameObject.GetComponent<Collider>());

        moveCollisionChecker1.transform.localScale = obj.GetComponent<Collider>().bounds.size + (new Vector3(colliderSizeModifier, colliderSizeModifier, colliderSizeModifier));
        moveCollisionChecker1.transform.position = obj.transform.position;
        moveCollisionChecker1.transform.position = dir1;
    }

    IEnumerator DelayedMoveObject(GameObject obj, Vector3 objDirection, Vector3 playerDirection)    // Coroutine
    {
        yield return new WaitForSeconds(delayTime);

        isColliding1 = moveCollisionChecker1.gameObject.GetComponent<TriggerDetection>().IsColliding();

        if (isColliding1 == false)
        {
            clickedArrow.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
            // Vector3.Lerp(obj.transform.position, moveCollisionChecker.transform.position, 2f);   // Must be in update loop

            AkSoundEngine.PostEvent("Play_Moving_Object", gameObject);
            obj.transform.position = objDirection;
            player.transform.position = playerDirection;
        }
        else
        {
            clickedArrow.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
            AkSoundEngine.PostEvent("Play_Move_Negative", gameObject);
            Debug.Log("Space occupied, cannot move object!");
            isColliding1 = false;
        }

        tempMoveLength = moveLength;
        Destroy(moveCollisionChecker1);

    }

    IEnumerator CheckAxisCollisons(GameObject obj, bool isVerticalAxis)
    {
        yield return new WaitForSeconds(delayTime);

        isColliding1 = moveCollisionChecker1.gameObject.GetComponent<TriggerDetection>().IsColliding();
        isColliding2 = moveCollisionChecker2.gameObject.GetComponent<TriggerDetection>().IsColliding();
        Debug.Log(isColliding1 + " " + isColliding2);

        bool[] axisBoolArray = new bool[3] {isVerticalAxis, isColliding1, isColliding2};

        player.GetComponent<Interactions>().SpawnArrows(axisBoolArray);

        Destroy(moveCollisionChecker1);
        Destroy(moveCollisionChecker2);
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
