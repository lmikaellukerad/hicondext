using System.Collections;
using UnityEngine;

/// <summary>
/// This class sets behavior for grabbing objects with two hands.
/// WIP
/// </summary>
public class DoubleHandGrab : MonoBehaviour
{
    public HandSimulator[] hands;
    public Transform left;
    public Transform right;

    private void OnGrab(Collision col)
    {
        if (col != null)
        {
            GameObject grabbed = col.gameObject;
            if (grabbed.GetComponent<FixedJoint>() == null)
            {
                FixedJoint joint = grabbed.AddComponent<FixedJoint>();
                joint.connectedBody = left.GetComponent<Rigidbody>();
            }
        }
    }

    private void DetectGrab()
    {
        Collision found = null;
        foreach (HandSimulator hand in hands)
        {
            Collision current = DetectTouch(hand);
            found = CompareCollisions(found, current);
        }
        OnGrab(found);
    }

    private Collision DetectTouch(HandSimulator hand)
    {
        Transform[] fingerTips = hand.FingerTipTransforms;
        Collision found = null;
        foreach (Transform fingerTip in fingerTips)
        {
            DetectCollision detector = fingerTip.GetComponent<DetectCollision>();
            Collision current = CheckCollision(detector);
            if (IsEqual(found, current))
            {
                found = current;
            }
            else
            {
                if (found == null && current != null)
                {
                    found = current;
                }
                if (found != null && current != null)
                {
                    return null;
                }
            }
               
        }
        return found;
    }

    private Collision CheckCollision(DetectCollision detector)
    {
        if (detector.Collided)
        {
            return detector.Object;
        }
        return null;
    }

    private bool IsEqual(Collision found, Collision current)
    {
        if (found != null && current != null)
        {
            return found.Equals(current);
        }
        return false;
    }

    private Collision CompareCollisions(Collision found, Collision current)
    {
        if (found == null)
        {
            return current;
        }
        else if (current == null)
        { 
            return found;
        }
        if (found.Equals(current))
        {
            return found;
        }
        return null;

    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    public void Update()
    {
        DetectGrab();
    }
}
