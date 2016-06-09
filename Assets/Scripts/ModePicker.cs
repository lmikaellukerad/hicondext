using System.Collections;
using Leap.Unity;
using UnityEngine;

/// <summary>
/// TODO: Needs refactoring and fix.
/// </summary>
public class ModePicker : MonoBehaviour
{
    public bool VRMode;
    private Vector3 headPosition = new Vector3(0, 0, 0);
    private Vector3 headRotation = new Vector3(-90, 180, 0);
    private Vector3 desktopPosition = new Vector3(0, -0.3f, 0.4f);
    private Vector3 desktopRotation = new Vector3(0, 0, 0);

    /// <summary>
    /// initializes the Leap for either VR mounted or non-mounted mode.
    /// </summary>
    public void Start()
    {
        // set the program to work in VR mode or non-VR mode
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

			transform.localPosition = this.headPosition;
			transform.localRotation = Quaternion.Euler(this.headRotation);
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

            transform.localPosition = this.desktopPosition;
            transform.localRotation = Quaternion.Euler(this.desktopRotation);
		}
	}

    /// <summary>
    /// Updates this instance. An update is called once per frame.
    /// </summary>
    public void Update()
    {
        // set the program to work in VR mode or non-VR mode
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

            transform.localPosition = this.headPosition;
            transform.localRotation = Quaternion.Euler(this.headRotation);
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

            transform.localPosition = this.desktopPosition;
            transform.localRotation = Quaternion.Euler(this.desktopRotation);
        }
	}
}
