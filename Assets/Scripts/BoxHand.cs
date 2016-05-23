using System;
using System.Collections;
using Leap;
using UnityEngine;

/**
 * Box Hand model for testing
 * extends HandModel
 * 
 */
namespace Leap.Unity
{
    public class BoxHand : HandModel
    {
        /// <summary>
        /// Gets the type of the hand model.
        /// </summary>
        /// <value>
        /// The type of the hand model.
        /// </value>
        public override ModelType HandModelType
        {
            get
            {
                return ModelType.Graphics;
            }
        }

        /// <summary>
        /// Reorients the look rotation.
        /// </summary>
        /// <returns>
        /// the reoriented quaternion
        /// </returns>
        public Quaternion Reorientation()
        {
            return Quaternion.Inverse(Quaternion.LookRotation(Vector3.forward, Vector3.up));
        }

        /// <summary>
        /// Initializes hand object .This function is called by the HandController during the Unity Update() phase when a new hand is detected
        /// by the Leap Motion device.
        /// </summary>
        public override void InitHand()
        {
            base.InitHand();
        }

        /// <summary>
        /// This function updates the Hand object with the correct position and rotation
        /// the HandController calls this function during the Unity Update() phase. 
        /// For HandModel instances in the physics hand list, the HandController calls this function in the FixedUpdate() phase.
        /// </summary>       
        public override void UpdateHand()
        {
            if (this.palm != null)
            {
                palm.position = this.GetPalmPosition();
                palm.rotation = this.GetPalmRotation() * this.Reorientation();
            }

            if (this.forearm != null)
            {
                forearm.position = this.GetArmCenter();
                forearm.rotation = this.GetArmRotation() * this.Reorientation();
            }
        }
    }
}
