using UnityEngine;
using System.Collections;
using Leap;
using Leap.Unity;

public class KinectHand : RiggedHand {

    public Transform wrist;

    public override void UpdateHand()
    {
        if (IsTracked)
        {
            if (palm != null)
            {
                palm.position = wrist.position;//GetWristPosition();//
                palm.rotation = GetPalmRotation() * Reorientation();
            }
            if (forearm != null)
            {
                if (IsTracked)
                {
                    Quaternion armRotation = GetArmRotation();
                    forearm.rotation = Quaternion.Euler(0f, 180f, 0f) * (new Quaternion(-armRotation.x, armRotation.y, -armRotation.z, armRotation.w) * Reorientation());
                }
            }

            for (int i = 0; i < fingers.Length; ++i)
            {
                if (fingers[i] != null)
                {
                    fingers[i].fingerType = (Finger.FingerType)i;
                    fingers[i].UpdateFinger();
                }
            }
        } 
    }

}
