using System.Collections;
using UnityEngine;

public abstract class GrabBehaviour : MonoBehaviour
{
    public abstract void onPinch(Vector3 v);

    public abstract void onRelease();

    public abstract void Hold();

    public abstract void recognizeGesture();

    public abstract void updateGrab();
}
