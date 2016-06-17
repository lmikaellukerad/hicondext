using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Author: Luke
/// This code is used to add detectors to finger tips.
/// </summary>
public class FingerTipDetector
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FingerTipDetector"/> class.
    /// </summary>
    /// <param name="tips">The transforms of fingertips.</param>
    public FingerTipDetector(Transform[] tips)
    {
        foreach (Transform t in tips)
        {
            this.InitializeFingerDetector(t.gameObject);
        }
    }

    /// <summary>
    /// This method is used to add a DetectFingerCollision script to part of the finger
    /// </summary>
    /// <param name="obj">The object to add a RigidBody to.</param>
    private void InitializeFingerDetector(GameObject obj)
    {
        if (obj.name.Contains("end"))
        {
            obj.AddComponent<DetectFingerCollision>();
        }
    }
}
