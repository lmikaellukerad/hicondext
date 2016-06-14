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
        private float lerpToStartDuration = 0.7f;
        private float lerpBackDuration = 0.25f;

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
            this.StartCoroutine(this.LerpToStart());
        }

        /// <summary>
        /// Resets the hand.
        /// </summary>
        protected override void HandReset()
        {
            this.StopAllCoroutines();
        }

        /// <summary>
        /// Returns the result of a call to a non-linear function. 
        /// It is used to calculate the movement of the hand while moving back to it's origin.
        /// In order to move smoothly, we need a function that starts fast and ends slow, but is not linear.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>float t</returns>
        private float NonLinearInterpolation(float t)
        {
            return (-Mathf.Cos(t * Mathf.PI) + 1) / 2;
        }

        /// <summary>
        /// Uses linear interpolation to smoothly move the hand back to its starting position, used when loss of tracking occurs.
        /// </summary>
        /// <returns>IEnumerator IE</returns>
        private IEnumerator LerpToStart()
        {
            Vector3 droppedArmCenter = this.forearm.localPosition;
            Quaternion droppedArmRotation = this.forearm.localRotation;
            Vector3 droppedPosition = this.palm.localPosition;
            Quaternion droppedOrientation = this.palm.localRotation;
            float duration = this.lerpToStartDuration;
            float startTime = Time.time;
            float endTime = startTime + duration;

            // move the arm to its startingposition with speed determined by the NonLinearInterpolation function
            while (Time.time <= endTime)
            {
                float t = (Time.time - startTime) / duration;
                this.palm.localPosition = Vector3.Lerp(droppedPosition, this.startingPalmPosition, this.NonLinearInterpolation(t));
                this.palm.localRotation = Quaternion.Lerp(droppedOrientation, this.startingOrientation, this.NonLinearInterpolation(t));
                this.forearm.localPosition = Vector3.Lerp(droppedArmCenter, this.armCenter, this.NonLinearInterpolation(t));
                this.forearm.localRotation = Quaternion.Lerp(droppedArmRotation, this.armRotation, this.NonLinearInterpolation(t));

                yield return null;
            }
        }

        /// <summary>
        /// Moves the hand using linear interpolation.
        /// </summary>
        /// <returns>IEnumerator IE</returns>
        private IEnumerator LerpBack()
        {
            Vector3 droppedPosition = this.palm.localPosition;
            Quaternion droppedOrientation = this.palm.localRotation;
            float duration = this.lerpBackDuration;
            float startTime = Time.time;
            float endTime = startTime + duration;

            // move the arm to its droppedPosition with speed determined by the NonLinearInterpolation function
            while (Time.time <= endTime)
            {
                float t = (Time.time - startTime) / duration;
                this.palm.localPosition = Vector3.Slerp(droppedPosition, this.startingPalmPosition, this.NonLinearInterpolation(1 - t));
                this.palm.localRotation = Quaternion.Lerp(droppedOrientation, this.startingOrientation, this.NonLinearInterpolation(1 - t));
                yield return null;
            }
        }
    }
}