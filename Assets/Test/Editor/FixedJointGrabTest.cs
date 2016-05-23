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
    public LayerMask interactable = 8;
    protected FixedJointGrab grab; 

    [SetUp]
    public virtual void setup()
    {
        GameObject empty = new GameObject();
        GameObject palm = new GameObject();
        HandModel hand = empty.AddComponent<SkeletalHand>();
        hand.palm = palm.transform;
        palm.AddComponent<Rigidbody>();
        grab = empty.AddComponent<FixedJointGrab>();
        grab.Initialize();

    }

    [TearDown]
    public void cleanup()
    {
        Object.DestroyImmediate(grab);
    }

    private GameObject createPhysicalObject(Vector3 pos)
    {
        GameObject o = new GameObject("object");
        o.AddComponent<BoxCollider>();
        o.AddComponent<Rigidbody>();
        o.transform.position = pos;
        o.layer = interactable;
        return o;
    }

    [Test]
    public void holdPinchingTest()
    {
       grab.Hold();
       Assert.False(grab.Pinching);
    }

    [Test]
    public void holdNoGrabbableItemTest()
    {
        grab.OnPinch(Vector3.zero);
        grab.Hold();
        Assert.AreSame(null, grab.GrabbedObject);
    }

    [Test]
    public void holdGrabbableItemNoFixedJointTest()
    {
        GameObject grabbable = createPhysicalObject(Vector3.zero);
        grab.OnPinch(Vector3.zero);
        grab.Hold();
        Assert.AreNotSame(null, grab.GrabbedObject.GetComponent<FixedJoint>());
        Object.DestroyImmediate(grabbable);
    }

    [Test]
    public void onPinchPinchingTest()
    {
        grab.OnPinch(Vector3.zero);
        Assert.True(grab.Pinching);
    }

    [Test]
    public void onPinchNoGrabbableItemTest()
    {
        grab.OnPinch(Vector3.zero);
        Assert.AreSame(null, grab.GrabbedObject);
    }

    [Test]
    public void onPinchGrabbableItemTest()
    {
        GameObject grabbable = createPhysicalObject(Vector3.zero);
        grab.OnPinch(Vector3.zero);
        Assert.AreSame(grabbable, grab.GrabbedObject);
        Object.DestroyImmediate(grabbable);
    }

    [Test]
    public void onReleasePinchingTest()
    {
        grab.OnRelease();
        Assert.False(grab.Pinching && grab.Pinch);
    }

    [Test]
    public void onReleaseNoGrabbableItemTest()
    {
        grab.OnPinch(Vector3.zero);
        grab.OnRelease();
        Assert.AreSame(null, grab.GrabbedObject);
    }

    [Test]
    public void onReleaseGrabbableItemTest()
    {
        GameObject grabbable = createPhysicalObject(Vector3.zero);
        grab.OnPinch(Vector3.zero);
        grab.Hold();
        grab.OnRelease();
        Assert.AreSame(null, grab.GrabbedObject);
        Object.DestroyImmediate(grabbable);
    }


}