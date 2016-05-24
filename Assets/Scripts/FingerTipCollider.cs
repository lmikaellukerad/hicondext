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
        s.radius = 0.025f;
    }

    /// <summary>
    /// Initializes this instance.
    /// </summary>
    private void Initialize()
    {
        foreach (Transform t in this.FingerTips)
        {
            this.AddCollider(t.gameObject);
        }

        if (this.Thumb != null)
        {
            this.AddCollider(this.Thumb.gameObject);
        }
    }

    /// <summary>
    /// Updates this instance for every frame.
    /// </summary>
    private void Update()
    {
    }
}
