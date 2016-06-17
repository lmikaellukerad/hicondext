using UnityEngine;
using System.Collections;
using NUnit.Framework;

public class FingerTipDetectorTest
{

    [Test]
    public void InitializeFingerDetectorNameContainsNoEnd()
    {
        GameObject test = new GameObject("nope");
        Transform[] tips = new Transform[1];
        tips[0] = test.transform;
        FingerTipDetector detector = new FingerTipDetector(tips);
        Assert.IsNull(test.GetComponent<DetectFingerCollision>());
    }

    [Test]
    public void InitializeFingerDetectorNameContainsEnd()
    {
        GameObject test = new GameObject("end");
        Transform[] tips = new Transform[1];
        tips[0] = test.transform;
        FingerTipDetector detector = new FingerTipDetector(tips);
        Assert.IsNotNull(test.GetComponent<DetectFingerCollision>());
    }

}
