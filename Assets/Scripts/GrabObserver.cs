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
    private List<Transform> thumbs = new List<Transform>();
    public GameObject obj;
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
            offset = obj.transform.position - AveragePosition(grabbingFingerTips);
            obj.GetComponent<Rigidbody>().isKinematic = true;
            this.sub.Subscribe(this);
        }
    }

    /// <summary>
    /// Receive a notification from the subject.
    /// </summary>
    public void Notify()
    {
        Vector3 averageBefore = AveragePosition(grabbingFingerTips);
        if (this.CheckGrabbed())
        {
            Vector3 averageAfter = AveragePosition(grabbingFingerTips);
            offset = offset + averageBefore - averageAfter;
            this.ConstrainHands();
            this.UpdateObject();
        }
        else
        {
            obj.GetComponent<Rigidbody>().isKinematic = false;
            this.ReleaseHands();
            this.sub.UnSubscribe(this);
        }
    }

    private void ReleaseHands()
    {
        rightHand.GetComponent<GrabHandSimulator>().ResetFingerLimits();
        leftHand.GetComponent<GrabHandSimulator>().ResetFingerLimits();
        rightHand.GetComponent<IKGrabConstrain>().ReferencePoint = null;
        rightHand.GetComponent<IKGrabConstrain>().Constrain = false;
        leftHand.GetComponent<IKGrabConstrain>().ReferencePoint = null;
        leftHand.GetComponent<IKGrabConstrain>().Constrain = true;
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
            DetectCollision tip = this.allFingerTips[i].GetComponent<DetectCollision>();
            if (tip.Collided && tip.Collision.gameObject == this.obj)
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
            return true;
        }
        else if (grabbingFingerTips.Intersect(rightFingers).Count() > 1 && grabbingFingerTips.Intersect(thumbs).Count() > 0)
        {
            return true;
        }
        else if (grabbingFingerTips.Intersect(leftFingers).Count() > 1 && grabbingFingerTips.Intersect(thumbs).Count() > 0)
        {
            return true;
        }

        return false;
    }

    private void ConstrainHands()
    {
        List<Transform> rightFingers = rightHand.GetComponent<HandSimulator>().FingerTipTransforms.ToList();
        List<Transform> leftFingers = leftHand.GetComponent<HandSimulator>().FingerTipTransforms.ToList();
        List<Transform> grabbingRight = grabbingFingerTips.Intersect(rightFingers).ToList();
        List<Transform> grabbingLeft = grabbingFingerTips.Intersect(leftFingers).ToList();

        if (grabbingRight.Count > 0)
        {
            for (int i = 0; i < grabbingRight.Count; i++)
            {
                rightHand.GetComponent<GrabHandSimulator>().ClampMin(grabbingRight[i]);
            }
        }

        if (grabbingLeft.Count > 0)
        {
            for (int i = 0; i < grabbingLeft.Count; i++)
            {
                leftHand.GetComponent<GrabHandSimulator>().ClampMin(grabbingLeft[i]);
            }
        }

        if (grabbingRight.Count > 0 && grabbingLeft.Count > 0)
        {
            rightHand.GetComponent<IKGrabConstrain>().ReferencePoint = obj.transform;
            rightHand.GetComponent<IKGrabConstrain>().Constrain = false;
            leftHand.GetComponent<IKGrabConstrain>().ReferencePoint = obj.transform;
            leftHand.GetComponent<IKGrabConstrain>().Constrain = false;
        }
    }

    /// <summary>
    /// Updates the object if it is still grabbed.
    /// </summary>
    private void UpdateObject()
    {
        this.obj.transform.position = this.AveragePosition(this.grabbingFingerTips) + offset;

        List<Transform> rightFingers = rightHand.GetComponent<HandSimulator>().FingerTipTransforms.ToList();
        List<Transform> leftFingers = leftHand.GetComponent<HandSimulator>().FingerTipTransforms.ToList();
        List<Transform> grabbingRight = grabbingFingerTips.Intersect(rightFingers).ToList();
        List<Transform> grabbingLeft = grabbingFingerTips.Intersect(leftFingers).ToList();


        if (grabbingRight.Count > 0 && grabbingLeft.Count > 0)
        {

        } 
        else if (grabbingRight.Count > 0)
        {
        }
        else if (grabbingLeft.Count > 0)
        {
        }

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
