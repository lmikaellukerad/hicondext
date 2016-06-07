using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The grab observer handles a single grab.
/// </summary>
public class GrabObserver
{
    private SubjectGrabBehaviour sub;
    private List<Transform> allFingerTips;
    private List<Transform> grabbingFingerTips = new List<Transform>();
    private GameObject obj;

    /// <summary>
    /// Initializes a new instance of the <see cref="GrabObserver"/> class.
    /// </summary>
    /// <param name="subject">The subject to subscribe to.</param>
    /// <param name="fingerTips">The finger tips of all hands.</param>
    /// <param name="obj">The object that is currently grabbed.</param>
    public GrabObserver(SubjectGrabBehaviour subject, List<Transform> fingerTips, GameObject obj)
    {
        this.sub = subject;
        this.grabbingFingerTips = fingerTips;
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
