using UnityEngine;
using System.Collections;
using Leap.Unity;

public class LightPinch : MonoBehaviour {


    public Light l;
    private HandModel model;
    private float refrence = 0.05f;

	// Use this for initialization
	void Start () {
        model = transform.GetComponent<HandModel>();
	}
	
	// Update is called once per frame
	void Update () {
        if (model != null && l != null)
        {
            FingerModel thumb = model.fingers[0];
            FingerModel indexFinger = model.fingers[1];
            if (thumb != null && indexFinger != null)
            {
                if (Vector3.Distance(thumb.GetTipPosition(), indexFinger.GetTipPosition()) < refrence)
                {
                    l.enabled = true;
                }
                else
                {
                    l.enabled = false;
                }
            }
            else
            {
                l.enabled = false;
            }
        }
	}
}
