﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Leap.Unity;
using UnityEngine;

/// <summary>
/// The grab observer handles a single grab.
/// </summary>
public class GrabObserver
{
    public GameObject grabbedObject;
    private GrabSubjectBehaviour subject;
    private List<Transform> allFingerTips;
    private List<Transform> grabbingFingerTips = new List<Transform>();
    private List<Transform> thumbs = new List<Transform>();
    private GrabStrategy strategy;
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
        this.subject = subject;
        this.rightHand = right;
        this.leftHand = left;
        this.grabbedObject = obj;
        this.Init();
        if (this.CheckGrabbed())
        {
            this.previous = obj.transform.position;
            this.grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            this.subject.Subscribe(this);
        }
    }

    /// <summary>
    /// Receive a notification from the subject.
    /// </summary>
    public void Notify()
    {
        this.strategy.UpdateObject();
        this.strategy.ConstrainHands(this.grabbingFingerTips);
        if (!this.CheckGrabbed())
        {
            this.grabbedObject.GetComponent<Rigidbody>().velocity = (this.grabbedObject.transform.position - this.previous) / Time.deltaTime;
            this.grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            this.rightHand.GetComponent<GrabHandSimulator>().ResetFingerLimits();
            this.leftHand.GetComponent<GrabHandSimulator>().ResetFingerLimits();
            this.strategy.Destroy();
            this.subject.UnSubscribe(this);
        }
        this.previous = this.grabbedObject.transform.position;
    }

    private void Init()
    {
        this.strategy = new NeutralGrab(leftHand, rightHand);
        this.thumbs.Add(this.rightHand.GetComponent<HandSimulator>().FingerTipTransforms[0]);
        this.thumbs.Add(this.leftHand.GetComponent<HandSimulator>().FingerTipTransforms[0]);
        this.allFingerTips = this.leftHand.GetComponent<HandSimulator>().FingerTipTransforms.ToList<Transform>();
        this.allFingerTips.AddRange(this.rightHand.GetComponent<HandSimulator>().FingerTipTransforms.ToList<Transform>());
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
            if (tip.CheckFinger() && tip.LastCollider.gameObject == this.grabbedObject)
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

        if (!leftOpen 
            && !rightOpen 
            && this.grabbingFingerTips.Intersect(rightFingers).Count() > 0 
            && this.grabbingFingerTips.Intersect(leftFingers).Count() > 0)
        {
            this.strategy = this.strategy.DoubleHand(this.grabbedObject);
            return true;
        }

        if (!leftOpen 
            && this.grabbingFingerTips.Intersect(leftFingers).Count() > 1 
            && this.grabbingFingerTips.Intersect(this.thumbs).Count() > 0)
        {
            this.strategy = this.strategy.LeftHand(this.grabbedObject);
            return true;
        }

        if (!rightOpen 
            && this.grabbingFingerTips.Intersect(rightFingers).Count() > 1 
            && this.grabbingFingerTips.Intersect(this.thumbs).Count() > 0)
        {
            this.strategy = this.strategy.RightHand(this.grabbedObject);
            return true;
        }

        return false;
    }
}
