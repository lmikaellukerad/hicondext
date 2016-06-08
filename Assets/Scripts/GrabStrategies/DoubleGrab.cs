using Leap.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Handles a grab with two hands
/// </summary>
class DoubleGrab : GrabStrategy
{
    private HandModel right;
    private HandModel left;
    private GameObject obj;
    private GameObject root;

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
        root = new GameObject("root");
        root.transform.parent = obj.transform.parent;
        Vector3 averageHandPos = this.AveragePosition(new List<Transform> { right.palm.transform, left.palm.transform });
        root.transform.position = averageHandPos;
        root.transform.rotation = Quaternion.LookRotation(left.palm.position - averageHandPos);
        obj.transform.parent = root.transform;
    }

    /// <summary>
    /// Constrains the hands to prevent them from passing through the grabbed object.
    /// </summary>
    /// <param name="grabbingFingers">The grabbing fingers.</param>
    public override void ConstrainHands(List<Transform> grabbingFingers)
    {
    }

    /// <summary>
    /// Updates the current object position and rotation.
    /// </summary>
    public override void UpdateObject()
    {
        Vector3 averageHandPos = this.AveragePosition(new List<Transform> { right.palm.transform, left.palm.transform });
        root.transform.position = averageHandPos;
        root.transform.rotation = Quaternion.LookRotation(left.palm.position - averageHandPos);
    }

    /// <summary>
    /// Destroys the root of the grabbed object.
    /// </summary>
    public override void Destroy()
    {
        root.transform.GetChild(0).transform.parent = root.transform.parent;
        GameObject.Destroy(root);
        right.GetComponent<GrabHandSimulator>().ResetFingerLimits();
        left.GetComponent<GrabHandSimulator>().ResetFingerLimits();
    }
}
