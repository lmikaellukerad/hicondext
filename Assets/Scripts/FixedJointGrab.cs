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

    public float Reference = 0.04f;
    public float Radius = 0.05f;

    private HandModel model;
    private int interactable = 8; // Layer with interactables
    private Vector3 previous;

    public bool Pinching
    {
        get;
        private set;
    }

    public bool Pinch
    {
        get;
        protected set;
    }

    public GameObject GrabbedObject
    {
        get;
        private set;
    }

    public Vector3 PinchPosition
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
    /// <param name="pinch">The pinch.</param>
    public override void OnPinch(Vector3 pinch)
    {
        Collider[] objects = Physics.OverlapSphere(pinch, this.Radius, 1 << this.interactable);
        float minimumDistance = float.MaxValue;
        this.Pinching = true;
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
            Finger thumb = leapHand.Fingers[0];
            for (int i = 1; i < HandModel.NUM_FINGERS; i++)
            {
                Finger finger = leapHand.Fingers[i];
                if (finger.TipPosition.DistanceTo(thumb.TipPosition) < this.Reference)
                {
                    this.Pinch = true;
                    this.PinchPosition = thumb.TipPosition.ToVector3();
                    return;
                }
            }
        }
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
    }

    /// <summary>
    /// Updates to check for grabbing, pinching or holding.
    /// </summary>
    public override void UpdateGrab()
    {
        this.Pinch = false;
        this.RecognizeGesture();
        if (this.Pinch && !this.Pinching)
        {
            this.OnPinch(this.PinchPosition);
        }
        else if (!this.Pinch && this.Pinching)
        {
            this.OnRelease();
        }

        this.Hold();
        this.previous = this.model.palm.transform.position;
    }

    /// <summary>
    /// Called during initialization
    /// </summary>
    private void Start()
    {
        this.Initialize();
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
            Gizmos.DrawSphere(this.PinchPosition, 0.05f);
        }
    }

    /// <summary>
    /// Updates this for each frame.
    /// </summary>
    private void Update()
    {
        this.UpdateGrab();
    }
}