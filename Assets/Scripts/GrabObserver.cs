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
    public GameObject Obj;
    private GrabSubjectBehaviour sub;
    private List<Transform> allFingerTips;
    private List<Transform> grabbingFingerTips = new List<Transform>();
    private List<Transform> thumbs = new List<Transform>();
    private GrabStrategy strategy = new NeutralGrab();
    private Vector3 offset;
    private Vector3 previous;
    private HandModel leftHand;
    private HandModel rightHand;

    /// <summary>
    /// Initializes a new instance of the <see cref="GrabObserver"/> class.
    /// </summary>
    /// <param name="subject">The subject to subscribe to.</param>
    /// <param name="left">The left hand model.</param>
    /// <param name="right">The right hand model.</param>
    /// <param name="obj">The object that is currently grabbed.</param>
    public GrabObserver(GrabSubjectBehaviour subject, HandModel left, HandModel right, GameObject obj)
    {
        this.sub = subject;
        this.rightHand = right;
        this.leftHand = left;
        this.thumbs.Add(this.rightHand.GetComponent<HandSimulator>().FingerTipTransforms[0]);
        this.thumbs.Add(this.leftHand.GetComponent<HandSimulator>().FingerTipTransforms[0]);
        this.allFingerTips = this.leftHand.GetComponent<HandSimulator>().FingerTipTransforms.ToList<Transform>();
        this.allFingerTips.AddRange(this.rightHand.GetComponent<HandSimulator>().FingerTipTransforms.ToList<Transform>());
        this.Obj = obj;
        if (this.CheckGrabbed())
        {
            this.previous = obj.transform.position;
            this.Obj.GetComponent<Rigidbody>().isKinematic = true;
            this.sub.Subscribe(this);
        }
    }

    /// <summary>
    /// Receive a notification from the subject.
    /// </summary>
    public void Notify()
    {
        this.strategy.UpdateObject();
        bool grabbed = this.CheckGrabbed();
        this.strategy.ConstrainHands(this.grabbingFingerTips);
        if (!grabbed)
        {
            this.Obj.GetComponent<Rigidbody>().velocity = (this.Obj.transform.position - this.previous) / Time.deltaTime;
            this.Obj.GetComponent<Rigidbody>().isKinematic = false;
            this.rightHand.GetComponent<GrabHandSimulator>().ResetFingerLimits();
            this.leftHand.GetComponent<GrabHandSimulator>().ResetFingerLimits();
            this.strategy.Destroy();
            this.sub.UnSubscribe(this);
        }
        this.previous = this.Obj.transform.position;
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
            if (tip.CheckFinger() && tip.LastCollider.gameObject == this.Obj)
            {
                this.grabbingFingerTips.Add(this.allFingerTips[i]);
            }
        }

        if (this.IsValidGrab())
        {
            return true;
        }

        this.grabbingFingerTips.Clear();
        return false;
    }

    /// <summary>
    /// Determines whether the current grabbing fingers make a valid grab.
    /// </summary>
    /// <returns>True if the list has two opposite vectors</returns>
    private bool IsValidGrab()
    {
        List<Transform> rightFingers = this.rightHand.GetComponent<HandSimulator>().FingerTipTransforms.ToList();
        List<Transform> leftFingers = this.leftHand.GetComponent<HandSimulator>().FingerTipTransforms.ToList();
        bool leftOpen = this.leftHand.GetComponent<GrabHandSimulator>().AllFingersOpen();
        bool rightOpen = this.rightHand.GetComponent<GrabHandSimulator>().AllFingersOpen();

        if (!leftOpen && !rightOpen && this.grabbingFingerTips.Intersect(rightFingers).Count() > 0 && this.grabbingFingerTips.Intersect(leftFingers).Count() > 0)
        {
            if (this.strategy.GetType() != typeof(DoubleGrab))
            {
                this.strategy.Destroy();
                this.strategy = new DoubleGrab(this.leftHand, this.rightHand, this.Obj);
            }

            return true;
        }
        else if (! leftOpen && this.grabbingFingerTips.Intersect(leftFingers).Count() > 1 && this.grabbingFingerTips.Intersect(this.thumbs).Count() > 0)
        {
            if (this.strategy.GetType() != typeof(LeftGrab))
            {
                this.strategy.Destroy();
                this.strategy = new LeftGrab(this.leftHand, this.rightHand, this.Obj);
            }

            return true;
        }
        else if (!rightOpen && this.grabbingFingerTips.Intersect(rightFingers).Count() > 1 && this.grabbingFingerTips.Intersect(this.thumbs).Count() > 0)
        {
            if (this.strategy.GetType() != typeof(RightGrab))
            {
                this.strategy.Destroy();
                this.strategy = new RightGrab(this.leftHand, this.rightHand, this.Obj);
            }

            return true;
        }

        return false;
    }
}
