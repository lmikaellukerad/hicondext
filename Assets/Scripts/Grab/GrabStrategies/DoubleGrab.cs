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
    private GameObject grabbedObject;
    private GameObject root;

    /// <summary>
    /// Initializes a new instance of the <see cref="DoubleGrab"/> class.
    /// </summary>
    /// <param name="right">The right hand.</param>
    /// <param name="left">The left hand.</param>
    /// <param name="obj">The object which has been grabbed.</param>
    public DoubleGrab(HandModel left, HandModel right, GameObject obj)
    {
        this.right = right;
        this.left = left;
        this.grabbedObject = obj;
        Vector3 averageHandPos = this.AveragePosition(this.left, this.right);
        this.root = new GameObject("root");
        this.root.transform.parent = this.grabbedObject.transform.parent;

        this.root.transform.position = averageHandPos;
        this.root.transform.rotation = Quaternion.LookRotation(this.left.palm.position - averageHandPos);
        this.grabbedObject.transform.parent = this.root.transform;
    }

    /// <summary>
    /// Constrains the hands to prevent them from passing through the grabbed object.
    /// </summary>
    /// <param name="grabbingFingers">The grabbing fingers.</param>
    public override void ConstrainHands(List<Transform> grabbingFingers)
    {
        this.HandleClamps(grabbingFingers);
        this.right.GetComponent<InverseKinematicGrabConstraint>().ConstrainedUpdate(this.grabbedObject.transform);
        this.left.GetComponent<InverseKinematicGrabConstraint>().ConstrainedUpdate(this.grabbedObject.transform);
    }

    /// <summary>
    /// Updates the current object position and rotation.
    /// </summary>
    public override void UpdateObject()
    {
        Vector3 newPos = this.AveragePosition(this.left, this.right);
        this.root.transform.position = newPos;
        this.root.transform.rotation = Quaternion.LookRotation(this.left.palm.position - newPos);
    }

    /// <summary>
    /// Destroys the root of the grabbed object.
    /// </summary>
    public override void Destroy()
    {
        this.root.transform.GetChild(0).transform.parent = this.root.transform.parent;
        GameObject.Destroy(this.root);
        this.right.GetComponent<InverseKinematicGrabConstraint>().Reset();
        this.left.GetComponent<InverseKinematicGrabConstraint>().Reset();
    }

    /// <summary>
    /// Switch strategy to a double hand strategy.
    /// </summary>
    /// <param name="grabbedObject">The grabbed object.</param>
    /// <returns>
    /// DoubleGrab strategy.
    /// </returns>
    public override GrabStrategy DoubleHand(GameObject grabbedObject)
    {
        this.grabbedObject = grabbedObject;
        return this;
    }

    /// <summary>
    /// Averages the position of the transforms.
    /// </summary>
    /// <param name="transforms">The transforms.</param>
    /// <returns>The average position vector</returns>
    protected Vector3 AveragePosition(HandModel left, HandModel right)
    {
        return (left.palm.transform.position + right.palm.transform.position) / 2;
    }
}
