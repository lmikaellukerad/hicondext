using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class KinectHandDropTest 
{
    private GameObject testObj;
    private GameObject palm;
    private GameObject wrist;

    [SetUp]
    public void Setup()
    {
        this.testObj = new GameObject();
        this.testObj.AddComponent<KinectHandDrop>();

        this.palm = new GameObject();
        this.palm.GetComponent<Transform>().position = new Vector3(1f, 1f, 1f);

        this.wrist = new GameObject();
        this.wrist.GetComponent<Transform>().position = new Vector3(2f, 2f, 2f);
    }

	[Test]
	public void AwakeTest()
    {
        this.testObj.GetComponent<KinectHandDrop>().SetTracking(false);
        Assert.False(this.testObj.GetComponent<KinectHandDrop>().GetTracking());
        this.testObj.GetComponent<KinectHandDrop>().PublicAwake();
        Assert.True(this.testObj.GetComponent<KinectHandDrop>().GetTracking());
	}

    [Test]
    public void HandFinishTest()
    {
        this.testObj.GetComponent<KinectHandDrop>().SetTracking(true);
        Assert.True(this.testObj.GetComponent<KinectHandDrop>().GetTracking());
        this.testObj.GetComponent<KinectHandDrop>().PublicHandFinish();
        Assert.False(this.testObj.GetComponent<KinectHandDrop>().GetTracking());
    }

    [Test]
    public void HandResetTest()
    {
        this.testObj.GetComponent<KinectHandDrop>().SetTracking(false);
        Assert.False(this.testObj.GetComponent<KinectHandDrop>().GetTracking());
        this.testObj.GetComponent<KinectHandDrop>().PublicHandReset();
        Assert.True(this.testObj.GetComponent<KinectHandDrop>().GetTracking());
    }

    [Test]
    public void UpdateTest()
    {
        this.testObj.GetComponent<KinectHandDrop>().SetTracking(false);
        this.testObj.GetComponent<KinectHandDrop>().Palm = this.palm.GetComponent<Transform>();
        this.testObj.GetComponent<KinectHandDrop>().Wrist = this.wrist.GetComponent<Transform>();

        Assert.AreEqual(this.palm.GetComponent<Transform>().position, this.testObj.GetComponent<KinectHandDrop>().Palm.position);

        this.testObj.GetComponent<KinectHandDrop>().Update();

        Assert.AreEqual(this.wrist.GetComponent<Transform>().position, this.testObj.GetComponent<KinectHandDrop>().Palm.position);
    }
}
