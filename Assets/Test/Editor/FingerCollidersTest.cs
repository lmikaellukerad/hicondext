using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class FingerCollidersTest
{
    private GameObject testObj;
    private GameObject secondObj;
    private SphereCollider testCollider;
    private Rigidbody testRigid;

    private GameObject juan;
    private GameObject two;
    private GameObject three;

    [SetUp]
    public void Setup()
    {
        this.testObj = new GameObject();
        this.secondObj = new GameObject();
        this.juan = new GameObject("Palm");
        this.two = new GameObject();
        this.three = new GameObject();
        this.two.transform.SetParent(this.juan.transform);
        this.three.transform.SetParent(this.two.transform);
    }
    
	[Test]
    public void AddColliderTest()
	{
        Transform[] test = new Transform[1];
        test[0] = new GameObject("Test").transform;
        new FingerColliders(test);

        Assert.IsNotNull(test[0].gameObject.GetComponent<SphereCollider>());
	}

    [Test]
    public void AddCollidersTest()
    {
        Transform[] test = new Transform[1];
        test[0] = this.three.transform;
        new FingerColliders(test);

        Assert.IsNotNull(test[0].gameObject.GetComponent<SphereCollider>());
    }
}
