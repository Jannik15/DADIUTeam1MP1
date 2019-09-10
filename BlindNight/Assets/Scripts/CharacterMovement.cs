using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private float playerMoveSpeed, moveThreshold, moveStep, playerRotateSpeed, rotStep;

    private Vector3 goalPos, oldCharPos;

    private Vector2 mousePos;
    private Vector2 targetMousePos;

    private float minDragDist = 0.2f;

    private bool joystickActive = false, playerFrozen = false;
    private float moveVectorScale = 100.0f;
    private float maxMoveVectorLength = 1.0f, distTravelled, stepLength;

    void Start()
    {
        UnfreezePlayer();
        playerMoveSpeed = 2;
        moveStep = playerMoveSpeed * Time.deltaTime;

        playerRotateSpeed = 10;
        rotStep = playerRotateSpeed * Time.deltaTime;

        goalPos = transform.position;
        moveThreshold = 0.1f;

        distTravelled = 0;
        stepLength = 1;
        
        oldCharPos = transform.position;
    }

    void Update()
    {

        //if (GameMaster.instance.GetCanPlay())

        if (!playerFrozen)

        {
            if (GameMaster.instance.GetWalkType() == 2)
            {
                MoveWithKeyboard();
            }
            else if (GameMaster.instance.GetWalkType() == 4)
            {
                MoveWithNewJoystick();
            }
            else
            {
                GetInput();
                Vector3 direction = goalPos - transform.position;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotStep);
                if (Vector3.Distance(transform.position, goalPos) > moveThreshold)
                {
                    transform.position = Vector3.MoveTowards(transform.position, goalPos, moveStep);
                }
            }


            distTravelled += Vector3.Distance(oldCharPos, transform.position);
            oldCharPos = transform.position;
            if (distTravelled > stepLength)
            {
                distTravelled = 0;
                AkSoundEngine.PostEvent("Play_Footstep_Surface", gameObject);
            }
        }
    }

    void MoveWithNewJoystick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Input.mousePosition;
            if (mousePos.y > (Screen.height / 2))
            {
                return;
            }
            joystickActive = true;
            GameObject JoystickUI = GameMaster.instance.FindObjectFromParentName("Canvas", "JoystickUI");
            JoystickUI.transform.position = mousePos;
            JoystickUI.SetActive(true);
        }
        if (Input.GetMouseButton(0))
        {
            if (!joystickActive)
            {
                return;
            }
            MoveWithJoystickVector((Vector2)Input.mousePosition - mousePos);
        }
        if (Input.GetMouseButtonUp(0))
        {
            joystickActive = false;
            GameMaster.instance.FindObjectFromParentName("Canvas", "JoystickUI").SetActive(false);
        }
    }

    void MoveWithJoystickVector(Vector2 direction)
    {
        UpdateJoystickUI(direction);

        float angle = -0.25f * Mathf.PI; // -45 degrees
        Vector2 turnedDirection = new Vector2(0,0);
        turnedDirection.x = direction.x * Mathf.Cos(angle) - direction.y * Mathf.Sin(angle);
        turnedDirection.y = direction.x * Mathf.Sin(angle) + direction.y * Mathf.Cos(angle);

        turnedDirection /= moveVectorScale;
        turnedDirection = Vector2.ClampMagnitude(turnedDirection, maxMoveVectorLength);
        //Debug.Log(turnedDirection);

        // get new position
        Vector3 newPos = transform.position;
        newPos.x += turnedDirection.x * playerMoveSpeed * Time.deltaTime;
        newPos.z += turnedDirection.y * playerMoveSpeed * Time.deltaTime;

        // look towards new position
        Vector3 newDir = newPos - transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(newDir), rotStep);

        // move to new position
        transform.position = newPos;
    }

    private void UpdateJoystickUI(Vector2 direction)
    {
        float angle = Vector2.Angle(new Vector2(0, 1), direction);
        if (direction.x > 0)
        {
            angle = (180 - angle) + 180;
        }
        angle -= 40; //image is not pointing forward

        GameObject JoystickUI = GameMaster.instance.FindObjectFromParentName("Canvas", "JoystickUI");
        if (JoystickUI)
        {
            GameObject DirectionUI = GameMaster.instance.FindObjectFromParentObject(JoystickUI, "Direction");
            if (DirectionUI)
            {
                DirectionUI.transform.eulerAngles = new Vector3(0, 0, angle);
            }
        }
    }

    void MoveWithKeyboard()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 position = transform.position;
            position.x -= moveStep;
            transform.position = position;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 position = transform.position;
            position.x += moveStep;
            transform.position = position;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 position = transform.position;
            position.z += moveStep;
            transform.position = position;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 position = transform.position;
            position.z -= moveStep;
            transform.position = position;
        }
    }

    public Vector3 GetPressPos()
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
        switch (GameMaster.instance.GetWalkType())
        {
            case 0:
                if (Input.GetMouseButtonDown(0))
                {
                    goalPos = GetPressPos();
                }
                break;
            case 1:
                if (Input.GetMouseButton(0))
                {
                    goalPos = GetPressPos();
                }
                break;
            case 3:
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
                    }
                    else
                    {
                        goalPos = transform.position;
                    }
                }
                if (Input.GetMouseButtonUp(0))
                {
                    goalPos = transform.position;
                }
                break;
            default:
                break;
        }
    }

    public void FreezePlayer()
    {
        playerFrozen = true;
    }

    public void UnfreezePlayer()
    {
        playerFrozen = false;
    }
}
