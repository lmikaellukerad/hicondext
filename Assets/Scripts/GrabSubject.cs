using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Leap.Unity;
using UnityEngine;

public class GrabSubject : GrabSubjectBehaviour
{
    public HandModel LeftHand;
    public HandModel RightHand;
    private List<Transform> fingers;

    /// <summary>
    /// Initializes this instance.
    /// </summary>
    public override void Initialize()
    {
        this.fingers = this.LeftHand.fingers.Cast<Transform>().ToList<Transform>();
        this.fingers.AddRange(this.RightHand.fingers.Cast<Transform>().ToList<Transform>());
    }

    /// <summary>
    /// Detects new grabs.
    /// </summary>
    public override void Detect()
    {
        Transform[] leftFingers = LeftHand.GetComponent<HandSimulator>().FingerTipTransforms;
        Transform[] rightFingers = RightHand.GetComponent<HandSimulator>().FingerTipTransforms;
        this.DetectGrab(leftFingers, rightFingers);

        this.State = new HoldSubjectState();
    }

    private void DetectGrab(Transform[] leftFingers, Transform[] rightFingers)
    {
        HashSet<GameObject> touched = new HashSet<GameObject>();
        CheckFingers(leftFingers, touched);
        CheckFingers(rightFingers, touched);
    }

    private void CheckFingers(Transform[] fingers, HashSet<GameObject> touched)
    {
        foreach (Transform f in fingers)
        {
            CheckFinger(f, touched);
        }
    }
    
    private void CheckFinger(Transform f, HashSet<GameObject> touched)
    {
        DetectCollision d = f.GetComponent<DetectCollision>();
        if (d.Collided)
        {
            GameObject o = d.Collision.gameObject;
            if (!touched.Contains(o))
            {
                new GrabObserver(this, LeftHand, RightHand, o);
            }
            touched.Add(o);
        }
    }

    /// <summary>
    /// Notifies all grabs.
    /// If we no longer have grabs to notify switch to the neutral state.
    /// </summary>
    public override void NotifyGrabs()
    {
        for (int i = this.Grabs.Count - 1; i >= 0; i--)
        {
            this.Grabs[i].Notify();
        }

        if (Grabs.Count == 0)
        {
            this.State = new NeutralSubjectState();
        }
    }
}
