using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Author: Luke
/// Abstract class GrabState for defining the state of a grab.
/// </summary>
public abstract class PhysicsGrabState : ScriptableObject
{
    /// <summary>
    /// Handle the state of the grab based on the current GrabBehavior properties.
    /// </summary>
    /// <param name="context">The GrabBehavior object that provides context for the state.</param>
    public abstract void Handle(PhysicsGrabBehaviour context);

    internal void Handle(PhysicsGrab physicsGrab)
    {
    }
}
