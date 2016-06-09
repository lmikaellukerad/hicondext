using Leap;
using Leap.Unity;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class HighlightObjectTest {

    public float Radius = 0.01f;

    private int interactable = 8;
    private GameObject previousObject;
    private GameObject nearestObject;
    private Shader old;

    private GameObject mainObj;
    private GameObject testObj0;
    private GameObject testObj1;

	[SetUp]
	public void Setup()
	{
        mainObj = new GameObject();
        mainObj.GetComponent<Transform>().Translate(new Vector3(0.01f, 0f, 0f));
        mainObj.AddComponent<HighlightObject>();
        testObj0 = new GameObject();
        testObj0.GetComponent<Transform>().Translate(new Vector3(0.05f, 0f, 0f));
        testObj1 = new GameObject();
	}

    [Test]
    public void NoCloseObjectTest()
    {
        mainObj.GetComponent<HighlightObject>().Radius = 0.00001f;
        mainObj.GetComponent<HighlightObject>().DetectObject();
        Assert.IsEmpty(mainObj.GetComponent<HighlightObject>().Objects);
    }

    [Test]
    public void CloseObjectTest()
    {
        mainObj.GetComponent<HighlightObject>().Radius = 10f;
        mainObj.GetComponent<HighlightObject>().DetectObject();
        Assert.IsNotEmpty(mainObj.GetComponent<HighlightObject>().Objects);
    }
}
