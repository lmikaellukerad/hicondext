using UnityEngine;
using System.Collections;
using Leap;
using System;

/**
 * Box Hand model for testing
 * extends HandModel
 * 
 */
namespace Leap.Unity
{
    public class BoxHand : HandModel
    {
        public override ModelType HandModelType
        {
            get
            {
                return ModelType.Graphics;
            }
        }

        public Quaternion Reorientation()
        {
            return Quaternion.Inverse(Quaternion.LookRotation(Vector3.forward, Vector3.up));
        }

        public override void UpdateHand()
        {
            if (palm != null)
            {
                palm.position = GetPalmPosition();
                palm.rotation = GetPalmRotation() * Reorientation();
            }

            if (forearm != null)
            {
                forearm.position = GetArmCenter();
                forearm.rotation = GetArmRotation();
            }
        }
    }
}
