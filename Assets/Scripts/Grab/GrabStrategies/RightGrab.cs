using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leap.Unity;
using UnityEngine;

/// <summary>
/// Sets the hand model for the single grab to the right hand.
/// </summary>
/// <seealso cref="SingleGrab"/>
public class RightGrab : SingleGrab
{
    public RightGrab(HandModel left, HandModel right, List<Transform> fingers, GameObject obj) : base(left, right, obj)
    {
        Debug.Log(this.left);
        Debug.Log(this.right);
        Debug.Log(this.hand);
        this.hand = right;
        this.clampedFingers = fingers;
        this.Init();
    }

    /// <summary>
    /// Switch strategy to a right hand strategy.
    /// </summary>
    /// <param name="grabbedObject">The grabbed object.</param>
    /// <returns>
    /// RightGrab strategy.
    /// </returns>
    public override GrabStrategy RightHand(GameObject grabbedObject)
    {
        this.grabbedObject = grabbedObject;
        return this;
    }
}
