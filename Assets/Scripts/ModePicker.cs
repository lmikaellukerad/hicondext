using UnityEngine;
using System.Collections;
using Leap.Unity;

public class ModePicker : MonoBehaviour {

	public bool VRMode;

	// Use this for initialization
	void Start()
    {
		if (VRMode)
		{
            if (transform.GetChild(0) != null && transform.GetChild(0).GetComponent<LeapServiceProvider>() != null)
            {
                transform.GetChild(0).GetComponent<LeapServiceProvider>()._isHeadMounted = true;
                transform.GetChild(0).GetComponent<LeapServiceProvider>().overrideDeviceType = true;
            }
            if (transform.GetComponent<LeapVRTemporalWarping>() != null)
            {
                transform.GetComponent<LeapVRTemporalWarping>().enabled = true;
            }
			transform.localPosition = new Vector3(0, 0, 0);
			transform.localRotation = Quaternion.Euler(new Vector3(-90, 180, 0));
		}
		else
        {
            if (transform.GetChild(0) != null && transform.GetChild(0).GetComponent<LeapServiceProvider>() != null)
            {
                transform.GetChild(0).GetComponent<LeapServiceProvider>()._isHeadMounted = false;
                transform.GetChild(0).GetComponent<LeapServiceProvider>().overrideDeviceType = false;
            }
            if (transform.GetComponent<LeapVRTemporalWarping>() != null)
            {
                transform.GetComponent<LeapVRTemporalWarping>().enabled = false;
            }
            transform.localPosition = new Vector3(0, -0.3f, 0.4f);
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
		}
	}
	
	// Update is called once per frame
    void Update()
    {
        if (VRMode)
        {
            if (transform.GetChild(0) != null && transform.GetChild(0).GetComponent<LeapServiceProvider>() != null)
            {
                transform.GetChild(0).GetComponent<LeapServiceProvider>()._isHeadMounted = true;
                transform.GetChild(0).GetComponent<LeapServiceProvider>().overrideDeviceType = true;
            }
            if (transform.GetComponent<LeapVRTemporalWarping>() != null)
            {
                transform.GetComponent<LeapVRTemporalWarping>().enabled = true;
            }
            transform.localPosition = new Vector3(0, 0, 0);
            transform.localRotation = Quaternion.Euler(new Vector3(-90, 180, 0));
        }
        else
        {
            if (transform.GetChild(0) != null && transform.GetChild(0).GetComponent<LeapServiceProvider>() != null)
            {
                transform.GetChild(0).GetComponent<LeapServiceProvider>()._isHeadMounted = false;
                transform.GetChild(0).GetComponent<LeapServiceProvider>().overrideDeviceType = false;
            }
            if (transform.GetComponent<LeapVRTemporalWarping>() != null)
            {
                transform.GetComponent<LeapVRTemporalWarping>().enabled = false;
            }
            transform.localPosition = new Vector3(0, -0.3f, 0.4f);
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
	}
}
