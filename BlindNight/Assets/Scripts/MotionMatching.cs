using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionMatching : MonoBehaviour
{
    /* What is a pose?
     * Pose = Each joint transform at a given timestep in the animation
     */
    

    Goal goal;
    public Transform[] rig;
    TrajectoryTest movement;    // Movement script reference
    List<MMPose> allPoses;
    CSVReader csvData;
    bool status = false;
    int currentPoseIndex = 0;
    //float currentAnimTime;

    void Start()
    {
        movement = GetComponent<TrajectoryTest>();
        csvData = FindObjectOfType<CSVReader>();
        allPoses = new List<MMPose>();

        /// Populate the list allPoses with all poses in the datasheet
        for (int i = 0; i < csvData.GetQuaternions()[0].Count; i++)
        {
            Pose[] tempPose = new Pose[rig.Length];
            for (int j = 0; j < rig.Length; j++)
            {
                tempPose[j] = new Pose(csvData.GetPositions()[j][i], csvData.GetQuaternions()[j][i]);
            }
            allPoses.Add(new MMPose(tempPose, i));
        }
    }

    void MMUpdate(Goal goal)
    {
        /// Determine the current pose based on the rig rotations and current pose index
        MMPose currentPose;
        Pose[] tempPose = new Pose[rig.Length];
        for (int i = 0; i < rig.Length; i++)
        {
            tempPose[i] = new Pose(rig[i].position, rig[i].rotation);
            //currentPose[i] = new Pose(csvData.GetPositions()[i][currentPoseIndex], csvData.GetQuaternions()[i][currentPoseIndex]);
        }
        currentPose = new MMPose(tempPose, currentPoseIndex);

        MMPose bestPose = new MMPose(new Pose[rig.Length], 0);
        float bestDiff = 9999999999;

        /// We compare all the poses to the current pose, to find the best position for each joint, and store these in a candidate pose
        for (int i = 0; i < allPoses.Count; i++)
        {
            MMPose candidatePose = new MMPose(new Pose[rig.Length], 0);
            candidatePose = allPoses[i];

            /// Must not be the same pose
            if (candidatePose.getPoseIndex() == currentPose.getPoseIndex())
                continue;

            /// Compare pose difference between quaternions - just take the one closest to the current
            
            float diff = 0;
            for (int j = 0; j < rig.Length; j++)
            {
                diff += Quaternion.Angle(currentPose.GetJointTransform(j).rotation, candidatePose.GetJointTransform(j).rotation);
            }
            if (diff < bestDiff)
            {
                bestPose = candidatePose;
            }
        }

        // Apply best pose to rig
        for(int i = 0; i < rig.Length; i++)
        {
            rig[i].rotation = bestPose.GetJointTransform(i).rotation;
        }
        currentPoseIndex = bestPose.getPoseIndex();
    }

    private void LateUpdate()
    {
        MMUpdate(goal);
    }

    private void AutoplayAnimation()
    {
        for (int i = 0; i < rig.Length; i++)
        {
            rig[i].rotation = csvData.GetQuaternions()[i][currentPoseIndex];
            rig[i].position = csvData.GetPositions()[i][currentPoseIndex];
        }

        if (!status)
            currentPoseIndex++;
        else
            currentPoseIndex--;
        if (currentPoseIndex >= csvData.GetQuaternions()[0].Count - 1)
            status = true;
        else if (currentPoseIndex <= 0)
            status = false;
    }
}

class TrajectoryPoint
{
    Vector3 point;
    float Angle;
    float timeDelay;
}

class Goal
{
    List<TrajectoryPoint> desiredTrajectory;

    /// If a specific type of stance is desired, this should be put here
}

class MMPose
{
    /*  The purpose of this class is to store the information from the motion capture for each joint in a Pose-like structure.
     *  A Pose takes the position (vector3) and Rotation (quaternion) of an object. 
     *  Our pose will store the positions and rotations of all joints, such that we can simply set the rig to a desired pose.
     *  This pose must also  contain the animation index, used to cull the same animation during comparisons.
     */
    Pose[] pose;
    int poseIndex;
    
    public MMPose(Pose[] _pose, int _poseIndex)
    {
        pose = _pose;
        poseIndex = _poseIndex;
    }

    public Pose[] GetPose()
    {
        return pose;
    }

    public Pose GetJointTransform(int num)
    {
        return pose[num];
    }

    public int getPoseIndex()
    {
        return poseIndex;
    }
}
