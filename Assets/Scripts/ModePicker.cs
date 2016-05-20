using System.Collections;
using Leap.Unity;
using UnityEngine;

public class ModePicker : MonoBehaviour
{
    public bool VRMode;

    /// <summary>
    /// initializes the Leap for either VR mounted or non-mounted mode.
    /// </summary>
    void Start()
    {
		if (this.VRMode)
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

    /// <summary>
    /// Updates this instance. An update is called once per frame.
    /// </summary>
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
