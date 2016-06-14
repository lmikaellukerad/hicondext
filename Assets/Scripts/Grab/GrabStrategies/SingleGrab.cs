using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leap.Unity;
using UnityEngine;

/// <summary>
/// Strategy that handles a grab with one hand.
/// </summary>
public abstract class SingleGrab : GrabStrategy
{
    protected HandModel hand;
    protected GameObject grabbedObject;
    private GameObject root;

    /// <summary>
    /// Initializes a new instance of the <see cref="SingleGrab"/> class.
    /// </summary>
    /// <param name="left">The left hand.</param>
    /// <param name="right">The right hand.</param>
    /// <param name="obj">The object.</param>
    public SingleGrab(HandModel left, HandModel right, GameObject obj)
    {
        this.left = left;
        this.right = right;
        this.grabbedObject = obj;
    }
    
    /// <summary>
    /// Destroys the root of the object.
    /// </summary>
    public override void Destroy()
    {
        this.root.transform.GetChild(0).transform.parent = this.root.transform.parent;
        GameObject.Destroy(this.root);
    }

    /// <summary>
    /// Constrains the hands to prevent them from passing through the grabbed object.
    /// </summary>
    /// <param name="grabbingFingers">The grabbing fingers.</param>
    public override void ConstrainHands(List<Transform> grabbingFingers)
    {
        this.HandleClamps(grabbingFingers);

        GrabStrategy.clampedFingers.Clear();
        GrabStrategy.clampedFingers.AddRange(grabbingFingers);
    }

    /// <summary>
    /// Updates the root object his position and rotation.
    /// </summary>
    public override void UpdateObject()
    {
        this.root.transform.position = this.hand.palm.position;
        this.root.transform.rotation = this.hand.palm.rotation;
    }

    /// <summary>
    /// Initializes this instance by adding a root for the object on the palm's position.
    /// </summary>
    protected void Init()
    {
        this.root = new GameObject("root");
        this.root.transform.parent = this.grabbedObject.transform.parent;
        this.root.transform.position = this.hand.palm.position;
        this.root.transform.rotation = this.hand.palm.rotation;
        this.grabbedObject.transform.parent = this.root.transform;
        this.hand.transform.GetComponent<ManusVibrate>().ShortVibration();
    }
}
