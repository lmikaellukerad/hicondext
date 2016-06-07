using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Leap.Unity;
using UnityEngine;

public class SubjectGrab : SubjectGrabBehaviour
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
        this.State = new SubjectHoldState();
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
            this.State = new SubjectNeutralState();
        }
    }
}
