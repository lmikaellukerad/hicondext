using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Leap.Unity;

/**
 * Test class for FixedJointManusGrab
 */
public class FixedJointManusGrabTest : FixedJointGrabTest {

    [SetUp]
    public override void setup()
    {
        GameObject empty = new GameObject();
        GameObject palm = new GameObject();
        HandModel hand = empty.AddComponent<SkeletalHand>();
        hand.palm = palm.transform;
        palm.AddComponent<Rigidbody>();
        grab = empty.AddComponent<FixedJointManusGrab>();
        grab.Initialize();

    }
}
