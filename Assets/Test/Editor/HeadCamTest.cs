using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class HeadCamTest 
{
    private GameObject testObj;
    private GameObject testHead;
    private Quaternion testInitial;

    [SetUp]
    public void Setup()
    {
        this.testInitial = Quaternion.Euler(new Vector3(1f, 2f, 3f));

        this.testHead = new GameObject("testhead");
        this.testHead.GetComponent<Transform>().transform.position = new Vector3(1f, 1f, 1f);
        this.testHead.GetComponent<Transform>().transform.rotation = Quaternion.Euler(new Vector3(2f, 2f, 2f));

        this.testObj = new GameObject("testobj");
        this.testObj.AddComponent<HeadCam>();
        this.testObj.GetComponent<Transform>().transform.rotation = this.testInitial;

        this.testObj.GetComponent<HeadCam>().Head = null;
        this.testObj.GetComponent<HeadCam>().Start();
    }


    [Test]
    public void UpdateWithNullTest()
    {
        Assert.AreEqual(this.testObj.GetComponent<HeadCam>().Head, null);

        // Nothing should have changed.
        Assert.AreEqual(this.testObj.GetComponent<Transform>().transform.position, this.testObj.GetComponent<Transform>().transform.position);
    }

    [Test]
    public void UpdateTest()
    {
        this.testObj.GetComponent<HeadCam>().Head = this.testHead;
        Assert.AreEqual(this.testObj.GetComponent<HeadCam>().Head, this.testHead);
        this.testObj.GetComponent<HeadCam>().Update();

        // Position and rotation should have been updated.
        Assert.AreEqual(this.testObj.GetComponent<Transform>().transform.position, this.testHead.GetComponent<Transform>().transform.position);
        Assert.AreEqual(this.testObj.GetComponent<Transform>().transform.rotation, Quaternion.Euler(this.testInitial.eulerAngles + this.testHead.GetComponent<Transform>().rotation.eulerAngles));
    }
}
