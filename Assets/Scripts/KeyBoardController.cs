using System.Collections;
using UnityEngine;

/// <summary>
/// Author: Arjan
/// Controls vibration through key button presses.
/// Used for debug only.
/// </summary>
public class KeyboardController : MonoBehaviour 
{    
    /// <summary>
    /// Updates this instance.
    /// </summary>
	public void Update() 
    {
        // Triggers a short vibration.
        if (Input.GetKeyDown("b"))
        {
            GetComponent<ManusVibrate>().ShortVibration();
        }

        // Triggers a double short vibration.
        if (Input.GetKeyDown("h"))
        {
            GetComponent<ManusVibrate>().DoubleShortVibration();
        }

        // Turns vibration on.
        if (Input.GetKeyDown("n"))
        {
            GetComponent<ManusVibrate>().VibrateOn();
        }

        // Turns vibration off.
        if (Input.GetKeyDown("m"))
        {
            GetComponent<ManusVibrate>().VibrateOff();
        }
	}
}
