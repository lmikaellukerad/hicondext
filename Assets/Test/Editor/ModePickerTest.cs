using Leap.Unity;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class ModePickerTest 
{
    private GameObject testObj;
    private GameObject childObj;
    private Vector3 virRotation;
    private Vector3 virPosition;
    private Vector3 nonVrPosition;
    private Vector3 nonVrRotation;

    [SetUp]
    public void Setup()
    {
        this.testObj = new GameObject("testobj");
        this.testObj.AddComponent<ModePicker>();
        this.testObj.AddComponent<LeapVRTemporalWarping>();

        this.childObj = new GameObject("childobj");
        this.childObj.transform.parent = this.testObj.transform;
        this.childObj.AddComponent<LeapServiceProvider>();

        this.virPosition = new Vector3(0, 0, 0);
        this.virRotation = new Vector3(-90, 180, 0);
        this.nonVrPosition = new Vector3(0, -0.3f, 0.4f);
        this.nonVrRotation = new Vector3(0, 0, 0);
    }

    ////////////////////////////////////////////////////////////////////////////
    /////////////////////// NON-VR MODE TESTS FROM HERE ////////////////////////
    ////////////////////////////////////////////////////////////////////////////

	[Test]
	public void NonVRRotationTest()
	{
        // Intentionally set these vectors to the wrong values.
        this.testObj.GetComponent<ModePicker>().transform.localPosition = this.virPosition;
        this.testObj.GetComponent<ModePicker>().transform.localRotation = Quaternion.Euler(this.virRotation);

        // Run the script.
        this.testObj.GetComponent<ModePicker>().VRMode = false;
        this.testObj.GetComponent<ModePicker>().Start();

        // Check if the values are now set correctly.
        Assert.AreEqual(this.testObj.GetComponent<ModePicker>().transform.localPosition, this.nonVrPosition);
        Assert.AreEqual(this.testObj.GetComponent<ModePicker>().transform.localRotation, Quaternion.Euler(this.nonVrRotation));
	}

    [Test]
    public void NonVRLeapServiceProviderTest()
    {
        // Intentionally set these booleans to the wrong value.
        this.testObj.transform.GetChild(0).GetComponent<LeapServiceProvider>()._isHeadMounted = true;
        this.testObj.transform.GetChild(0).GetComponent<LeapServiceProvider>().overrideDeviceType = true;

        // Run the script.
        this.testObj.GetComponent<ModePicker>().VRMode = false;
        this.testObj.GetComponent<ModePicker>().Start();

        // Assert that the booleans are now correct.
        Assert.False(this.testObj.transform.GetChild(0).GetComponent<LeapServiceProvider>()._isHeadMounted);
        Assert.False(this.testObj.transform.GetChild(0).GetComponent<LeapServiceProvider>().overrideDeviceType);
    }

    [Test]
    public void NonVRWarpingTest()
    {
        this.testObj.GetComponent<ModePicker>().VRMode = false;
        this.testObj.GetComponent<ModePicker>().Start();

        Assert.False(this.testObj.GetComponent<LeapVRTemporalWarping>().enabled);
    }

    ////////////////////////////////////////////////////////////////////////////
    ///////////////////////// VR MODE TESTS FROM HERE //////////////////////////
    ////////////////////////////////////////////////////////////////////////////

    [Test]
    public void VRRotationTest()
    {
        // Intentionally set these vectors to the wrong values.
        this.testObj.GetComponent<ModePicker>().transform.localPosition = this.nonVrPosition;
        this.testObj.GetComponent<ModePicker>().transform.localRotation = Quaternion.Euler(this.nonVrRotation);

        // Run the script.
        this.testObj.GetComponent<ModePicker>().VRMode = true;
        this.testObj.GetComponent<ModePicker>().Start();

        // Check if the values are now set correctly.
        Assert.AreEqual(this.testObj.GetComponent<ModePicker>().transform.localPosition, this.virPosition);

        // Had to compare ToString here, due to the Equals method of the Rotation class screwing up. (0,0.7,0.7,0) did not equal (0,0.7,0.7,0) somehow.
        Assert.AreEqual(this.testObj.GetComponent<ModePicker>().transform.localRotation.ToString(), Quaternion.Euler(this.virRotation).ToString());
    }

    [Test]
    public void VRLeapServiceProviderTest()
    {
        // Intentionally set these booleans to the wrong value.
        this.testObj.transform.GetChild(0).GetComponent<LeapServiceProvider>()._isHeadMounted = false;
        this.testObj.transform.GetChild(0).GetComponent<LeapServiceProvider>().overrideDeviceType = false;

        // Run the script.
        this.testObj.GetComponent<ModePicker>().VRMode = true;
        this.testObj.GetComponent<ModePicker>().Start();

        // Assert that the vooleans are now correct.
        Assert.True(this.testObj.transform.GetChild(0).GetComponent<LeapServiceProvider>()._isHeadMounted);
        Assert.True(this.testObj.transform.GetChild(0).GetComponent<LeapServiceProvider>().overrideDeviceType);
    }

    [Test]
    public void VRWarpingTest()
    {
        this.testObj.GetComponent<ModePicker>().VRMode = true;
        this.testObj.GetComponent<ModePicker>().Start();

        Assert.True(this.testObj.GetComponent<LeapVRTemporalWarping>().enabled);
    }
}
