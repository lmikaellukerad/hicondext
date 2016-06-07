using System.Collections;
using UnityEngine;

public class SubjectGrab : SubjectGrabBehaviour
{
    /// <summary>
    /// Initializes this instance.
    /// </summary>
    public override void Initialize()
    {
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
