using System;
using Leap;
using Leap.Unity;
using UnityEngine;

public class PhysicsGrab : PhysicsGrabBehaviour
{
    private HandModel model;
    private int interactable = 8; // Layer with interactables
    private float grabDist;
    private float grabDistMargin;
    private Transform pinchingFinger;
    private float fingerSize;
    private Vector3 previous;
    private PhysicsGrabState previousState;

    public GameObject GrabbedObject
    {
        get;
        protected set;
    }

    /// <summary>
    /// Initializes this instance.
    /// </summary>
    public override void Initialize()
    {
        this.model = this.GetHandModel();
        this.PinchPosition = Vector3.zero;
        this.grabDist = 0.0f;
        this.grabDistMargin = 1.1f;
        this.pinchingFinger = null;
        this.fingerSize = 0.01f;
        this.GrabbedObject = null;
        this.previous = this.model.palm.transform.position;
        this.State = ScriptableObject.CreateInstance<PhysicsNeutralState>();
        this.previousState = this.State;
    }

    /// <summary>
    /// Checks the grabbing motion of the hand.
    /// </summary>
    public override void CheckGrab()
    {
        Transform[] fingerTipTransforms = this.GetHandModel().GetComponent<HandSimulator>().FingerTipTransforms;
        if (this.DetectGrab(fingerTipTransforms))
        {
            this.OnGrab();
            this.State = ScriptableObject.CreateInstance<PhysicsHoldState>();
        }  
    }

    /// <summary>
    /// Checks the release motion of the hand.
    /// </summary>
    public override void CheckRelease()
    {
        Transform[] fingerTipTransforms = this.GetHandModel().GetComponent<HandSimulator>().FingerTipTransforms;
        if (this.DetectRelease(fingerTipTransforms[0]))
        {
            this.OnRelease();
            this.State = ScriptableObject.CreateInstance<PhysicsNeutralState>();
        }
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
    /// Detects a grabbing motion and if the user holds an item.
    /// </summary>
    /// <param name="fingers">The fingers.</param>
    /// <returns>boolean b</returns>
    public override bool DetectGrab(Transform[] fingers)
    {
        Transform thumb = fingers[0];
        for (int i = 1; i < fingers.Length; i++)
        {
            if (this.DetectPinch(thumb, fingers[i]))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Detects if the hand should release the object it's holding.
    /// </summary>
    /// <param name="thumb">The thumb.</param>
    /// <returns>boolean b</returns>
    public override bool DetectRelease(Transform thumb)
    {
        if (Vector3.Distance(this.pinchingFinger.position, thumb.position) > (this.grabDist * this.grabDistMargin))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// The item gets locked to a finger using FixedJoint. Gets executed when the user starts holding an item.
    /// </summary>
    public override void OnGrab()
    {
        if (this.GrabbedObject != null)
        {
            if (this.GrabbedObject.GetComponent<FixedJoint>() == null)
            {
                FixedJoint joint = this.GrabbedObject.AddComponent<FixedJoint>();
                joint.connectedBody = this.pinchingFinger.GetComponent<Rigidbody>();
                this.GrabbedObject.GetComponent<Collider>().enabled = false;
            }
        }
    }

    /// <summary>
    /// This method gets executed whenever the fingers stop pinching.
    /// </summary>
    public override void OnRelease()
    {
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
        this.PinchPosition = Vector3.zero;
        this.pinchingFinger = null;
    }

    /// <summary>
    /// Detects a pinching motion between the thumb and one other finger.
    /// </summary>
    /// <param name="thumb">The thumb.</param>
    /// <param name="finger">The finger.</param>
    /// <returns>true if pinch detected, false otherwise</returns>
    private bool DetectPinch(Transform thumb, Transform finger)
    {
        Collider[] fingerCollisions = Physics.OverlapSphere(finger.position, this.fingerSize, 1 << this.interactable);
        Collider[] thumbCollisions = Physics.OverlapSphere(thumb.position, this.fingerSize, 1 << this.interactable);
        Collider grabbed = this.FindIntersection(fingerCollisions, thumbCollisions);
        if (grabbed != null)
        {
            this.PinchPosition = finger.position;
            this.pinchingFinger = finger;
            this.GrabbedObject = grabbed.gameObject;
            this.grabDist = Vector3.Distance(this.pinchingFinger.position, thumb.position);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Finds the common object within two lists of Colliders.
    /// </summary>
    /// <param name="a">The first Collider list.</param>
    /// <param name="b">The second Collider list.</param>
    /// <returns>Collider c</returns>
    private Collider FindIntersection(Collider[] a, Collider[] b)
    {
        for (int i = 0; i < a.Length; i++)
        {
            for (int j = 0; j < b.Length; j++)
            {
                if (a[i].Equals(b[j]))
                {
                    return a[i];
                }
            }
        }

        return null;
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
        this.State.Handle(this);
        this.previous = this.model.palm.transform.position;

        // Debug log
        if (!this.State.Equals(this.previousState))
        {
            Debug.Log(this.State);
        }

        this.previousState = this.State;
    }

    /// <summary>
    /// Used for debug only
    /// </summary>
    private void OnDrawGizmos()
    {
        HandModel hand_model = GetComponent<HandModel>();
        Hand leap_hand = hand_model.GetLeapHand();

        Gizmos.color = Color.red;

        if (this.GrabbedItem != null)
        {
            Gizmos.DrawSphere(this.PinchPosition, this.fingerSize);
        }
    }
}