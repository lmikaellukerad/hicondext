using Leap.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

abstract class SingleGrab : GrabStrategy
{
    protected HandModel hand;
    protected GameObject obj;
    private List<Transform> clampedFingers = new List<Transform>(); 
    private GameObject root;

    protected void Init()
    {
        root = new GameObject("root");
        root.transform.parent = obj.transform.parent;
        root.transform.position = hand.palm.position;
        root.transform.rotation = hand.palm.rotation;
        obj.transform.parent = root.transform;
    }

    public override void Destroy()
    {
        root.transform.GetChild(0).transform.parent = root.transform.parent;
        GameObject.Destroy(root);
        hand.GetComponent<GrabHandSimulator>().ResetFingerLimits();
    }

    public override void ConstrainHands(List<Transform> grabbingFingers)
    {
        for(int i = 0; i < grabbingFingers.Count; i ++){
            Transform finger = grabbingFingers[i];
            if (clampedFingers.Contains(finger))
            {
                clampedFingers.Remove(finger);
            }
            else
            {
                hand.GetComponent<GrabHandSimulator>().ClampMax(finger);
            }
        }
        for (int i = 0; i < clampedFingers.Count; i++)
        {
                hand.GetComponent<GrabHandSimulator>().ResetFingerLimit(clampedFingers[i]);
        }
        clampedFingers.Clear();
        clampedFingers.AddRange(grabbingFingers);
    }

    public override void UpdateObject()
    {
        root.transform.position = hand.palm.position;
        root.transform.rotation = hand.palm.rotation;
    }

}
