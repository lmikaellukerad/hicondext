using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leap.Unity;
using UnityEngine;

/// <summary>
/// Strategy that handles a grab with two hands.
/// </summary>
public class DoubleGrab : GrabStrategy
{
    private HandModel right;
    private HandModel left;
    private GameObject obj;
    private GameObject root;
    private List<Transform> clampedFingers = new List<Transform>(); 

    /// <summary>
    /// Initializes a new instance of the <see cref="DoubleGrab"/> class.
    /// </summary>
    /// <param name="right">The right hand.</param>
    /// <param name="left">The left hand.</param>
    /// <param name="obj">The object which has been grabbed.</param>
    public DoubleGrab(HandModel right, HandModel left, GameObject obj)
    {
        this.right = right;
        this.left = left;
        this.obj = obj;
        this.root = new GameObject("root");
        this.root.transform.parent = this.obj.transform.parent;
        Vector3 averageHandPos = this.AveragePosition(new List<Transform> { this.right.palm.transform, this.left.palm.transform });
        this.root.transform.position = averageHandPos;
        this.root.transform.rotation = Quaternion.LookRotation(this.left.palm.position - averageHandPos);
        this.obj.transform.parent = this.root.transform;
    }

    /// <summary>
    /// Constrains the hands to prevent them from passing through the grabbed object.
    /// </summary>
    /// <param name="grabbingFingers">The grabbing fingers.</param>
    public override void ConstrainHands(List<Transform> grabbingFingers)
    {
        List<Transform> newFingers = grabbingFingers.Except(this.clampedFingers).ToList();
        List<Transform> removedFingers = this.clampedFingers.Except(grabbingFingers).ToList();

        this.AddClampFingers(this.right, newFingers);
        this.RemoveClampFingers(this.right, removedFingers);

        this.AddClampFingers(this.left, newFingers);
        this.RemoveClampFingers(this.left, removedFingers);

        this.clampedFingers.Clear();
        this.clampedFingers.AddRange(grabbingFingers);
    }

    /// <summary>
    /// Updates the current object position and rotation.
    /// </summary>
    public override void UpdateObject()
    {
        Vector3 averageHandPos = this.AveragePosition(new List<Transform> { this.right.palm.transform, this.left.palm.transform });
        this.root.transform.position = averageHandPos;
        this.root.transform.rotation = Quaternion.LookRotation(this.left.palm.position - averageHandPos);
    }

    /// <summary>
    /// Destroys the root of the grabbed object.
    /// </summary>
    public override void Destroy()
    {
        this.root.transform.GetChild(0).transform.parent = this.root.transform.parent;
        GameObject.Destroy(this.root);
        this.right.GetComponent<GrabHandSimulator>().ResetFingerLimits();
        this.left.GetComponent<GrabHandSimulator>().ResetFingerLimits();
    }
}
