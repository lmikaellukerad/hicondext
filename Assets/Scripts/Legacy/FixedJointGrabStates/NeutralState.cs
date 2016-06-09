using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Implements GrabState, defines the state of the grab when the hand is in a neutral state (not grabbing, pinching or holding anything).
/// </summary>
public class NeutralState : GrabState
{
    /// <summary>
    /// Handle the state of the grab based on the current GrabBehavior properties.
    /// </summary>
    /// <param name="context">The GrabBehavior object that provides context for the state.</param>
    public override void Handle(GrabBehaviour context)
    {
        context.RecognizeGesture();
        if (context.Pinch && !context.Pinching)
        {
            context.State = ScriptableObject.CreateInstance<PinchState>();
        }
    }
}
