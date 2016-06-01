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
        if (this.Vibrate)
        {
            this.ShortVibration();
            this.Vibrate = false;
        }
	}

    /// <summary>
    /// Makes the glove vibrate for a given amount of seconds.
    /// </summary>
    /// <param name="ms">The amount of time the glove has to vibrate for.</param>
    public void VibrateFor(int ms)
    {
        if (!this.vibrating)
        {
            this.vibrating = true;
            Manus.ManusSetVibration(this.hand, 100f);
            Thread.Sleep(ms);
            Manus.ManusSetVibration(this.hand, 0f);
            this.vibrating = false;
        }
    }
    
    /// <summary>
    /// Makes the current clove vibrate for 150 milliseconds.
    /// </summary>
    public void ShortVibration()
    {
        this.VibrateFor(150);
    }
}
