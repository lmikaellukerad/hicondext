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
    public LeftGrab(HandModel left, HandModel right, GameObject obj)
    {
        this.left = left;
        this.right = right;
        this.hand = left;
        this.obj = obj;
        this.Init();
    }
}
