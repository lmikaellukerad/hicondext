using UnityEngine;

public abstract class GrabBehaviour : MonoBehaviour
{
    public GrabState State
    {
        get;
        set;
    }

    public bool Pinching
    {
        get;
        protected set;
    }

    public bool Pinch
    {
        get;
        protected set;
    }

    public Vector3 PinchPosition
    {
        get;
        protected set;
    }

    public abstract void OnPinch();

    public abstract void OnRelease();

    public abstract void Hold();

    public abstract void RecognizeGesture();

    public abstract void UpdateGrab();
}