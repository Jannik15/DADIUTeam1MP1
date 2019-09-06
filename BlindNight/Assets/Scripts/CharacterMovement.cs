using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private float playerMoveSpeed, moveThreshold, moveStep, playerRotateSpeed, rotStep;

    private Vector3 goalPos;

    private Vector2 mousePos;
    private Vector2 targetMousePos;

    private float minDragDist = 0.2f;

    void Start()
    {
        playerMoveSpeed = 4;
        moveStep = playerMoveSpeed * Time.deltaTime;

        playerRotateSpeed = 4;
        rotStep = playerRotateSpeed * Time.deltaTime;

        goalPos = transform.position;
        moveThreshold = 0.1f;
    }

    void Update()
    {
        if (GameMaster.instance.GetWalkType() == 2) {
            MoveWithKeyboard();
        } else {
        GetInput();
        Vector3 direction = goalPos - transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotStep);
        if (Vector3.Distance(transform.position, goalPos) > moveThreshold)
            {
                transform.position = Vector3.MoveTowards(transform.position, goalPos, moveStep);
            }
        }
    }

    void MoveWithKeyboard() {
        if(Input.GetKey(KeyCode.LeftArrow)) {
            Vector3 position = transform.position;
            position.x -= moveStep;
            transform.position = position;
        }
        if(Input.GetKey(KeyCode.RightArrow)) {
            Vector3 position = transform.position;
            position.x += moveStep;
            transform.position = position;
        }
        if(Input.GetKey(KeyCode.UpArrow)) {
            Vector3 position = transform.position;
            position.z += moveStep;
            transform.position = position;
        }
        if(Input.GetKey(KeyCode.DownArrow)) {
            Vector3 position = transform.position;
            position.z -= moveStep;
            transform.position = position;
        }
    }

    public Vector3 getPressPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return new Vector3(hit.point.x, transform.position.y, hit.point.z);
        }
        else
        {
            return new Vector3(0, 0, 0);
        }
    }

    public void GetInput()
    {
        if (GameMaster.instance.GetWalkType() == 0 && Input.GetMouseButtonDown(0))
        {
            goalPos = getPressPos();
        }

        if (GameMaster.instance.GetWalkType() == 1 && Input.GetMouseButton(0))
        {
            goalPos = getPressPos();
        }

        if (GameMaster.instance.GetWalkType() == 3)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mousePos = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                targetMousePos = Input.mousePosition;
                if (Vector2.Distance(targetMousePos, mousePos) > 60)
                {
                    Vector2 dirVector = (targetMousePos - mousePos).normalized;
                    goalPos = new Vector3(transform.position.x + dirVector.x, transform.position.y, transform.position.z + dirVector.y);
                } else
                {
                    goalPos = transform.position;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                goalPos = transform.position;
            }
        }
    }
}
