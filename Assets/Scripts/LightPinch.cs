using UnityEngine;
using System.Collections;
using Leap.Unity;

/**
* TODO: Grabbed object lags behind when walking or moving.
*   This script determines the behaviour when grabbing gesture is made with Leap Motion
*/
public class LightPinch : MonoBehaviour
{
    private HandModel model;
    private FingerModel thumb;
    private int interactable = 8;       //Integer of layer with interactables
    private bool pinching;
    private Vector3 pinchPosition;
    private GameObject grabbedObject;
    //private Vector3 grabPosition;
    public float reference = 0.03f;
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

    void FixedUpdate()
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
                pinching = false;
                if (grabbedObject != null)
                    grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                grabbedObject = null;
            }

            if (grabbedObject != null)
            {
                Vector3 distance = pinchPosition - grabbedObject.transform.position;
                //grabbedObject.GetComponent<Rigidbody>().AddForceAtPosition(force * distance, grabbedObject.transform.position, ForceMode.Impulse);
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                grabbedObject.GetComponent<Rigidbody>().position = pinchPosition;//model.palm.transform.TransformPoint(Vector3.up * -0.05f + Vector3.forward * 0.05f);
                grabbedObject.GetComponent<Rigidbody>().rotation = model.GetPalmRotation();
                //grabbedObject.transform.parent = model.transform;
            }
        }
   
    }
}

