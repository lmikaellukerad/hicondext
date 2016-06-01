using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Implements GrabState, defines the state of a grab when the hand holds an item.
/// </summary>
public class HoldState : GrabState
{
    /// <summary>
    /// Handle the state of the grab based on the current GrabBehavior properties.
    /// </summary>
    /// <param name="context">The GrabBehavior object that provides context for the state.</param>
    public override void Handle(GrabBehaviour context)
    {
        context.Hold();
        if (!context.Pinch && context.Pinching)
        {
            context.State = ScriptableObject.CreateInstance<ReleaseState>();
        }
    }
}
