using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Implements GrabSate, defines the state of a grab when releasing an item from the hand.
/// </summary>
public class ReleaseState : GrabState
{

    /// <summary>
    /// Handle the state of the grab based on the current GrabBehavior properties.
    /// </summary>
    /// <param name="context">The GrabBehavior object that provides context for the state.</param>
    public override void Handle(GrabBehaviour context)
    {
        context.OnRelease();
        if (!context.Pinch && !context.Pinching)
        {
            ReleaseState.Destroy(context.State);
            context.State = ScriptableObject.CreateInstance<NeutralState>();
        }
    }
}
