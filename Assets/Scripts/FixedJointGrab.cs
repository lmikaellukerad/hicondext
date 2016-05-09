using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;
using Leap.Unity;

/**
* TODO: Grabbed object lags behind when walking or moving.
*   This script determines the behaviour when grabbing gesture is made with Leap Motion
*/
public class FixedJointGrab : MonoBehaviour
{
    private HandModel model;
    private FingerModel thumb;
    private int interactable = 8;       //Integer of layer with interactables
    private bool pinching;
    private Vector3 pinchPosition;
    private Vector3 previous;
    private GameObject grabbedObject;
    //private Vector3 grabPosition;
    public float reference = 0.04f;
    public float radius = 0.05f;
    public int force = 500;

    // Use this for initialization
    void Start()
    {
        model = transform.GetComponent<HandModel>();
        if (model != null)
            thumb = model.fingers[0];

        pinching = false;
        pinchPosition = Vector3.zero;
        //grabPosition = Vector3.zero;
        grabbedObject = null;
        previous = model.palm.transform.position;

    }

    // Update is called once per frame

    void onPinch(Vector3 pinch)
    {
        Collider[] objects = Physics.OverlapSphere(pinch, radius, (1 << interactable));
        float minimumDistance = radius;
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

    void onRelease()
    {
        pinching = false;

        if (grabbedObject != null)
        {
            Destroy(grabbedObject.GetComponent<FixedJoint>());
            grabbedObject.GetComponent<Rigidbody>().velocity = (model.palm.transform.position - previous) / Time.deltaTime;
        }

        grabbedObject = null;
    }


    void Update()

    {
        bool pinch = false;
        if (thumb != null)
        {
            for (int i = 1; i < model.fingers.Length; i++)
            {
                FingerModel finger = model.fingers[i];
                if (finger != null)
                {
                    if (Vector3.Distance(thumb.GetTipPosition(), finger.GetTipPosition()) < reference)
                    {
                        pinchPosition = thumb.GetTipPosition();
                        pinch = true;
                        break;
                    }

                }
            }
            if (pinch && !pinching)
            {
                onPinch(pinchPosition);
            }
            else if (!pinch && pinching)
            {
                onRelease();
            }

            if (grabbedObject != null)
            {
                if (grabbedObject.GetComponent<FixedJoint>() == null)
                {
                    grabbedObject.AddComponent<FixedJoint>();
                    grabbedObject.GetComponent<FixedJoint>().connectedBody = model.palm.GetComponent<Rigidbody>();
                }
            }            
        }
        previous = model.palm.transform.position;

    }
}

