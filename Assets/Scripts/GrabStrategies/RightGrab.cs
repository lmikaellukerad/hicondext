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
    /// <summary>
    /// Initializes a new instance of the <see cref="RightGrab" /> class.
    /// </summary>
    /// <param name="left">The left hand.</param>
    /// <param name="right">The right hand.</param>
    /// <param name="obj">The object.</param>
    public RightGrab(HandModel left, HandModel right, GameObject obj)
    {
        this.left = left;
        this.right = right;
        this.hand = right;
        this.obj = obj;
        this.Init();
    }
}
