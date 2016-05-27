using System.Collections;
using Leap;
using Leap.Unity;
using UnityEngine;

public class KinectHand : RiggedHand
{
    public Transform Wrist;

    public override void UpdateHand()
    {
        if (this.IsTracked) // read the tracking data
        {
            if (this.palm != null) // translate/rotate palm according to tracking data
            {
                palm.position = this.Wrist.position;
                palm.rotation = this.GetPalmRotation() * this.Reorientation();
            }

            if (this.forearm != null) // translate/rotate forearm according to tracking data
            {
                Quaternion armRotation = GetArmRotation();
                forearm.rotation = Quaternion.Euler(0f, 180f, 0f) * (new Quaternion(-armRotation.x, armRotation.y, -armRotation.z, armRotation.w) * this.Reorientation());
            }

            for (int i = 0; i < fingers.Length; ++i) // translate/rotate fingers according to tracking data
            {
                if (this.fingers[i] != null)
                {
                    fingers[i].fingerType = (Finger.FingerType)i;
                    fingers[i].UpdateFinger();
                }
            }
        }
    }
}
