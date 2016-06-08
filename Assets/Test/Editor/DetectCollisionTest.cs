using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class DetectCollisionTest
{

    private GameObject testObj;

    [SetUp]
    public void setup()
    {
        testObj = new GameObject("testobj");
        testObj.AddComponent<DetectCollision>();
    }

    [Test]
    public void NotCollidedTest()
    {
        Assert.False(testObj.GetComponent<DetectCollision>().Collided);
    }

    [Test]
    public void CollidedBooleanTest()
    {
        Collision c = new Collision();
        testObj.GetComponent<DetectCollision>().CollisionEnterSimulator(c);
        Assert.True(testObj.GetComponent<DetectCollision>().Collided);
    }

    [Test]
    public void CollidedObjectTest()
    {
        Collision c = new Collision();
        testObj.GetComponent<DetectCollision>().CollisionEnterSimulator(c);
        Assert.AreEqual(testObj.GetComponent<DetectCollision>().Object, c);
    }

    [Test]
    public void ExitBooleanTest()
    {
        Collision c = new Collision();
        testObj.GetComponent<DetectCollision>().CollisionExitSimulator(c);
        Assert.False(testObj.GetComponent<DetectCollision>().Collided);
    }

    [Test]
    public void ExitObjectTest()
    {
        Collision c = new Collision();
        testObj.GetComponent<DetectCollision>().CollisionExitSimulator(c);
        Assert.AreEqual(testObj.GetComponent<DetectCollision>().Object, null);
    }
}