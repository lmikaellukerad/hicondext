using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leap.Unity;
using UnityEngine;

/// <summary>
/// Starting strategy when no grab has been confirmed.
/// </summary>
public class NeutralGrab : GrabStrategy
{
    
    public NeutralGrab(HandModel left, HandModel right)
    {
        this.left = left;
        this.right = right;
    }

    public override void Destroy()
    {
    }

    public override void ConstrainHands(List<Transform> grabbingFingers)
    {
    }

    public override void UpdateObject()
    {
    }
}