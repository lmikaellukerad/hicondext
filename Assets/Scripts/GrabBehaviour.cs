using UnityEngine;

public abstract class GrabBehaviour : MonoBehaviour
{
    public abstract void OnPinch(Vector3 v);

    public abstract void OnRelease();

    public abstract void Hold();

    public abstract void RecognizeGesture();

    public abstract void UpdateGrab();
}