using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionMatching : MonoBehaviour
{
    public Transform[] rig;
    List<Pose[]> allPoses;
    CSVReader csvData;
    int j = 0;
    bool status = false;
    int currentAnimIndex;
    float currentAnimTime;

    void Start()
    {
        csvData = FindObjectOfType<CSVReader>();

        for (int i = 0; i < csvData.GetQuaternions()[0].Count; i++)
        {
            Pose[] tempPose = new Pose[rig.Length];
            for (int j = 0; j < rig.Length; j++)
                tempPose[j] = new Pose(csvData.GetPositions()[j][currentAnimIndex], csvData.GetQuaternions()[j][currentAnimIndex]);
            allPoses.Add(tempPose);
        }
    }

    void MMUpdate(float dt)
    {
        currentAnimTime += dt;
        /// Pose takes a vector3 and a quaternion, since we need a representation of this for all joints, we use a Pose array
        Pose[] currentPose = new Pose[rig.Length];
        for (int i = 0; i < rig.Length; i++)
        {
             currentPose[i] = new Pose(csvData.GetPositions()[i][currentAnimIndex], csvData.GetQuaternions()[i][currentAnimIndex]);
        }
        
        Pose[] bestPose = new Pose[rig.Length];

        /// We compare all the poses to the current pose, to find the best position for each joint, and store these in a candidate pose
        for (int i = 0; i < allPoses.Count; i++)
        {
            Pose[] candidatePose = new Pose[rig.Length];
            candidatePose = allPoses[i];
            if (candidatePose == currentPose)
                continue;
            float[] differences = new float[rig.Length];
            for (int j = 0; j < rig.Length; j++)
            {
                if (i == 0 || Quaternion.Angle(currentPose[j].rotation, candidatePose[j].rotation) < differences[j])
                {
                    differences[j] = Quaternion.Angle(currentPose[j].rotation, candidatePose[j].rotation);
                    bestPose[j] = candidatePose[j];
                }
            }
        }
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
        if (j >= csvData.GetQuaternions()[0].Count - 1)
            status = true;
        else if (j <= 0)
            status = false;


    }
}

class TrajectoryPoint
{
    Vector3 point;
    float angle;
    float timeDelay;
}

class Goal
{
    List<TrajectoryPoint> desiredTrajectory;

    /// If a specific type of stance is desired, this should be put here
}
