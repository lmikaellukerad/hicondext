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
        this.State = new NeutralSubjectState();
        this.fingers = this.LeftHand.GetComponent<HandSimulator>().FingerTipTransforms.ToList<Transform>();
        this.fingers.AddRange(this.RightHand.GetComponent<HandSimulator>().FingerTipTransforms.ToList<Transform>());
    }

    /// <summary>
    /// Detects new grabs.
    /// </summary>
    public override void Detect()
    {
        Transform[] leftFingers = this.LeftHand.GetComponent<HandSimulator>().FingerTipTransforms;
        Transform[] rightFingers = this.RightHand.GetComponent<HandSimulator>().FingerTipTransforms;
        this.DetectGrab(leftFingers, rightFingers);
        if (Grabs.Count != 0 && this.State.GetType() == typeof(NeutralSubjectState))
        {
            this.State = new HoldSubjectState();
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

    /// <summary>
    /// Detects if a new object is touched if so creates a new GrabObserver.
    /// </summary>
    /// <param name="leftFingers">The left fingers.</param>
    /// <param name="rightFingers">The right fingers.</param>
    private void DetectGrab(Transform[] leftFingers, Transform[] rightFingers)
    {
        HashSet<GameObject> touched = new HashSet<GameObject>();
        this.AddCurrentGrabbedObjects(touched);
        this.CheckFingers(leftFingers, touched);
        this.CheckFingers(rightFingers, touched);
    }

    /// <summary>
    /// Adds the current grabbed objects to the touched HashSet.
    /// </summary>
    /// <param name="touched">The touched set of objects.</param>
    private void AddCurrentGrabbedObjects(HashSet<GameObject> touched)
    {
        foreach (GrabObserver g in this.Grabs)
        {
            touched.Add(g.Obj);
        }
    }

    /// <summary>
    /// Checks the fingers for touched objects.
    /// </summary>
    /// <param name="fingers">The fingers.</param>
    /// <param name="touched">The touched set of objects.</param>
    private void CheckFingers(Transform[] fingers, HashSet<GameObject> touched)
    {
        foreach (Transform f in fingers)
        {
            this.CheckFinger(f, touched);
        }
    }

    /// <summary>
    /// Checks if the finger touches a new object if so creates a GrabObserver.
    /// </summary>
    /// <param name="f">The finger.</param>
    /// <param name="touched">The touched objects.</param>
    private void CheckFinger(Transform f, HashSet<GameObject> touched)
    {
        DetectFingerCollision d = f.GetComponent<DetectFingerCollision>();
        if (d.CheckFinger())
        {
            GameObject o = d.LastCollider.gameObject;
            if (!touched.Contains(o))
            {
                new GrabObserver(this, this.LeftHand, this.RightHand, o);
            }

            touched.Add(o);
        }
    }
}
