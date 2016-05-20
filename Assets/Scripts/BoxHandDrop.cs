using System.Collections;
using Leap;
using UnityEngine;

/**
 * Script for behaviour when tracking of hand is lost
 * 
 */
namespace Leap.Unity
{
    public class BoxHandDrop : HandTransitionBehavior
    {
        private Vector3 startingPalmPosition;
        private Quaternion startingOrientation;
        private Transform palm;
        private Transform forearm;
        private Vector3 armCenter;
        private Quaternion armRotation;

        /// <summary>
        /// Awakes this instance and initiates private variables.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            this.palm = GetComponent<HandModel>().palm;
            this.forearm = GetComponent<HandModel>().forearm;
            this.armCenter = this.forearm.localPosition;
            this.armRotation = this.forearm.localRotation;
            this.startingPalmPosition = this.palm.localPosition;
            this.startingPalmPosition = this.palm.localPosition;
            this.startingOrientation = this.palm.localRotation;
        }

        /// <summary>
        /// Finishes the hand.
        /// </summary>
        protected override void HandFinish()
        {
            StartCoroutine(LerpToStart());
        }
        /// <summary>
        /// Resets the hand.
        /// </summary>
        protected override void HandReset()
        {
            StopAllCoroutines();
        }

        /// <summary>
        /// Returns the result of a call to a non-linear function.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>
        /// float
        /// </returns>
        private float NonLinearInterpolation(float t)
        {
            return (-Mathf.Cos(t * Mathf.PI) + 1) / 2;
        }

        /// <summary>
        /// Uses linear interpolation to smoothly move the hand back to its starting position, used when loss of tracking occurs.
        /// </summary>
        /// <returns>
        /// IEnumerator
        /// </returns>
        private IEnumerator LerpToStart()
        {
            Vector3 droppedArmCenter = forearm.localPosition;
            Quaternion droppedArmRotation = forearm.localRotation;
            Vector3 droppedPosition = palm.localPosition;
            Quaternion droppedOrientation = palm.localRotation;
            float duration = 0.7f;
            float startTime = Time.time;
            float endTime = startTime + duration;

            while (Time.time <= endTime)
            {
                float t = (Time.time - startTime) / duration;
                palm.localPosition = Vector3.Lerp(droppedPosition, startingPalmPosition, NonLinearInterpolation(t));
                palm.localRotation = Quaternion.Lerp(droppedOrientation, startingOrientation, NonLinearInterpolation(t));
                forearm.localPosition = Vector3.Lerp(droppedArmCenter, this.armCenter, NonLinearInterpolation(t));
                forearm.localRotation = Quaternion.Lerp(droppedArmRotation, this.armRotation, NonLinearInterpolation(t));

                yield return null;
            }
        }

        /// <summary>
        /// Moves the hand using linear interpolation.
        /// </summary>
        /// <returns>
        /// IEnumerator
        /// </returns>
        private IEnumerator LerpBack()
        {
            Vector3 droppedPosition = palm.localPosition;
            Quaternion droppedOrientation = palm.localRotation;
            float duration = 0.25f;
            float startTime = Time.time;
            float endTime = startTime + duration;

            while (Time.time <= endTime)
            {
                float t = (Time.time - startTime) / duration;
                palm.localPosition = Vector3.Slerp(droppedPosition, startingPalmPosition, NonLinearInterpolation(1 - t));
                palm.localRotation = Quaternion.Lerp(droppedOrientation, startingOrientation, NonLinearInterpolation(1 - t));
                yield return null;
            }
        }
    }
}


