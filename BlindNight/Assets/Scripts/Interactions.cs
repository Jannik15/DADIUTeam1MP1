using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    // public CharacterMovement charMove;
    public Vector3 clickPos, hitCollider;
    public float interactThreshold, interactThresholdOS, interactStep, slideSpeed, unitSize;

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
        
        if(!(Physics.Raycast(ray, out hit) && hit.collider.tag == "interactable")) {
            return;
        }
        
        hitCollider = hit.collider.transform.position;
        GameObject otherObject = hit.collider.gameObject;
        Debug.Log("Item has been pressed " + hit.collider.name); 
        CheckObjectMovement(otherObject);
    }

    void CheckObjectMovement(GameObject obj) {
        float objX = obj.transform.position.x;
        float objZ = obj.transform.position.z;

        float objWidth = obj.GetComponent<BoxCollider>().size.x;
        float objHeight = obj.GetComponent<BoxCollider>().size.z;

        float playerX = transform.position.x;
        float playerZ = transform.position.z;
        
        CheckObjectMovementVertical(objX, objZ, objWidth/2, objHeight/2, playerX, playerZ);
        // Check horizontal 

    }

    void CheckObjectMovementVertical(float objX, float objZ, float halfWidth, float halfHeight, float playerX, float playerZ) {
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



        


    }
}

