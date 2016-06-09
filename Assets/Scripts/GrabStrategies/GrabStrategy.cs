using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leap.Unity;
using UnityEngine;

public abstract class GrabStrategy
{
    public abstract void Destroy();

    public abstract void ConstrainHands(List<Transform> grabbingFingers);

    public abstract void UpdateObject();

    /// <summary>
    /// Adds the clamp to the fingers if they are from this HandModel.
    /// </summary>
    /// <param name="hand">The hand.</param>
    /// <param name="fingersToClamp">The fingers to clamp.</param>
    protected void AddClampFingers(HandModel hand, List<Transform> fingersToClamp)
    {
        List<Transform> fingers = hand.GetComponent<HandSimulator>().FingerTipTransforms.ToList();
        List<Transform> grabbing = fingersToClamp.Intersect(fingers).ToList();

        for (int i = 0; i < grabbing.Count; i++)
        {
            Transform finger = grabbing[i];
            finger.GetComponent<DetectFingerCollision>().Radius = 0.02f;
            hand.GetComponent<GrabHandSimulator>().ClampMax(finger);
        }
    }

    /// <summary>
    /// Removes the clamp of the fingers if they are from this HandModel.
    /// </summary>
    /// <param name="hand">The hand.</param>
    /// <param name="fingersToRelease">The fingers to release.</param>
    protected void RemoveClampFingers(HandModel hand, List<Transform> fingersToRelease)
    {
        List<Transform> fingers = hand.GetComponent<HandSimulator>().FingerTipTransforms.ToList();
        List<Transform> grabbing = fingersToRelease.Intersect(fingers).ToList();

        for (int i = 0; i < grabbing.Count; i++)
        {
            Transform finger = grabbing[i];
            finger.GetComponent<DetectFingerCollision>().Radius = 0.01f;
            hand.GetComponent<GrabHandSimulator>().ResetFingerLimit(finger);
        }
    }

    /// <summary>
    /// Averages the position of the transforms.
    /// </summary>
    /// <param name="transforms">The transforms.</param>
    /// <returns>The average position vector</returns>
    protected Vector3 AveragePosition(List<Transform> transforms)
    {
        Vector3 total = Vector3.zero;
        foreach (Transform trans in transforms)
        {
            total += trans.position;
        }

        return total / transforms.Count;
    }
}