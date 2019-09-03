using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public int walkType, moveSpeed;

    // This is the script used to move the character.
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(walkType == 0) {
            //Touch touch = Input.GetTouch(0);
            //transform.LookAt(touch.position);
            //transform.position = transform.position * moveSpeed * touch.position;

            bool aTouch = Input.GetMouseButtonDown(0);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if(aTouch /* && walkType == 0 */) {
                if (Physics.Raycast(ray)) {
                    Vector3 touchPos = Input.mousePosition;
                    transform.position = touchPos * moveSpeed * Time.deltaTime; 
            }
          
            }

        //}
    }
}
