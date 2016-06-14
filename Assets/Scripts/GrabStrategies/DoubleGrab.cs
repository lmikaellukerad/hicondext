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
        this.obj.gameObject.layer = 10;
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
        this.HandleClamps(grabbingFingers);

        this.right.GetComponent<IKGrabConstrain>().ConstrainedUpdate(this.obj.transform);
        this.left.GetComponent<IKGrabConstrain>().ConstrainedUpdate(this.obj.transform);
    }

    /// <summary>
    /// Updates the current object position and rotation.
    /// </summary>
    public override void UpdateObject()
    {
        Vector3 newPos = this.AveragePosition(new List<Transform> { this.right.palm.transform, this.left.palm.transform });

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
        this.obj.gameObject.layer = 0;
        this.right.GetComponent<IKGrabConstrain>().Reset();
        this.left.GetComponent<IKGrabConstrain>().Reset();
    }

    /// <summary>
    /// Averages the position of the transforms.
    /// </summary>
    /// <param name="transforms">The transforms.</param>
    /// <returns>The average position vector</returns>
    protected Vector3 AveragePosition(List<Transform> transforms)
    {
        Vector3 total = Vector3.zero;
        foreach (Transform trans in transforms)
        {
            total += trans.position;
        }

        return total / transforms.Count;
    }
}
