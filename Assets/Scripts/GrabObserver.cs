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
    public GrabObserver(GrabSubjectBehaviour subject, HandModel left, HandModel right, GameObject obj)
    {
        this.sub = subject;
        this.rightHand = right;
        this.leftHand = left;
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
        this.grabbingFingerTips.Clear();
        List<Vector3> normals = new List<Vector3>();
        for (int i = 0; i < this.allFingerTips.Count; i++)
        {
            DetectCollision tip = this.allFingerTips[i].GetComponent<DetectCollision>();
            if (tip.Collided && tip.Collision.gameObject == this.obj)
            {
                this.grabbingFingerTips.Add(this.allFingerTips[i]);
                normals.Add(Vector3.Normalize(tip.Collision.contacts[0].normal));
            }
        }

        if (this.HasOpposites(normals))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Determines whether the specified vectors has opposites.
    /// </summary>
    /// <param name="vectors">The vectors.</param>
    /// <returns>True if the list has two opposite vectors</returns>
    private bool HasOpposites(List<Vector3> vectors)
    {
        foreach (Vector3 n in vectors)
        {
            foreach (Vector3 m in vectors)
            {
                if (n != m)
                {
                    float d = Vector3.Dot(n, m);
                    if (d < 0.5)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Updates the object if it is still grabbed.
    /// </summary>
    private void UpdateObject()
    {
        this.obj.transform.position = this.AveragePosition(this.grabbingFingerTips);
    }

    /// <summary>
    /// Averages the position of the transforms.
    /// </summary>
    /// <param name="transforms">The transforms.</param>
    /// <returns>The average position vector</returns>
    private Vector3 AveragePosition(List<Transform> transforms)
    {
        Vector3 total = Vector3.zero;
        foreach (Transform trans in transforms)
        {
            total += trans.position;
        }

        return total / transforms.Count;
    }
}
