using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteadyCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public float distance, cameraHeight;
    private Vector3 offset;
    void Start()
    {
        //distance = Vector3.Distance(transform.position, target.transform.position);
        //cameraHeight = 22.0f;
        //offset = transform.position - target.transform.position;
        offset = new Vector3(-22, 30, -22);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
        /* Vector3 pos = target.transform.position;
        pos.z += cameraHeight;
        transform.position = pos; */
        transform.position = target.transform.position + offset;
    }
}
