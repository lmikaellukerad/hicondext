using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using ManusMachina;
using UnityEngine;

public class ManusVibrate : MonoBehaviour 
{
    /// <summary>
    /// User can enable this from unity to make the glove vibrate once.
    /// </summary>
    public bool Vibrate;

    /// <summary>
    /// The glove object.
    /// </summary>
    private GLOVE_HAND hand;

    /// <summary>
    /// Keeps track of the current vibrating state of the glove.
    /// </summary>
    private bool vibrating;

    /// <summary>
    /// Starts this instance.
    /// </summary>
	public void Start()
    {
        this.hand = GetComponent<HandSimulator>().hand;
        this.vibrating = false;
	}

    /// <summary>
    /// Updates this instance.
    /// </summary>
    public void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            print("TRIGGERED: Short vibration");
            this.ShortVibration();
        }
        if (Input.GetKeyDown("n"))
        {
            print("TRIGGERED: Vibration on");
            VibrateOn();
        }
        if (Input.GetKeyDown("m"))
        {
            print("TRIGGERED: Vibration off");
            VibrateOff();
        }
	}

    /// <summary>
    /// Makes the glove vibrate for a given amount of milliseconds.
    /// </summary>
    /// <param name="ms">The amount of time the glove has to vibrate for.</param>
    public void VibrateFor(float sec)
    {
        StartCoroutine(VibrateForCoroutine(sec));
    }

    public IEnumerator VibrateForCoroutine(float sec)
    {
        
        if (!this.vibrating)
        {
            this.VibrateOn();
            print("vibON");
            yield return new WaitForSeconds(sec);
            this.VibrateOff();
            print("vibOFF");
            //Invoke("VibrateOff", sec);
        }
    }
    
    /// <summary>
    /// Makes the current clove vibrate for 150 milliseconds.
    /// </summary>
    public void ShortVibration()
    {
        this.VibrateFor(0.2f);
    }

    public void VibrateOn()
    {
        // GloveIndex, Power (0 to 1)
        this.vibrating = true;
        Manus.ManusSetVibration(this.hand, 1f);
    }

    public void VibrateOff()
    {
        this.vibrating = false;
        Manus.ManusSetVibration(this.hand, 0f);
    }
}
