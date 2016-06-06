using System;
using Leap;
using Leap.Unity;
using UnityEngine;

public abstract class PhysicsGrabBehaviour : MonoBehaviour
{
    public PhysicsGrabState State
    {
        get;
        set;
    }

    public Vector3 PinchPosition
    {
        get;
        protected set;
    }

    public GameObject GrabbedItem
    {
        get;
        protected set;
    }

    public abstract void Initialize();

    public abstract void CheckGrab();

    public abstract void CheckRelease();

    public abstract void OnGrab();

    public abstract void OnRelease();

    public abstract bool DetectGrab(Transform[] leftFingers, Transform[] rightFingers);

    public abstract bool DetectRelease(Transform[] leftFingers, Transform[] rightFingers);
}