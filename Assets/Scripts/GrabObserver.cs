using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Leap.Unity;
using UnityEngine;

/// <summary>
/// The grab observer handles a single grab.
/// </summary>
public class GrabObserver
{
    private GrabSubjectBehaviour sub;
    private List<Transform> allFingerTips;
    private List<Transform> grabbingFingerTips = new List<Transform>();
    private GameObject obj;
    private HandModel leftHand;
    private HandModel rightHand;

    /// <summary>
    /// Initializes a new instance of the <see cref="GrabObserver"/> class.
    /// </summary>
    /// <param name="subject">The subject to subscribe to.</param>
    /// <param name="right">The right hand model.</param>
    /// <param name="left">The left hand model.</param>
    /// <param name="obj">The object that is currently grabbed.</param>
    public GrabObserver(GrabSubjectBehaviour subject, HandModel right, HandModel left, GameObject obj)
    {
        this.sub = subject;
        this.rightHand = right;
        this.leftHand = right;
        this.allFingerTips = this.leftHand.fingers.Cast<Transform>().ToList<Transform>();
        this.allFingerTips.AddRange(this.rightHand.fingers.Cast<Transform>().ToList<Transform>());
        this.obj = obj;
        if (this.CheckGrabbed())
        {
            this.sub.Subscribe(this);
        }
    }

    /// <summary>
    /// Receive a notification from the subject.
    /// </summary>
    public void Notify()
    {
        if (this.CheckGrabbed())
        {
            this.UpdateObject();
        }
        else
        {
            this.sub.UnSubscribe(this);
        }
    }

    /// <summary>
    /// Checks if the object is still grabbed.
    /// </summary>
    /// <returns>True if the object is still grabbed</returns>
    private bool CheckGrabbed()
    {
        return false;
    }

    /// <summary>
    /// Updates the object if it is still grabbed.
    /// </summary>
    private void UpdateObject()
    {
    }
}
