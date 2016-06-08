using Leap.Unity;
using UnityEngine;

/// <summary>
/// This script determines the behavior when grabbing gesture is made with the Manus.
/// </summary>
public class FixedJointManusGrab : FixedJointGrab
{
    /// <summary>
    /// Determines the current gesture given the current finger positions and rotations
    /// </summary>
    public override void RecognizeGesture()
    {
        Transform[] fingerTipTransforms = GetHandModel().GetComponent<HandSimulator>().FingerTipTransforms;
        if (fingerTipTransforms != null && fingerTipTransforms.Length != 0)
        {
            if (this.DetectGrab(fingerTipTransforms))
            {
                return;
            }
        }

        this.Pinch = false;
    }

    /// <summary>
    /// Detects the grab based on the positions of the Manus fingers. 
    /// Compare the distance between thumb and all other fingers to recognize a pinch motion.
    /// </summary>
    /// <param name="fingerTips">The finger tips.</param>
    /// <returns>true if grab is detected, false otherwise</returns>
    private bool DetectGrab(Transform[] fingerTips)
    {
        Transform thumb = fingerTips[0];
        for (int i = 1; i < fingerTips.Length; i++)
        {
            Transform fingerTip = fingerTips[i];
            if (Vector3.Distance(fingerTip.position, thumb.position) < this.Reference)
            {
                this.Pinch = true;
                this.PinchPosition = fingerTips[1].position; // set the pinch position to the index finger
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Used for debug only, draws gizmo's around the fingertips. Can be turned off during runtime.
    /// </summary>
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
