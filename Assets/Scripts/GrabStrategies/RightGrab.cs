using Leap.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Sets the correct hand model upon creation.
/// </summary>
/// <seealso cref="SingleGrab"/>
class RightGrab : SingleGrab
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RightGrab"/> class.
    /// </summary>
    /// <param name="right">The right.</param>
    /// <param name="obj">The object.</param>
    public RightGrab(HandModel right, GameObject obj)
    {
        this.hand = right;
        this.obj = obj;
        Init();
    }
}
