using System.Collections;
using Leap.Unity;
using UnityEngine;

public class ModePicker : MonoBehaviour
{
    public bool VRMode;
    private Vector3 vrPosition = new Vector3(0, 0, 0);
    private Vector3 vrRotation = new Vector3(-90, 180, 0);
    private Vector3 nonVrPosition = new Vector3(0, -0.3f, 0.4f);
    private Vector3 nonVrRotation = new Vector3(0, 0, 0);

    /// <summary>
    /// initializes the Leap for either VR mounted or non-mounted mode.
    /// </summary>
    public void Start()
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

			transform.localPosition = this.vrPosition;
			transform.localRotation = Quaternion.Euler(this.vrRotation);
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

            transform.localPosition = this.nonVrPosition;
            transform.localRotation = Quaternion.Euler(this.nonVrRotation);
		}
	}

    /// <summary>
    /// Updates this instance. An update is called once per frame.
    /// </summary>
    public void Update()
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

            transform.localPosition = this.vrPosition;
            transform.localRotation = Quaternion.Euler(this.vrRotation);
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

            transform.localPosition = this.nonVrPosition;
            transform.localRotation = Quaternion.Euler(this.nonVrRotation);
        }
	}
}
