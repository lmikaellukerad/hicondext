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
    protected GameObject obj;
    private List<Transform> clampedFingers = new List<Transform>(); 
    private GameObject root;

    /// <summary>
    /// Destroys the root of the object.
    /// </summary>
    public override void Destroy()
    {
        this.root.transform.GetChild(0).transform.parent = this.root.transform.parent;
        GameObject.Destroy(this.root);
        this.hand.GetComponent<GrabHandSimulator>().ResetFingerLimits();
    }

    /// <summary>
    /// Constrains the hands to prevent them from passing through the grabbed object.
    /// </summary>
    /// <param name="grabbingFingers">The grabbing fingers.</param>
    public override void ConstrainHands(List<Transform> grabbingFingers)
    {
        List<Transform> newFingers = grabbingFingers.Except(this.clampedFingers).ToList();
        List<Transform> removedFingers = this.clampedFingers.Except(grabbingFingers).ToList();

        this.AddClampFingers(this.hand, newFingers);
        this.RemoveClampFingers(this.hand, removedFingers);

        this.clampedFingers.Clear();
        this.clampedFingers.AddRange(grabbingFingers);
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
        this.root.transform.parent = this.obj.transform.parent;
        this.root.transform.position = this.hand.palm.position;
        this.root.transform.rotation = this.hand.palm.rotation;
        this.obj.transform.parent = this.root.transform;
    }
}
