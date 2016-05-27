﻿using Leap.Unity;
using UnityEngine;

/**
*   This script determines the behaviour when grabbing gesture is made with Leap Motion
*/

public class FixedJointManusGrab : FixedJointGrab
{
    /// <summary>
    /// Determines the current gesture given the current finger positions and -rotations
    /// </summary>
    public override void RecognizeGesture()
    {
        Transform[] fingerTipTransforms = GetHandModel().GetComponent<HandSimulator>().FingerTipTransforms;
        if (fingerTipTransforms != null && fingerTipTransforms.Length != 0)
        {
            // compare the distance between thumb and all other fingers to recognize a pinch/grabbing motion
            Transform thumb = fingerTipTransforms[0];
            for (int i = 1; i < fingerTipTransforms.Length; i++)
            {
                Transform fingerTip = fingerTipTransforms[i];
                if (Vector3.Distance(fingerTip.position, thumb.position) < this.Reference)
                {
                    this.Pinch = true;
                    this.PinchPosition = fingerTipTransforms[1].position; // set the pinch position to the index finger
                    return;
                }
            }
        }
    }

    // Debug only
    private void OnDrawGizmos()
    {
        HandModel hand_model = GetComponent<HandModel>();

        Gizmos.color = Color.red;
        if (this.GetHandModel() != null)
        {
            Transform[] fingerTipTransforms = this.GetHandModel().GetComponent<HandSimulator>().FingerTipTransforms;
            if (fingerTipTransforms != null && fingerTipTransforms.Length != 0)
            {
                for (int i = 0; i < fingerTipTransforms.Length; i++)
                {
                    Transform fingerTip = fingerTipTransforms[i];
                    Gizmos.DrawSphere(fingerTip.transform.position, 0.01f);
                }
            }
        }

        if (this.Pinch && this.Pinching)
        {
            Gizmos.DrawSphere(this.PinchPosition, this.Radius);
        }
    }
}
