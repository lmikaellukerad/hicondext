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
    public GameObject obj;
    private GrabSubjectBehaviour sub;
    private List<Transform> allFingerTips;
    private List<Transform> grabbingFingerTips = new List<Transform>();
    private List<Transform> thumbs = new List<Transform>();
    private GrabStrategy strategy = new NeutralGrab();
    private Vector3 offset;
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
        this.thumbs.Add(rightHand.GetComponent<HandSimulator>().FingerTipTransforms[0]);
        this.thumbs.Add(leftHand.GetComponent<HandSimulator>().FingerTipTransforms[0]);
        this.allFingerTips = this.leftHand.GetComponent<HandSimulator>().FingerTipTransforms.ToList<Transform>();
        this.allFingerTips.AddRange(this.rightHand.GetComponent<HandSimulator>().FingerTipTransforms.ToList<Transform>());
        this.obj = obj;
        if (this.CheckGrabbed())
        {
            obj.GetComponent<Rigidbody>().isKinematic = true;
            this.sub.Subscribe(this);
        }
    }

    /// <summary>
    /// Receive a notification from the subject.
    /// </summary>
    public void Notify()
    {
        this.strategy.UpdateObject();
        if (this.CheckGrabbed())
        {
            this.strategy.ConstrainHands(grabbingFingerTips);
        }
        else
        {
            obj.GetComponent<Rigidbody>().isKinematic = false;
            strategy.Destroy();
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
        for (int i = 0; i < this.allFingerTips.Count; i++)
        {
            DetectFingerCollision tip = this.allFingerTips[i].GetComponent<DetectFingerCollision>();
            if (tip.CheckFinger(allFingerTips[i].gameObject) && tip.LastCollider.gameObject == this.obj)
            {
                this.grabbingFingerTips.Add(this.allFingerTips[i]);
            }
        }

        if (this.IsValidGrab())
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
    private bool IsValidGrab()
    {
        List<Transform> rightFingers = rightHand.GetComponent<HandSimulator>().FingerTipTransforms.ToList();
        List<Transform> leftFingers = leftHand.GetComponent<HandSimulator>().FingerTipTransforms.ToList();
        if (grabbingFingerTips.Intersect(rightFingers).Count() > 0 && grabbingFingerTips.Intersect(leftFingers).Count() > 0)
        {
            if (strategy.GetType() != typeof(DoubleGrab))
            {
                strategy.Destroy();
                strategy = new DoubleGrab(leftHand, rightHand, obj);
            }
            return true;
        }
        else if (grabbingFingerTips.Intersect(leftFingers).Count() > 1 && grabbingFingerTips.Intersect(thumbs).Count() > 0)
        {
            if (!leftHand.GetComponent<GrabHandSimulator>().AllFingersOpen(0.2f))
            {
                if (strategy.GetType() != typeof(LeftGrab))
                {
                    strategy.Destroy();
                    strategy = new LeftGrab(leftHand, obj);
                }
                return true;
            }
        }
        else if (grabbingFingerTips.Intersect(rightFingers).Count() > 1 && grabbingFingerTips.Intersect(thumbs).Count() > 0)
        {
            if (!rightHand.GetComponent<GrabHandSimulator>().AllFingersOpen(0.2f))
            {
                if (strategy.GetType() != typeof(RightGrab))
                {
                    strategy.Destroy();
                    strategy = new RightGrab(rightHand, obj);
                }
                return true;
            }
        }
        return false;
    }
}
