using UnityEngine;
using Leap.Unity;
using Leap;

/**
* Author: Luke
* TODO: Grabbed object lags behind when walking or moving.
*   This script determines the behaviour when grabbing gesture is made with Leap Motion
*/
public class FixedJointGrab : GrabBehaviour
{
    private HandModel model;

    private int interactable = 8;       //Layer with interactables
    public bool pinching { get; private set; }
    public bool pinch { get; protected set; }
    public Vector3 pinchPosition;
    private Vector3 previous;
    public GameObject grabbedObject { get; private set; }
    public float reference = 0.04f;
    public float radius = 0.05f;

    // Use this for initialization
    void Start() 
    { 
        initialize();
    }

    //Debug only
    void OnDrawGizmos()
    {

        HandModel hand_model = GetComponent<HandModel>();
        Hand leap_hand = hand_model.GetLeapHand();

        Gizmos.color = Color.red;
        //Gizmos.DrawSphere(pinchPosition, radius);
        //Gizmos.DrawLine(thumb.GetTipPosition(), model.palm.transform.position);
        if (pinch && pinching)
        {
            Gizmos.DrawSphere(pinchPosition, 0.05f);
        }
    }

    public void initialize()
    {
        model = getHandModel();
        pinching = false;
        pinch = false;
        pinchPosition = Vector3.zero;
        grabbedObject = null;
        previous = model.palm.transform.position;
    }

    public HandModel getHandModel()
    {
        return transform.GetComponent<HandModel>();
    }

    public override void onPinch(Vector3 pinch)
    {
        Collider[] objects = Physics.OverlapSphere(pinch, radius, (1 << interactable));
        float minimumDistance = float.MaxValue;
        
        if (grabbedObject == null)
        { 
            pinching = true;
        
            for (int i = 0; i < objects.Length; i++)
            {
                Collider o = objects[i];
                float currentDistance = Vector3.Distance(pinch, o.GetComponent<Transform>().position);
                if (currentDistance <= minimumDistance)
                {
                    grabbedObject = o.gameObject;
                    minimumDistance = currentDistance;
                }
            }
        }


    }

    public override void onRelease()
    {
        if (grabbedObject != null)
        {
            pinching = false;
            if (Application.isPlaying)
            {
                Destroy(grabbedObject.GetComponent<FixedJoint>());
            }
            grabbedObject.GetComponent<Collider>().enabled = true;
            grabbedObject.GetComponent<Rigidbody>().velocity = (model.palm.transform.position - previous) / Time.deltaTime;
            
        }
        GetComponent<HandSimulator>().OnRelease();
        grabbedObject = null;
    }

    public override void recognizeGesture()
    {
        float dist = reference;
        Hand leapHand = model.GetLeapHand();
        if (pinch)
        {
            dist = reference * 1.1f;
        }
        if (leapHand != null)
        {
            Finger thumb = leapHand.Fingers[0];
            for (int i = 1; i < HandModel.NUM_FINGERS; i++)
            {
                Finger finger = leapHand.Fingers[i];

                if (finger.TipPosition.DistanceTo(thumb.TipPosition) < dist)
                {
                    pinch = true;
                    pinchPosition = thumb.TipPosition.ToVector3();
                    return;
                }
            }
        }
        pinch = false;
    }

    public override void Hold()
    {
        if (grabbedObject != null)
        {
            if (grabbedObject.GetComponent<FixedJoint>() == null)
            {
                FixedJoint joint = grabbedObject.AddComponent<FixedJoint>();
                joint.connectedBody = model.palm.GetComponent<Rigidbody>();
                grabbedObject.GetComponent<Collider>().enabled = false;
                //joint.anchor = model.palm.transform.localPosition + new Vector3(0.5f, 0.2f, 0.6f);
                
            }
            GetComponent<HandSimulator>().OnGrab();

        }
    }

    public override void updateGrab()
    {
       // pinch = false;
        recognizeGesture();
        if (pinch && !pinching)
        {
            onPinch(pinchPosition);
        }
        else if (pinch && pinching)
        {
            Hold();
        }
        else if (!pinch && pinching)
        {
            onRelease();
        }

        
        previous = model.palm.transform.position;
    }

    void Update()
    {
        updateGrab();
    }
}

