using UnityEngine;
using System.Collections;
using Leap.Unity;

public class KinectHandDrop : HandTransitionBehavior
{
    private bool tracking;
    public Transform palm;
    public Transform wrist;

    protected override void Awake()
    {
        base.Awake();
        tracking = true;
    }

    protected override void HandFinish()
    {
        tracking = false;
    }

    protected override void HandReset()
    {
        tracking = true;
    }

    // Update is called once per frame
    void Update () {
        if (!tracking)
            palm.position = wrist.position;
	}
}
