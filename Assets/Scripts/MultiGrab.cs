using Leap.Unity;
using System;
using System.Collections;
using UnityEngine;


public class MultiGrab : SubjectGrabBehaviour
{
    public HandModel LeftHand;
    public HandModel RightHand;

    public override void Detect()
    {
        throw new NotImplementedException();
    }

    public override void Initialize()
    {
        throw new NotImplementedException();
    }

    public override void NotifyGrabs()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Starts this instance.
    /// </summary>
    public void Start()
    {
    }
    
    /// <summary>
    /// Updates this instance.
    /// </summary>
    public void Update()
    {
    }
}
