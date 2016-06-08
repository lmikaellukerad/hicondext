using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// This code is used to add colliders to finger models.
/// </summary>
public class FingerTipCollider : MonoBehaviour
{
    /// <summary>
    /// The physics material for the collider, should be set through the inspector.
    /// </summary>
    public PhysicMaterial Material;

    /// <summary>
    /// Sets the colliders for an array of fingertip transforms.
    /// </summary>
    /// <param name="tips">The transforms of the fingertips.</param>
    public void SetColliders(Transform[] tips)
    {
        foreach (Transform t in tips)
        {
            this.GetNextBone(t.gameObject);
        }
    }

    /// <summary>
    /// This method is used to add a SphereCollider to a GameObject.
    /// </summary>
    /// <param name="obj">The object to add a SphereCollider to.</param>
    private Collider AddCollider(GameObject obj)
    {
        SphereCollider s = obj.AddComponent<SphereCollider>();
        s.material = this.Material;
        s.radius = 0.03f;
        s.isTrigger = false;
        s.isTrigger = true;
        return s;
    }

    /// <summary>
    /// This method is used to add a RigidBody to a GameObject.
    /// </summary>
    /// <param name="obj">The object to add a RigidBody to.</param>
    private void AddRigidbody(GameObject obj)
    {
        Rigidbody r = obj.AddComponent<Rigidbody>();
        r.useGravity = false;
        r.isKinematic = true;
    }

    /// <summary>
    /// This method is used to add a DetectCollision to a GameObject.
    /// </summary>
    /// <param name="obj">The object to add a DetectCollision script to.</param>
    private void AddCollisionDetection(GameObject obj)
    {
        obj.AddComponent<DetectCollision>();
    }

    /// <summary>
    /// This method is used to add a SphereCollider to part of the finger
    /// </summary>
    /// <param name="obj">The object to add a RigidBody to.</param>
    private void InitializeFingerCollider(GameObject obj)
    {
        Collider c = this.AddCollider(obj);
        this.AddCollisionDetection(obj);
        if (obj.name.Contains("end"))
        {
            this.AddCollisionDetection(obj);
            obj.AddComponent<DetectFingerCollision>();
        }
        this.AddRigidbody(obj);
    }

    /// <summary>
    /// This method is used to get the next part of the finger.
    /// </summary>
    /// <param name="obj">The object to initialize.</param>
    private void GetNextBone(GameObject obj)
    {
        this.InitializeFingerCollider(obj);
        if (!obj.transform.parent.parent.name.Contains("Palm"))
        {
            this.GetNextBone(obj.transform.parent.gameObject);
        }
        else
        {
            return;
        }
    }
}
