using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryTest : MonoBehaviour
{
    [HideInInspector] public Vector3 dir;
    public int trajectoryGizmosCount;
    public float speed, segmentScale;
    private string[] states =
    {
        "Idle",
        "Transition",
        "Movement",
        "TurnLeft",
        "TurnRight"
    };
    [HideInInspector] public string currentState;

    //public GameObject leftFootCollider, rightFootCollider;

    void Start()
    {
        currentState = states[0];
    }

    // Update is called once per frame
    void Update()
    {
        dir = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
        transform.position = transform.position + dir * Time.deltaTime;
        if (dir.magnitude > 0.00f)
            currentState = states[2];
        else
            currentState = states[0];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(leftFootCollider.transform.position, 0.05f);
        //Gizmos.DrawWireSphere(rightFootCollider.transform.position, 0.05f);

        Gizmos.color = Color.green;
        //float j =
        for (float i = 1; i < trajectoryGizmosCount; i = 100)
        {
            Gizmos.DrawWireSphere(transform.position + dir*2 / i, 0.1f);
        }
    }
}
