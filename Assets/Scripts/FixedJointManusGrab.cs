using UnityEngine;
using Leap.Unity;
using Leap;

/**
* TODO: Grabbed object lags behind when walking or moving.
*   This script determines the behaviour when grabbing gesture is made with Leap Motion
*/
public class FixedJointManusGrab : FixedJointGrab
{

    //Debug only
    void OnDrawGizmos()
    {

        HandModel hand_model = GetComponent<HandModel>();

        Gizmos.color = Color.red;
        //Gizmos.DrawSphere(pinchPosition, radius);
        //Gizmos.DrawLine(thumb.GetTipPosition(), model.palm.transform.position);
        if (getHandModel() != null)
        {
            Transform[] fingerTipTransforms = getHandModel().GetComponent<HandSimulator>().FingerTipTransforms;
            if (fingerTipTransforms != null && fingerTipTransforms.Length != 0)
            {
                Transform thumb = fingerTipTransforms[0];
                for (int i = 0; i < fingerTipTransforms.Length; i++)
                {
                    Transform fingerTip = fingerTipTransforms[i];
                    Gizmos.DrawSphere(fingerTip.transform.position, 0.01f);
                }
            }
        }
        if (pinch && pinching)
        {
            Gizmos.DrawSphere(pinchPosition, 0.05f);
        }
    }

    /// <summary>
    /// Determines the current gesture given the current fingerpositions and -rotations
    /// </summary>
    public override void recognizeGesture()
    {
        Transform[] fingerTipTransforms = getHandModel().GetComponent<HandSimulator>().FingerTipTransforms;
        if (fingerTipTransforms != null && fingerTipTransforms.Length != 0)
        {
            Transform thumb = fingerTipTransforms[0];
            for (int i = 1; i < fingerTipTransforms.Length; i++)
            {
                Transform fingerTip = fingerTipTransforms[i];
                if (Vector3.Distance(fingerTip.position, thumb.position) < reference)
                {
                    pinch = true;
                    pinchPosition = thumb.position;
                    return;
                }
            }
        }
    }

}
