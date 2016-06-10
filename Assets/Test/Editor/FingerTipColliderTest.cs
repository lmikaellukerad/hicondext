using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class FingerTipColliderTest
{
    private GameObject testObj;
    private GameObject secondObj;
    private SphereCollider testCollider;
    private Rigidbody testRigid;

    private GameObject juan;
    private GameObject two;
    private GameObject tree;

    [SetUp]
    public void Setup()
    {
        this.testObj = new GameObject();
        this.testObj.AddComponent<FingerTipCollider>();
        this.secondObj = new GameObject();
        this.juan = new GameObject("Palm");
        this.two = new GameObject();
        this.tree = new GameObject();
        this.two.GetComponent<Transform>().SetParent(this.juan.GetComponent<Transform>());
        this.tree.GetComponent<Transform>().SetParent(this.two.GetComponent<Transform>());
    }
    /*
	[Test]
    public void AddColliderTest()
	{
        Assert.IsNull(this.secondObj.GetComponent<FingerTipCollider>());
        this.testObj.GetComponent<FingerTipCollider>().AddCollider(this.secondObj);

        GameObject temp = new GameObject();
        this.testCollider = temp.AddComponent<SphereCollider>();
        this.testCollider.radius = 0.03f;

        // Testing if the collider is exactly the same is not possible, due to Material object not being creatable from scripts, so comparing radius has to suffice here.
        Assert.AreEqual(this.testCollider.radius, this.secondObj.GetComponent<SphereCollider>().radius);
	}

    [Test]
    public void AddRigidbodyTest()
    {
        this.testObj.GetComponent<FingerTipCollider>().AddRigidbody(this.secondObj);

        GameObject temp = new GameObject();
        this.testRigid = temp.AddComponent<Rigidbody>();
        this.testRigid.useGravity = false;
        this.testRigid.isKinematic = true;

        Assert.AreEqual(this.testRigid.useGravity, this.secondObj.GetComponent<Rigidbody>().useGravity);
        Assert.AreEqual(this.testRigid.isKinematic, this.secondObj.GetComponent<Rigidbody>().isKinematic);
    }

    [Test]
    public void AddCollisionDetectionTest()
    {
        this.testObj.GetComponent<FingerTipCollider>().AddCollisionDetection(this.secondObj);
        Assert.IsNotNull(this.secondObj.GetComponent<DetectCollision>());
    }

    [Test]
    public void InitializeFingerColliderTest()
    {
        this.testObj.GetComponent<FingerTipCollider>().InitializeFingerCollider(this.secondObj);
        Assert.IsNotNull(this.secondObj.GetComponent<SphereCollider>());
        Assert.IsNotNull(this.secondObj.GetComponent<Rigidbody>());
        Assert.IsNotNull(this.secondObj.GetComponent<DetectCollision>());
    }

    [Test]
    public void StartFingersTest()
    {
        this.testObj.GetComponent<FingerTipCollider>().FingerTips = new Transform[1];
        this.testObj.GetComponent<FingerTipCollider>().FingerTips[0] = this.tree.GetComponent<Transform>();
        this.testObj.GetComponent<FingerTipCollider>().Start();

        Assert.IsNotNull(this.two.GetComponent<SphereCollider>());
        Assert.IsNotNull(this.two.GetComponent<Rigidbody>());
        Assert.IsNotNull(this.two.GetComponent<DetectCollision>());

        Assert.IsNotNull(this.tree.GetComponent<SphereCollider>());
        Assert.IsNotNull(this.tree.GetComponent<Rigidbody>());
        Assert.IsNotNull(this.tree.GetComponent<DetectCollision>());
    }

    [Test]
    public void StartThumbTest()
    {
        this.testObj.GetComponent<FingerTipCollider>().FingerTips = new Transform[0];
        this.testObj.GetComponent<FingerTipCollider>().Thumb = this.tree.GetComponent<Transform>();
        this.testObj.GetComponent<FingerTipCollider>().Start();

        Assert.IsNotNull(this.two.GetComponent<SphereCollider>());
        Assert.IsNotNull(this.two.GetComponent<Rigidbody>());
        Assert.IsNotNull(this.two.GetComponent<DetectCollision>());

        Assert.IsNotNull(this.tree.GetComponent<SphereCollider>());
        Assert.IsNotNull(this.tree.GetComponent<Rigidbody>());
        Assert.IsNotNull(this.tree.GetComponent<DetectCollision>());
    }
    */
}
