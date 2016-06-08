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
    /// Makes the glove vibrate for a given amount of milliseconds.
    /// </summary>
    /// <param name="sec">The amount of time the glove has to vibrate for.</param>
    public void VibrateFor(float sec)
    {
        this.StartCoroutine(this.VibrateForCoroutine(sec));
    }

    /// <summary>
    /// Co-routine to VibrateFor, makes timing possible.
    /// </summary>
    /// <param name="sec">The amount of time the glove has to vibrate for.</param>
    /// <returns>IEnumerator, used for timing purposes. Can be ignored.</returns>
    public IEnumerator VibrateForCoroutine(float sec)
    {        
        if (!this.vibrating)
        {
            this.VibrateOn();
            yield return new WaitForSeconds(sec);
            this.VibrateOff();
        }
    }
    
    /// <summary>
    /// Makes the current glove vibrate for 100 milliseconds.
    /// </summary>
    public void ShortVibration()
    {
        this.VibrateFor(0.1f);
    }

    /// <summary>
    /// Makes the current glove vibrate shortly twice, with a brief pause.
    /// </summary>
    public void DoubleShortVibration()
    {
        this.StartCoroutine(this.DoubleShortVibrationCoroutine());
    }

    /// <summary>
    /// Co-routine to DoubleShortVibration, makes timing possible.
    /// </summary>
    /// <returns>IEnumerator, used for timing purposes. Can be ignored.</returns>
    public IEnumerator DoubleShortVibrationCoroutine()
    {
        this.ShortVibration();
        yield return new WaitForSeconds(0.2f);
        this.ShortVibration();
    }

    /// <summary>
    /// Turns vibration on.
    /// </summary>
    public void VibrateOn()
    {
        // GloveIndex, Power (0 to 1)
        this.vibrating = true;
        Manus.ManusSetVibration(this.hand, 1f);
    }

    /// <summary>
    /// Turns vibration off.
    /// </summary>
    public void VibrateOff()
    {
        this.vibrating = false;
        Manus.ManusSetVibration(this.hand, 0f);
    }

    /// <summary>
    /// Returns the current value of vibrating.
    /// </summary>
    /// <returns>The current value of vibrating.</returns>
    public bool GetVibrating()
    {
        return this.vibrating;
    }

    /// <summary>
    /// Sets the vibrating boolean.
    /// </summary>
    /// <param name="newbool">The value to set vibrating to.</param>
    public void SetVibrating(bool newbool)
    {
        this.vibrating = newbool;
    }
}
