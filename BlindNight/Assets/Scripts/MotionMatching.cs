using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionMatching : MonoBehaviour
{
    public Transform[] rig;
    CSVReader csvData;
    int j = 0;
    bool status = false;

    void Start()
    {
        csvData = FindObjectOfType<CSVReader>();
        Debug.Log(csvData.gameObject.name);
    }
    
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        for (int i = 0; i < rig.Length ; i++)
        {
            rig[i].rotation = csvData.GetQuaternions()[i][j];
            rig[i].position = csvData.GetPositions()[i][j];
        }

        if (!status)
            j++;
        else
            j--;
        if (j >= csvData.GetQuaternions()[0].Count-1 || j <= csvData.GetQuaternions()[0].Count+1)
            status = !status;
    }
}

class TrajectoryPoint
{
    Vector3 point;
    float angle;
}
