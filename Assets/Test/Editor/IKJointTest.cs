using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class IKJointTest 
{
    private InverseKinematicJoint testJoint;
    private Transform testTrans;
    private GameObject testObj0;
    private GameObject testObj1;
    private GameObject testObj2;

    [SetUp]
    public void Setup()
    {
        this.testObj0 = new GameObject("object0");
        this.testObj0.transform.Translate(2f, 1f, 3f, Space.Self);

        this.testObj1 = new GameObject("object1");
        this.testObj1.transform.Translate(1f, 2f, 3f, Space.Self);

        this.testObj2 = new GameObject("object2");
        this.testObj2.transform.Translate(3f, 2f, 1f, Space.Self);

        this.testJoint = new InverseKinematicJoint(this.testObj0.transform, this.testObj1.transform, this.testObj2.transform);

        this.testTrans = (new GameObject("joint_" + this.testObj0.name)).transform;
    }

	[Test]
    public void NameTest()
	{
        // Are the names right?
        Assert.AreEqual("joint_object0", "joint_" + this.testObj0.name);
	}

    [Test]
    public void DistanceTest()
    {
        // Is the distance calculated right?
        Assert.AreEqual(Vector3.Distance(this.testObj0.transform.position, this.testObj1.transform.position), this.testJoint.JointLength);
    }

    [Test]
    public void ParentTest()
    {
        // Is the parent set right?
        Assert.AreEqual(this.testObj0.transform.parent, this.testJoint.Joint);
    }

    [Test]
    public void ConstructJointTest()
    {
        // Is the joint set right?
        Vector3 look = this.testObj1.transform.position - this.testObj0.transform.position;
        Vector3 poleVector = this.testObj2.transform.position - this.testObj0.transform.position;
        Vector3.Normalize(look);
        Vector3 orth = poleVector - Vector3.Project(poleVector, look);

        Assert.AreEqual(this.testJoint.Joint.rotation, Quaternion.LookRotation(look, orth));
        Assert.AreEqual(this.testJoint.Joint.position, this.testObj0.transform.position);
    }

    [Test]
    public void SetLengthTest()
    {
        Assert.AreEqual(this.testJoint.JointLength, Vector3.Distance(this.testObj0.transform.position, this.testObj1.transform.position));
        this.testJoint.JointLength = 100f;
        Assert.AreEqual(this.testJoint.JointLength, 100f);
    }

    [Test]
    public void SetJointTest()
    {
        this.testJoint.Joint = this.testObj2.transform;
        Assert.AreEqual(this.testJoint.Joint, this.testObj2.transform);
    }
}
