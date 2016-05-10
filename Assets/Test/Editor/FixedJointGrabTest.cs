using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;
using Leap.Unity;

/**
 * ToDo: more relevant tests
 * Author: Luke
 * Test script for FixedJointGrab
 * 
 */
public class FixedJointGrabTest
{ 


    [Test]
    public void holdTest()
    {
       GameObject empty = new GameObject();
       empty.AddComponent<SkeletalHand>();
       FixedJointGrab grab = empty.AddComponent<FixedJointGrab>();
       grab.Hold();
       Assert.False(grab.pinching);
    }

    [Test]
    public void onPinchTest()
    {
        GameObject empty = new GameObject();
        empty.AddComponent<SkeletalHand>();
        FixedJointGrab grab = empty.AddComponent<FixedJointGrab>()
        grab.onPinch(Vector3.zero);
        Assert.True(grab.pinching);
    }

    [Test]
    public void onRelease()
    {
        GameObject empty = new GameObject();
        empty.AddComponent<SkeletalHand>();
        FixedJointGrab grab = empty.AddComponent<FixedJointGrab>();
        grab.onPinch(Vector3.zero);
        Assert.False(grab.pinching && grab.pinch);
    }
}