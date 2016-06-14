using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leap.Unity;
using UnityEngine;

public abstract class GrabStrategy
{
    protected static List<Transform> clampedFingers = new List<Transform>(); 
    public HandModel left;
    public HandModel right;

    public abstract void Destroy();

    public abstract void ConstrainHands(List<Transform> grabbingFingers);

    public abstract void UpdateObject();

    protected void HandleClamps(List<Transform> grabbingFingers)
    {
        List<Transform> newFingers = grabbingFingers.Except(GrabStrategy.clampedFingers).ToList();
        List<Transform> removedFingers = GrabStrategy.clampedFingers.Except(grabbingFingers).ToList();

        this.AddClampFingers(this.right, newFingers);
        this.RemoveClampFingers(this.right, removedFingers);

        this.AddClampFingers(this.left, newFingers);
        this.RemoveClampFingers(this.left, removedFingers);

        GrabStrategy.clampedFingers.Clear();
        GrabStrategy.clampedFingers.AddRange(grabbingFingers);
    }

    /// <summary>
    /// Adds the clamp to the fingers if they are from this HandModel.
    /// </summary>
    /// <param name="hand">The hand.</param>
    /// <param name="fingersToClamp">The fingers to clamp.</param>
    private void AddClampFingers(HandModel hand, List<Transform> fingersToClamp)
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
    private void RemoveClampFingers(HandModel hand, List<Transform> fingersToRelease)
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
}