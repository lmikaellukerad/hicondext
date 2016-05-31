using Leap;
using Leap.Unity;
using UnityEngine;

/// <summary>
/// Author: Luke
/// TODO: Grabbed object lags behind when walking or moving.
/// This script determines the behavior when grabbing gesture is made with Leap Motion
/// </summary>
public class FixedJointGrab : GrabBehaviour
{

    public float Reference;
    public float Radius;

    private HandModel model;
    private int interactable = 8; // Layer with interactables
    private Vector3 previous;

    public GameObject GrabbedObject
    {
        get;
        protected set;
    }

    /// <summary>
    /// Initializes this instance.
    /// </summary>
    public void Initialize()
    {
        this.model = this.GetHandModel();
        this.Pinching = false;
        this.Pinch = false;
        this.PinchPosition = Vector3.zero;
        this.GrabbedObject = null;
        this.previous = this.model.palm.transform.position;
        this.State = ScriptableObject.CreateInstance<NeutralState>();
    }

    /// <summary>
    /// Returns the hand model.
    /// </summary>
    /// <returns>
    /// HandModel of this
    /// </returns>
    public HandModel GetHandModel()
    {
        return transform.GetComponent<HandModel>();
    }

    /// <summary>
    /// This method gets executed whenever the fingers are pinching.
    /// </summary>
    public override void OnPinch()
    {
        // create a collision sphere at position pinch with radius Radius.
        Vector3 pinch = PinchPosition;
        Collider[] objects = Physics.OverlapSphere(pinch, this.Radius, 1 << this.interactable);
        float minimumDistance = float.MaxValue;
        this.Pinching = true;

        // check what object is closest to our pinch, this object is the grabbed object
        for (int i = 0; i < objects.Length; i++)
        {
            Collider o = objects[i];
            float currentDistance = Vector3.Distance(pinch, o.GetComponent<Transform>().position);
            if (currentDistance < minimumDistance)
            {
                this.GrabbedObject = o.gameObject;
                minimumDistance = currentDistance;
            }
        }
    }

    /// <summary>
    /// This method gets executed whenever the fingers stop pinching.
    /// </summary>
    public override void OnRelease()
    {
        this.Pinching = false;
        if (this.GrabbedObject != null)
        {
            if (Application.isPlaying)
            {
                FixedJointGrab.Destroy(this.GrabbedObject.GetComponent<FixedJoint>());
            }

            this.GrabbedObject.GetComponent<Collider>().enabled = true;
            this.GrabbedObject.GetComponent<Rigidbody>().velocity = (this.model.palm.transform.position - this.previous) / Time.deltaTime;
        }

        this.GrabbedObject = null;
    }

    /// <summary>
    /// Determines the current gesture given the current finger positions and -rotations
    /// </summary>
    public override void RecognizeGesture()
    {
        Hand leapHand = this.model.GetLeapHand();
        if (leapHand != null)
        {
            if (this.DetectGrab(leapHand))
            {
                return;
            }
        }
        this.Pinch = false;
    }

    /// <summary>
    /// connects a RigidBody to the FixedJoint, practically lets a hand hold an item.
    /// </summary>
    public override void Hold()
    {
        if (this.GrabbedObject != null)
        {
            if (this.GrabbedObject.GetComponent<FixedJoint>() == null)
            {
                FixedJoint joint = this.GrabbedObject.AddComponent<FixedJoint>();
                joint.connectedBody = this.model.palm.GetComponent<Rigidbody>();
                this.GrabbedObject.GetComponent<Collider>().enabled = false;
            }
        }
        this.RecognizeGesture();
    }

    /// <summary>
    /// Updates to check for grabbing, pinching or holding.
    /// </summary>
    public override void UpdateGrab()
    {
        this.State.Handle(this);
        this.previous = this.model.palm.transform.position;
        Debug.Log(this.State);
    }

    /// <summary>
    /// Called during initialization
    /// </summary>
    private void Start()
    {
        this.Initialize();
    }

    /// <summary>
    /// Updates this for each frame.
    /// </summary>
    private void Update()
    {
        this.UpdateGrab();
    }

    /// <summary>
    /// Detects a grabbing motion of a leap hand.
    /// </summary>
    /// <param name="leapHand">The leap hand object.</param>
    /// <returns>true if pinch detected, false otherwise</returns>
    private bool DetectGrab(Hand leapHand)
    {
        Finger thumb = leapHand.Fingers[0];
        for (int i = 1; i < HandModel.NUM_FINGERS; i++)
        {
            if (this.DetectPinch(thumb, leapHand.Fingers[i]))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Detects a pinching motion between the thumb and another finger.
    /// </summary>
    /// <param name="thumb">The thumb.</param>
    /// <param name="finger">The finger.</param>
    /// <returns>true if pinch detected, false otherwise</returns>
    private bool DetectPinch(Finger thumb, Finger finger)
    {
        if (finger.TipPosition.DistanceTo(thumb.TipPosition) < this.Reference)
        {
            this.Pinch = true;
            this.PinchPosition = thumb.TipPosition.ToVector3();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Used for debug only
    /// </summary>
    private void OnDrawGizmos()
    {
        HandModel hand_model = GetComponent<HandModel>();
        Hand leap_hand = hand_model.GetLeapHand();

        Gizmos.color = Color.red;

        if (this.Pinch && this.Pinching)
        {
            Gizmos.DrawSphere(this.PinchPosition, this.Radius);
        }
    }
}
