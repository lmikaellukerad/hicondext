using System.Collections;
using Leap;
using Leap.Unity;
using UnityEngine;

public class KinectHand : RiggedHand
{
    public Transform Wrist;

    public override void UpdateHand()
    {
        // read the tracking data
        if (this.IsTracked)
        {
            // translate/rotate palm according to tracking data
            if (this.palm != null) 
            {
                palm.position = this.Wrist.position;
                palm.rotation = this.GetPalmRotation() * this.Reorientation();
            }

            // translate/rotate forearm according to tracking data
            if (this.forearm != null) 
            {
                Quaternion armRotation = GetArmRotation();
                forearm.rotation = Quaternion.Euler(0f, 180f, 0f) * (new Quaternion(-armRotation.x, armRotation.y, -armRotation.z, armRotation.w) * this.Reorientation());
            }

            // translate/rotate fingers according to tracking data
            for (int i = 0; i < fingers.Length; ++i) 
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