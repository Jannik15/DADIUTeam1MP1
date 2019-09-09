using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    // public CharacterMovement charMove;
    // Merge test comment 9:54 9/9/19
    LayerMask layerMask;
    private Vector3 clickPos, hitCollider;
    private float interactThreshold, interactThresholdOS, interactStep, slideSpeed, unitSize, arrowDisplaceX, arrowDisplaceY, arrowDisplaceZ;
    private bool canMove;
    GameObject otherObject;
    public GameObject arrow;

    private GameObject objectClicked, arrowClicked;

    InteractionsSideChecker _ISC;
    private GameObject[] arrowList;
    private MoveObject _moveObject;
    // Start is called before the first frame update
   
    // Handle interaction with items when the player clicks on them. 
    // For first version, click on object, if it has "Interactable" tag, Debug in console.
    void Start()
    {
        layerMask = 1 << 10;
        clickPos = gameObject.GetComponent<CharacterMovement>().GetPressPos();
        interactThreshold = 1.5f;
        interactThresholdOS = 0.5f;
        slideSpeed = 10.0f;
        interactStep = slideSpeed * Time.deltaTime;
        canMove = true;
        unitSize = 1;
        arrowDisplaceX = 1f;
        arrowDisplaceY = 2.5f;
        arrowDisplaceZ = 1f;
        _ISC = GetComponent<InteractionsSideChecker>();
        _moveObject = GetComponent<MoveObject>();
    }

    // Update is called once per frame
    void Update()
    {
        getPressPosInteractable();
        //HandleInteraction();

    }




    public void getPressPosInteractable() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(!Input.GetMouseButtonDown(0)) {
            return;
        }

        RaycastHit hit;
        // Check if item pressed is interactable. 
        if (!Physics.Raycast(ray, out hit, layerMask))
        {
            Debug.Log("Nothing hit");
            return;
        }

        hitCollider = hit.collider.transform.position;
        otherObject = hit.collider.gameObject;
        //Debug.Log("Item has been pressed " + hit.collider.name); 

        // Check if interactable object has been moved to, and what direction you're in.
        if(hit.collider.tag == "interactable" /* && Vector3.Distance(transform.position, otherObject) < 1 */) {
            Debug.Log("Entered box click");
            // LockMovement();
            objectClicked = hit.collider.gameObject;
            DestroyArrows();
            SpawnArrows();
            
            
        }
        
        if(hit.collider.tag == "arrow") {
            Debug.Log("Entered arrow statement");
            arrowClicked = hit.collider.gameObject;


/*             if(hit.collider.tag == "northArrow") {
                Debug.Log("North arrow clicked");
            }
            if(hit.collider.tag == "southArrow") {
                Debug.Log("North arrow clicked");
            }
            if(hit.collider.tag == "northArrow") {
                Debug.Log("North arrow clicked");
            }
            if(hit.collider.tag == "northArrow") {
                Debug.Log("North arrow clicked");
            } */

            switch(hit.collider.name) {
                case "northArrow":

                    //Debug.Log("North Arrow Clicked");
                    _moveObject.MoveNorth(objectClicked);
                    DestroyArrows();
                    break;

                case "southArrow":
                    //Debug.Log("South Arrow Clicked");
                    _moveObject.MoveSouth(objectClicked);
                    DestroyArrows();
                    break;

                case "eastArrow":
                    //Debug.Log("East Arrow Clicked");
                    _moveObject.MoveEast(objectClicked);
                    DestroyArrows();
                    break;

                case "westArrow":
                    //Debug.Log("West Arrow Clicked");
                    _moveObject.MoveWest(objectClicked);
                    DestroyArrows();
                    break;
                default: 
                    //Debug.Log("Name doesn't exist");
                    break;
            }


/*                 if(colliding ) {
                otherObject.transform.position = oldPos; 
                // Play not moveable animation                   
                }

                else {
                otherObject.transform.position = oldPos;
                Vector3.Lerp(hitCollider, new Vector3(hitCollider.x, hitCollider.y, hitCollider.z), interactStep);
                // Play move animation
                } */

        }
/*         CheckObjectMovement(otherObject); */
    }
/* 
    void CheckObjectMovement(GameObject obj) {
        float objX = obj.transform.position.x;
        float objZ = obj.transform.position.z;

        float objWidth = obj.GetComponent<BoxCollider>().size.x;
        float objHeight = obj.GetComponent<BoxCollider>().size.z;

        float playerX = transform.position.x;
        float playerZ = transform.position.z;
        
        CheckObjectMovementVertical(objX, objZ, objWidth/2, objHeight/2, playerX, playerZ);
        // Check horizontal 

    } */

/*     void CheckObjectMovementVertical(float objX, float objZ, float halfWidth, float halfHeight, float playerX, float playerZ) {
        if(playerX > objX + halfWidth) {
            return;
        }

        if(playerX < objX - halfWidth) {
            return;
        }

        if(playerZ > objZ + halfHeight + unitSize) {
            return;
        }

        if(playerZ < objZ - halfHeight - unitSize) {
            return;
        }

        Debug.Log("I am in the right Vertical area");



        


    } */

    public void SpawnArrows() {
        bool[] arrowBool = new bool[2];
        arrowBool = _ISC.IsHorizontal(otherObject);

        if (arrowBool[1] == true) {

            if(arrowBool[0] == false) {
            //Debug.Log("Entered Spawn arrow horizontal");
            //Up arrow
                GameObject upArrow = Instantiate(arrow, new Vector3(hitCollider.x, hitCollider.y + arrowDisplaceY, hitCollider.z + arrowDisplaceZ), Quaternion.identity);
                upArrow.name = "westArrow";
            //Down arrow (rotate by 180)
                GameObject downArrow = Instantiate(arrow, new Vector3(hitCollider.x, hitCollider.y + arrowDisplaceY, hitCollider.z - arrowDisplaceZ), Quaternion.identity);
                downArrow.transform.eulerAngles = new Vector3(0,180,0);//rotation = new Quaternion(downArrow.transform.rotation.x, 270, downArrow.transform.rotation.z, downArrow.transform.rotation.w);
                downArrow.name = "eastArrow";
        }
        
            else {
                //Debug.Log("Entered Spawn arrow vertical");
                // Right arrow (rotate 90)
                GameObject rightArrow = Instantiate(arrow, new Vector3(hitCollider.x + arrowDisplaceX, hitCollider.y + arrowDisplaceY, hitCollider.z), Quaternion.identity);
                rightArrow.transform.eulerAngles = new Vector3(0,90,0);
                rightArrow.name = "northArrow";
                // Left arrow (rotate 270)
                GameObject leftArrow = Instantiate(arrow, new Vector3(hitCollider.x - arrowDisplaceX, hitCollider.y + arrowDisplaceY, hitCollider.z), Quaternion.identity);
                leftArrow.transform.eulerAngles = new Vector3(0,270,0);
                leftArrow.name = "southArrow";
            }

        }
        else {

            Debug.Log("Out of bounds");
        }
        
                
                
    }

    public void DestroyArrows() {
        arrowList = GameObject.FindGameObjectsWithTag("arrow");
        Debug.Log(arrowList);
        for(int i = 0; i < arrowList.Length; i++) {
            // This should loop through all arrows, and destroy them. 
            Destroy(arrowList[i]);
            //Debug.Log("There are " + arrowList[i] + " Arrows left");
        }
    }
}

