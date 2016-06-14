using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leap.Unity;
using UnityEngine;

/// <summary>
/// Sets the hand model for the single grab to the left hand.
/// </summary>
/// <seealso cref="SingleGrab"/>
public class LeftGrab : SingleGrab
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LeftGrab"/> class.
    /// </summary>
    /// <param name="left">The left hand.</param>
    /// <param name="right">The right hand.</param>
    /// <param name="obj">The object.</param>
    public LeftGrab(HandModel left, HandModel right, GameObject obj) : base(left, right, obj)
    {
        Debug.Log(this.left);
        Debug.Log(this.right);
        Debug.Log(this.hand);
        this.hand = left;
        this.Init();
    }

    /// <summary>
    /// Switch strategy to a left hand strategy.
    /// </summary>
    /// <param name="grabbedObject">The grabbed object.</param>
    /// <returns>
    /// LeftGrab strategy.
    /// </returns>
    public override GrabStrategy LeftHand(GameObject grabbedObject)
    {
        this.grabbedObject = grabbedObject;
        return this;
    }
}
