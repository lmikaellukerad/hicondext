using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

/// <summary>
/// This class manages leap and kinect movements.
/// </summary>
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

    public Transform RightClavicle;
    public Transform RightUpperArm;
    public Transform RightElbow;
    public Transform RightHand;

    public Transform LeftThigh;
    public Transform LeftKnee;
    public Transform LeftFoot;

    public Transform RightThigh;
    public Transform RightKnee;
    public Transform RightFoot;

    public Transform BodyRoot;
    public GameObject OffsetNode;

    public bool UseLeapHands = false;
    public GameObject RightHandTarget = null;
    public GameObject LeftHandTarget = null;

    protected Animator animator;

    private Transform leftFingers = null;
    private Transform rightFingers = null;
    private Transform leftToes = null;
    private Transform rightToes = null;

    public void Start()
    {
        this.animator = this.GetComponent<Animator>();
    }

    /// <summary>
    /// Used for debug only.
    /// </summary>
    public void OnDrawGizmos()
    {
        if (this.RightHandTarget != null)
        {
            Gizmos.DrawSphere(this.RightHandTarget.transform.position, 0.05f);
        }

        if (this.LeftHandTarget != null)
        {
            Gizmos.DrawSphere(this.LeftHandTarget.transform.position, 0.05f);
        }
    }

    /// <summary>
    /// Checks if the leap is active.
    /// </summary>
    /// <param name="boneIndex">Index of the bone.</param>
    /// <returns>boolean b</returns>
    public bool CheckLeap(int boneIndex)
    {
        if (this.UseLeapHands)
        {
            if (this.RightHandTarget.activeSelf
                && this.RightHandTarget.activeInHierarchy
                && (boneIndex >= 4 && boneIndex <= 13))
            {
                return true;
            }

            if (this.LeftHandTarget.activeSelf
                && this.LeftHandTarget.activeInHierarchy
                && (boneIndex >= 4 && boneIndex <= 13))
            {
                return true;
            }
        }

        return false;
    }

    // Update the avatar each frame.
    public override void UpdateAvatar(uint userID)
    {
        if (!transform.gameObject.activeInHierarchy)
        {
            return;
        }

        // Get the KinectManager instance
        if (this.kinectManager == null)
        {
            this.kinectManager = KinectManager.Instance;
        }

        // move the avatar to its Kinect position
        this.MoveAvatar(userID);

        for (var boneIndex = 0; boneIndex < bones.Length; boneIndex++)
        {
            if (!this.CheckLeap(boneIndex))
            {
                if (!this.bones[boneIndex])
                {
                    continue;
                }

                if (boneIndex2JointMap.ContainsKey(boneIndex))
                {
                    KinectWrapper.NuiSkeletonPositionIndex joint = !this.mirroredMovement ? boneIndex2JointMap[boneIndex] : boneIndex2MirrorJointMap[boneIndex];
                    this.TransformBone(userID, joint, boneIndex, !this.mirroredMovement);
                }
                else if (specIndex2JointMap.ContainsKey(boneIndex))
                {
                    // special bones (clavicles)
                    List<KinectWrapper.NuiSkeletonPositionIndex> alJoints = !mirroredMovement ? specIndex2JointMap[boneIndex] : specIndex2MirrorJointMap[boneIndex];
                }
            }
        }
    }

    // If the bones to be mapped have been declared, map that bone to the model.
    protected override void MapBones()
    {
        this.bones[0] = this.HipCenter;
        this.bones[1] = this.Spine;
        this.bones[2] = this.Neck;
        this.bones[3] = this.Head;

        this.bones[4] = this.LeftClavicle;
        this.bones[5] = this.LeftUpperArm;
        this.bones[6] = this.LeftElbow;
        this.bones[7] = this.LeftHand;
        this.bones[8] = this.leftFingers;

        this.bones[9] = this.RightClavicle;
        this.bones[10] = this.RightUpperArm;
        this.bones[11] = this.RightElbow;
        this.bones[12] = this.RightHand;
        this.bones[13] = this.rightFingers;

        this.bones[14] = this.LeftThigh;
        this.bones[15] = this.LeftKnee;
        this.bones[16] = this.LeftFoot;
        this.bones[17] = this.leftToes;

        this.bones[18] = this.RightThigh;
        this.bones[19] = this.RightKnee;
        this.bones[20] = this.RightFoot;
        this.bones[21] = this.rightToes;

        // body root and offset
        this.bodyRoot = this.BodyRoot;
        this.offsetNode = this.OffsetNode;

        if (this.offsetNode == null)
        {
            this.offsetNode = new GameObject(name + "Ctrl")
            {
                layer = transform.gameObject.layer,
                tag = transform.gameObject.tag
            };
            this.offsetNode.transform.position = transform.position;
            this.offsetNode.transform.rotation = transform.rotation;
            this.offsetNode.transform.parent = transform.parent;

            this.transform.parent = offsetNode.transform;
            this.transform.localPosition = Vector3.zero;
            this.transform.localRotation = Quaternion.identity;
        }
    }
}