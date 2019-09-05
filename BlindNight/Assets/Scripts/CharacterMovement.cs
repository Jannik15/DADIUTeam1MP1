using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    private float playerMoveSpeed, moveThreshold, moveStep;


    
    // This is the script used to move the character.
    // Start is called before the first frame update

    private Vector3 goalPos;
    
    void Start()
    {
        playerMoveSpeed = 10;
        moveStep = playerMoveSpeed * Time.deltaTime;
        goalPos = transform.position;
        moveThreshold = 0.1f;
    }

    void Update()
    {

        GetInput();
        if(Vector3.Distance(transform.position, goalPos) > moveThreshold)  {

            transform.position = Vector3.MoveTowards(transform.position, goalPos, moveStep);
        }
        
    }

    public void GetInput(){
         if(GameMaster.instance.GetWalkType() == 0 && Input.GetMouseButtonDown(0)) {

                goalPos = getPressPos();
            } 

            /* if(Input.GetTouch(0).phase == TouchPhase.Began) {
                goalPos = getPressPos();
            }  */

        if(GameMaster.instance.GetWalkType() == 1 && Input.GetMouseButton(0)) {
            //if(Input.touchCount > 1) {
                goalPos = getPressPos();
            //}
        }
    }

    public Vector3 getPressPos() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit)) {
            return new Vector3(hit.point.x, transform.position.y, hit.point.z);
      
        }
    
        else {
            return new Vector3(0,0,0);
        }
    }

}



