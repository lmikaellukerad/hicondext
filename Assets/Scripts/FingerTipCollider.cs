using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// This code is used to add colliders to finger models.
/// </summary>
public class FingerTipCollider : MonoBehaviour
{
    /// <summary>
    /// Sets the colliders for an array of fingertip transforms.
    /// </summary>
    /// <param name="tips">The transforms of the fingertips.</param>
    public void SetColliders(Transform[] tips)
    {
        foreach (Transform t in tips)
        {
            this.InitializeFingerCollider(t.gameObject);
        }
    }

    /// <summary>
    /// This method is used to add a DetectFingerCollision script to part of the finger
    /// </summary>
    /// <param name="obj">The object to add a RigidBody to.</param>
    private void InitializeFingerCollider(GameObject obj)
    {
        if (obj.name.Contains("end"))
        {
            obj.AddComponent<DetectFingerCollision>();
        }
    }
}
