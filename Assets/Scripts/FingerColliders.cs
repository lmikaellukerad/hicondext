﻿using System.Collections;
using UnityEngine;

/// <summary>
/// Author: Luke
/// Adds colliders to fingers.
/// </summary>
public class FingerColliders
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FingerColliders"/> class.
    /// </summary>
    /// <param name="fingerTips">The finger tips.</param>
    public FingerColliders(Transform[] fingerTips)
    {
        foreach (Transform tip in fingerTips)
        {
            NextJoint(tip.gameObject);
        }
    }

    /// <summary>
    /// Creates the collider.
    /// </summary>
    /// <param name="joint">The joint.</param>
    /// <param name="nextJoint">The next joint.</param>
    private void CreateCollider(GameObject joint, GameObject nextJoint)
    {
        CapsuleCollider collider = joint.AddComponent<CapsuleCollider>();
        collider.enabled = false;
        float offset = (joint.transform.position - nextJoint.transform.position).magnitude;
        float radius = 0.03f;
        collider.center = new Vector3(0, -(offset + 2 * radius)/2, 0);
        collider.radius = radius;
        collider.height = offset + 2 * radius;
        collider.direction = 1;
    }

    /// <summary>
    /// Creates the collider.
    /// </summary>
    /// <param name="joint">The joint.</param>
    private void CreateCollider(GameObject joint)
    {
        SphereCollider collider = joint.AddComponent<SphereCollider>();
        float radius = 0.03f;
        collider.radius = radius;
    }

    /// <summary>
    /// Nexts the joint.
    /// </summary>
    /// <param name="joint">The joint.</param>
    private void NextJoint(GameObject joint)
    {
        if (joint.transform.parent.gameObject.name.Contains("Palm"))
        {
            return;
        }
        else
        {
            GameObject parent = joint.transform.parent.gameObject;
            CreateCollider(joint);
            NextJoint(parent);
        }
    }

}