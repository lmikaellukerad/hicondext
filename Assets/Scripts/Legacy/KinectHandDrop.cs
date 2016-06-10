using System.Collections;
using Leap.Unity;
using UnityEngine;

public class KinectHandDrop : HandTransitionBehavior
{
    public Transform Palm;
    public Transform Wrist;
    private bool tracking;

    // Update is called once per frame
    public void Update()
    {
        if (!this.tracking)
        {
            this.Palm.position = this.Wrist.position;
        }
    }

    /// <summary>
    /// Gets the tracking boolean.
    /// </summary>
    /// <returns>The tracking boolean.</returns>
    public bool GetTracking()
    {
        return this.tracking;
    }

    /// <summary>
    /// Sets the tracking boolean.
    /// </summary>
    /// <param name="b">The value to set tracking to.</param>
    public void SetTracking(bool b)
    {
        this.tracking = b;
    }

    /// <summary>
    /// Same as Awake(), but public, for testing purposes.
    /// </summary>
    public void PublicAwake()
    {
        this.Awake();
    }

    /// <summary>
    /// Same as HandFinish(), but public, for testing purposes.
    /// </summary>
    public void PublicHandFinish()
    {
        this.HandFinish();
    }

    /// <summary>
    /// Same as HandReset(), but public, for testing purposes.
    /// </summary>
    public void PublicHandReset()
    {
        this.HandReset();
    }

    protected override void Awake()
    {
        base.Awake();
        this.tracking = true;
    }

    protected override void HandFinish()
    {
        this.tracking = false;
    }

    protected override void HandReset()
    {
        this.tracking = true;
    }    
}
