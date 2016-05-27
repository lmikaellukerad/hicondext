using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// This code is used to detect any collisions between the fingertips and other objects.
/// </summary>
public class FingerTipCollider : MonoBehaviour
{
    public Transform[] FingerTips = new Transform[4];

    public Transform Thumb;
    public PhysicMaterial Material;

    /// <summary>
    /// Starts this instance.
    /// </summary>
    private void Start()
    {
        this.Initialize();
    }

    /// <summary>
    /// This method is used to add a SphereCollider to a GameObject.
    /// </summary>
    /// <param name="obj">The object to add a SphereCollider to.</param>
    private void AddCollider(GameObject obj)
    {
        SphereCollider s = obj.AddComponent<SphereCollider>();
        s.material = this.Material;
        s.radius = 0.03f;
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
        this.AddCollider(obj);
        this.AddCollisionDetection(obj);
        this.AddRigidbody(obj);
    }

    /// <summary>
    /// This method is used to get the next part of the finger.
    /// </summary>
    /// <param name="obj">The object to initialize.</param>
    private void GetNextBone(GameObject obj)
    {
        this.InitializeFingerCollider(obj);
        if (obj.transform.parent.name != "Palm" && obj.transform.parent.name != "Palm 1")
        {
            this.GetNextBone(obj.transform.parent.gameObject);
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// Initializes this instance.
    /// </summary>
    private void Initialize()
    {
        foreach (Transform t in this.FingerTips)
        {
            this.GetNextBone(t.gameObject);
        }

        if (this.Thumb != null)
        {
            this.GetNextBone(this.Thumb.gameObject);
        }
    }
}
