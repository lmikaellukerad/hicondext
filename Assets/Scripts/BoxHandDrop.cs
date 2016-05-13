﻿using UnityEngine;
using System.Collections;
using Leap;

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

        protected override void Awake()
        {
            base.Awake();
            palm = GetComponent<HandModel>().palm;
            forearm = GetComponent<HandModel>().forearm;
            armCenter = forearm.localPosition;
            armRotation = forearm.localRotation;
            startingPalmPosition = palm.localPosition;
            startingPalmPosition = palm.localPosition;
            startingOrientation = palm.localRotation;
        }

        protected override void HandFinish()
        {
            StartCoroutine(LerpToStart());
        }
        protected override void HandReset()
        {
            StopAllCoroutines();
        }

        private float NonLinearInterpolation(float t)
        {
            return (-Mathf.Cos(t * Mathf.PI) + 1) / 2;
        }

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
                forearm.localPosition = Vector3.Lerp(droppedArmCenter, armCenter, NonLinearInterpolation(t));
                forearm.localRotation = Quaternion.Lerp(droppedArmRotation, armRotation, NonLinearInterpolation(t));

                yield return null;
            }
        }

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

