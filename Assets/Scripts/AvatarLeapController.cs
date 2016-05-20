using UnityEngine;
//using Windows.Kinect;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;

public class AvatarLeapController : AvatarController
{
    // Public variables that will get matched to bones. If empty, the Kinect will simply not track it.
    public Transform HipCenter;
    public Transform Spine;
    public Transform Neck;
    public Transform Head;

    public Transform LeftClavicle;
    public Transform LeftUpperArm;
    public Transform LeftElbow;
    public Transform LeftHand;
    private Transform LeftFingers = null;

    public Transform RightClavicle;
    public Transform RightUpperArm;
    public Transform RightElbow;
    public Transform RightHand;
    private Transform RightFingers = null;

    public Transform LeftThigh;
    public Transform LeftKnee;
    public Transform LeftFoot;
    private Transform LeftToes = null;

    public Transform RightThigh;
    public Transform RightKnee;
    public Transform RightFoot;
    private Transform RightToes = null;

    public Transform BodyRoot;
    public GameObject OffsetNode;


    protected Animator animator;

    public bool UseLeapHands = false;
    public GameObject rightHandTarget = null;
    public GameObject leftHandTarget = null;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // If the bones to be mapped have been declared, map that bone to the model.
    protected override void MapBones()
    {
        bones[0] = HipCenter;
        bones[1] = Spine;
        bones[2] = Neck;
        bones[3] = Head;

        bones[4] = LeftClavicle;
        bones[5] = LeftUpperArm;
        bones[6] = LeftElbow;
        bones[7] = LeftHand;
        bones[8] = LeftFingers;

        bones[9] = RightClavicle;
        bones[10] = RightUpperArm;
        bones[11] = RightElbow;
        bones[12] = RightHand;
        bones[13] = RightFingers;

        bones[14] = LeftThigh;
        bones[15] = LeftKnee;
        bones[16] = LeftFoot;
        bones[17] = LeftToes;

        bones[18] = RightThigh;
        bones[19] = RightKnee;
        bones[20] = RightFoot;
        bones[21] = RightToes;

        // body root and offset
        bodyRoot = BodyRoot;
        offsetNode = OffsetNode;

        if (offsetNode == null)
        {
            offsetNode = new GameObject(name + "Ctrl") { layer = transform.gameObject.layer, tag = transform.gameObject.tag };
            offsetNode.transform.position = transform.position;
            offsetNode.transform.rotation = transform.rotation;
            offsetNode.transform.parent = transform.parent;

            transform.parent = offsetNode.transform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        //		if(bodyRoot == null)
        //		{
        //			bodyRoot = transform;
        //		}
    }

    public bool checkLeap(int boneIndex)
    {
        print("HI");
        if (UseLeapHands)
        {
            if (rightHandTarget != null && rightHandTarget.activeInHierarchy && (boneIndex == 5 || boneIndex == 6 || boneIndex == 7))
            {
                print("HI");
                return true;
            }

            if (leftHandTarget != null && leftHandTarget.activeInHierarchy && (boneIndex == 10 || boneIndex == 11 || boneIndex == 12))
            {
                print("HI");
                return true;
            }
        }
        return false;
    }

    // Update the avatar each frame.
    public override void UpdateAvatar(uint UserID)
    {
        if (!transform.gameObject.activeInHierarchy)
            return;

        // Get the KinectManager instance
        if (kinectManager == null)
        {
            kinectManager = KinectManager.Instance;
        }

        // move the avatar to its Kinect position
        MoveAvatar(UserID);

        for (var boneIndex = 0; boneIndex < bones.Length; boneIndex++)
        {
            if (!checkLeap(boneIndex))
            {
                if (!bones[boneIndex])
                    continue;

                if (boneIndex2JointMap.ContainsKey(boneIndex))
                {
                    KinectWrapper.NuiSkeletonPositionIndex joint = !mirroredMovement ? boneIndex2JointMap[boneIndex] : boneIndex2MirrorJointMap[boneIndex];
                    TransformBone(UserID, joint, boneIndex, !mirroredMovement);
                }
                else if (specIndex2JointMap.ContainsKey(boneIndex))
                {
                    // special bones (clavicles)
                    List<KinectWrapper.NuiSkeletonPositionIndex> alJoints = !mirroredMovement ? specIndex2JointMap[boneIndex] : specIndex2MirrorJointMap[boneIndex];

                    if (alJoints.Count >= 2)
                    {
                        //Vector3 baseDir = alJoints[0].ToString().EndsWith("Left") ? Vector3.left : Vector3.right;
                        //TransformSpecialBone(UserID, alJoints[0], alJoints[1], boneIndex, baseDir, !mirroredMovement);
                    }
                }
            }
        }
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (animator)
        {

            //if the IK is active, set the position and rotation directly to the goal. 
            if (UseLeapHands)
            {

                // Set the right hand target position and rotation, if one has been assigned
                if (rightHandTarget != null && rightHandTarget.activeInHierarchy)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.transform.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.transform.rotation);
                }
                // Set the right hand target position and rotation, if one has been assigned
                if (leftHandTarget != null && rightHandTarget.activeInHierarchy)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.transform.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.transform.rotation);
                }

            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetLookAtWeight(0);
            }
        }
    }
}

