using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionMatching : MonoBehaviour
{
    /* What is a pose? (idle, walk, run?)
     * Pose = Each joint transform at a given timestep in the animation
     */
    
    public Transform[] rig;
    TrajectoryTest movement;    // Movement script reference
    List<MMPose> allPoses;
    CSVReader csvData;
    bool status = false;
    int currentPoseIndex = 0;
    string currentPoseState = "Idle";
    MMPose currentPose;
    GameObject player;

    [Tooltip("In seconds")]
    public int timestampJumpThreshold = 3;

    void Start()
    {
        movement = GetComponent<TrajectoryTest>();
        csvData = FindObjectOfType<CSVReader>();
        allPoses = new List<MMPose>();
        player = GameObject.FindGameObjectWithTag("Player");

        /// Populate the list allPoses with all poses in the datasheet
        for (int i = 0; i < csvData.GetQuaternions()[0].Count; i++)
        {
            Pose[] tempPose = new Pose[rig.Length];
            for (int j = 0; j < rig.Length; j++)
            {
                tempPose[j] = new Pose(csvData.GetPositions()[j][i], csvData.GetQuaternions()[j][i]);
            }
            allPoses.Add(new MMPose(tempPose, i, csvData.GetStates()[i]));
        }
        currentPose = allPoses[0];
        currentPoseIndex = currentPose.GetPoseIndex();
        currentPoseState = currentPose.GetPoseState();
    }

    void MMUpdate()
    {

        /// Determine the current pose based on the rig rotations and current pose index
        Pose[] tempPose = new Pose[rig.Length];
        for (int i = 0; i < rig.Length; i++)
        {
            tempPose[i] = new Pose(rig[i].position, rig[i].rotation);
        }
        currentPose = new MMPose(tempPose, currentPoseIndex, currentPoseState);

        MMPose bestPose = new MMPose(new Pose[rig.Length], 0, "");
        float bestDiff = 9999999;

        /// We compare all the poses to the current pose, to find the best position for each joint, and store these in a candidate pose
        MMPose candidatePose = new MMPose(new Pose[rig.Length], 0, "");
        for (int i = 0; i < allPoses.Count; i++)
        {
            candidatePose = allPoses[i];

            /// Must not be the same pose
            if (candidatePose.GetPoseIndex() == currentPose.GetPoseIndex())
            {
                continue;
            }

            /// Compare pose difference between quaternions - just take the one closest to the current
            
            float diff = 0;
            if (movement.currentState == candidatePose.GetPoseState())
            {
                for (int j = 0; j < rig.Length; j++)
                {
                    diff += Quaternion.Angle(currentPose.GetJointTransform(j).rotation, candidatePose.GetJointTransform(j).rotation);   // Need other way to evaluate diff? (joint position diffs also?) YYY
                }
                if (csvData.GetTimestamps()[i] > csvData.GetTimestamps()[currentPose.GetPoseIndex()] + timestampJumpThreshold && diff < bestDiff)
                {
                    bestPose = candidatePose;
                    bestDiff = diff;

                }
                
            }
        }

        Debug.Log(bestPose.GetPoseIndex() + " " + bestPose.GetPoseState());

        // Find current player direction from movement
        Vector3 mov = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Quaternion playerDirection = Quaternion.LookRotation(mov, Vector3.up);

        // Apply best pose to rig
        for (int i = 0; i < rig.Length; i++)
        {
            rig[i].rotation = playerDirection * bestPose.GetJointTransform(i).rotation; // new best pose relative to the current player direction
        }
        currentPoseIndex = bestPose.GetPoseIndex();
        currentPoseState = bestPose.GetPoseState();
    }

    private void LateUpdate()
    {

        // AutoplayAnimation();
        MMUpdate();
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

class MMPose
{
    /*  The purpose of this class is to store the information from the motion capture for each joint in a Pose-like structure.
     *  A Pose takes the position (vector3) and Rotation (quaternion) of an object. 
     *  Our pose will store the positions and rotations of all joints, such that we can simply set the rig to a desired pose.
     *  This pose must also  contain the animation index, used to cull the same animation during comparisons.
     */
    Pose[] pose;
    int poseIndex;
    string poseState;
    
    public MMPose(Pose[] _pose, int _poseIndex, string _poseState)
    {
        pose = _pose;
        poseIndex = _poseIndex;
        poseState = _poseState;
    }

    public Pose[] GetPose()
    {
        return pose;
    }

    public Pose GetJointTransform(int num)
    {
        return pose[num];
    }

    public string GetPoseState()
    {
        return poseState;
    }

    public int GetPoseIndex()
    {
        return poseIndex;
    }
}
