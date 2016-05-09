using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;
using Leap.Unity;

/**
 * WIP
 * Author: Luke
 * 
 * 
 */
public class FixedJointGrabTest : MonoBehaviour
{ 

    private HandModel GetHandModelMock()
    {
        return Substitute.For<SkeletalHand>();
    }

    private FingerModel GetFingerModelMock()
    {
        return Substitute.For<SkeletalFinger>();
    }

    [Test]
    public void GrabTest()
    {
        var grab = new FixedJointGrab();
        HandModel handModelMock = GetHandModelMock();
        FingerModel fingerModelMock = GetFingerModelMock();
        Assert.AreEqual(GetHandModelMock(), GetHandModelMock());
    }
}