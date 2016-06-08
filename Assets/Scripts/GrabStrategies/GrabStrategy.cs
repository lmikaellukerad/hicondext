using Leap.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

abstract class GrabStrategy
{
    public abstract void Destroy();

    public abstract void ConstrainHands(List<Transform> grabbingFingers);

    public abstract void UpdateObject();


    protected void ClampFingers(HandModel hand, List<Transform> grabbingFingers)
    {
        List<Transform> fingers = hand.GetComponent<HandSimulator>().FingerTipTransforms.ToList();
        List<Transform> grabbing = grabbingFingers.Intersect(fingers).ToList();

        for (int i = 0; i < grabbing.Count; i++)
        {
            hand.GetComponent<GrabHandSimulator>().ClampMax(grabbing[i]);
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