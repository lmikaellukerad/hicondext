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
        /// Reorientates the lookrotation.
        /// </summary>
        /// <returns>
        /// the reorientated quaternion
        /// </returns>
        public Quaternion Reorientation()
        {
            return Quaternion.Inverse(Quaternion.LookRotation(Vector3.forward, Vector3.up));
        }


        /// <summary>
        /// Initialises hand object .This function is called by the HandController during the Unity Update() phase when a new hand is detected
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
            if (palm != null)
            {
                palm.position = GetPalmPosition();
                palm.rotation = GetPalmRotation() * Reorientation();
            }

            if (forearm != null)
            {
                forearm.position = GetArmCenter();
                forearm.rotation = GetArmRotation() * Reorientation();
            }
        }
    }
}
