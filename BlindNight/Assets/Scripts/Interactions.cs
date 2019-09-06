using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    // public CharacterMovement charMove;
    public Vector3 clickPos, hitCollider;
    public float interactThreshold, interactThresholdOS, interactStep, slideSpeed, unitSize;
    private Vector3 oldPos;
    public bool canMove;
    // Start is called before the first frame update
   
    // Handle interaction with items when the player clicks on them. 
    // For first version, click on object, if it has "Interactable" tag, Debug in console.
    void Start()
    {
        clickPos = gameObject.GetComponent<CharacterMovement>().getPressPos();
        interactThreshold = 1.5f;
        interactThresholdOS = 0.5f;
        slideSpeed = 10.0f;
        interactStep = slideSpeed * Time.deltaTime;
        canMove = true;
        unitSize = 1;
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
        if(!(Physics.Raycast(ray, out hit) && hit.collider.tag == "interactable")) {
            return;
        }
        
        hitCollider = hit.collider.transform.position;
        GameObject otherObject = hit.collider.gameObject;
        Debug.Log("Item has been pressed " + hit.collider.name); 

        // Check if interactable object has been moved to, and what direction you're in.
        if(hit.collider.tag == "interactable" /* && Vector3.Distance(transform.position, otherObject) < 1 */) {
            // LockMovement();
            /*
            SpawnArrows();
            */
            
        }
        if(hit.collider.name.Contains("arrow")) {

                oldPos = otherObject.transform.position;
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
        /*  if(isHorizontal) {
            Instantiate(rightArrow, rightArrowPos)
            Instantiate(leftArrow, leftArrowPos)
        }
        
            else() {
                Instantiate(upArrow, upArrowPos)
                Instantiate(downArrow, downArrowPos)
            }
                
                */
    }
}

