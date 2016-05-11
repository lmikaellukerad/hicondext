using UnityEngine;
using Leap.Unity;
using Leap;

/**
* TODO: Grabbed object lags behind when walking or moving.
*   This script determines the behaviour when grabbing gesture is made with Leap Motion
*/
public class FixedJointManusGrab : GrabBehaviour
{
    private HandModel model;
    private int interactable = 8;       //Layer with interactables
    public bool pinching { get; private set; }
    public bool pinch { get; private set; }
    public Vector3 pinchPosition { get; private set; }
    private Vector3 previous;
    private GameObject grabbedObject;
    //private Vector3 grabPosition;
    public float reference = 0.04f;
    public float radius = 0.05f;

    // Use this for initialization
    void Start()
    {
        model = getHandModel();
        pinching = false;
        pinch = false;
        pinchPosition = Vector3.zero;
        grabbedObject = null;
        previous = model.palm.transform.position;

    }

    //Debug only
    void OnDrawGizmos()
    {

        HandModel hand_model = GetComponent<HandModel>();

        Gizmos.color = Color.red;
        //Gizmos.DrawSphere(pinchPosition, radius);
        //Gizmos.DrawLine(thumb.GetTipPosition(), model.palm.transform.position);
        if (model != null)
        {
            Transform[] fingerTipTransforms = model.GetComponent<HandSimulator>().FingerTipTransforms;
            if (fingerTipTransforms != null && fingerTipTransforms.Length != 0)
            {
                Transform thumb = fingerTipTransforms[0];
                for (int i = 0; i < fingerTipTransforms.Length; i++)
                {
                    Transform fingerTip = fingerTipTransforms[i];
                    Gizmos.DrawSphere(fingerTip.transform.position, 0.01f);
                }
            }
        }
        if (pinch && pinching)
        {
            Gizmos.DrawSphere(pinchPosition, 0.05f);
        }
    }

    public HandModel getHandModel()
    {
        return transform.GetComponent<HandModel>();
    }

    public override void onPinch(Vector3 pinch)
    {
        Collider[] objects = Physics.OverlapSphere(pinch, radius, (1 << interactable));
        float minimumDistance = float.MaxValue;
        pinching = true;
        for (int i = 0; i < objects.Length; i++)
        {
            Collider o = objects[i];
            float currentDistance = Vector3.Distance(pinch, o.GetComponent<Transform>().position);
            if (currentDistance < minimumDistance)
            {
                grabbedObject = o.gameObject;
                minimumDistance = currentDistance;
            }
        }

    }

    public override void onRelease()
    {

        pinching = false;
        if (grabbedObject != null)
        {
            Destroy(grabbedObject.GetComponent<FixedJoint>());
            grabbedObject.GetComponent<Rigidbody>().velocity = (model.palm.transform.position - previous) / Time.deltaTime;
        }

        grabbedObject = null;
    }

    public override void recognizeGesture()
    {
        Transform[] fingerTipTransforms = model.GetComponent<HandSimulator>().FingerTipTransforms;
        if (fingerTipTransforms != null && fingerTipTransforms.Length != 0)
        {
            Transform thumb = fingerTipTransforms[0];
            for (int i = 1; i < fingerTipTransforms.Length; i++)
            {
                Transform fingerTip = fingerTipTransforms[i];
                if (Vector3.Distance(fingerTip.position, thumb.position) < reference)
                {
                    pinch = true;
                    pinchPosition = thumb.position;
                    return;
                }
            }
        }
    }

    public override void Hold()
    {
        if (grabbedObject != null)
        {
            if (grabbedObject.GetComponent<FixedJoint>() == null)
            {
                FixedJoint joint = grabbedObject.AddComponent<FixedJoint>();
                joint.connectedBody = model.palm.GetComponent<Rigidbody>();
                //joint.anchor = model.palm.transform.localPosition + new Vector3(0.5f, 0.2f, 0.6f);
            }
        }
    }

    public override void updateGrab()
    {
        pinch = false;
        recognizeGesture();
        if (pinch && !pinching)
        {
            onPinch(pinchPosition);
        }
        else if (!pinch && pinching)
        {
            onRelease();
        }
        Hold();
        previous = model.palm.transform.position;
    }

    void Update()
    {
        updateGrab();
    }
}

