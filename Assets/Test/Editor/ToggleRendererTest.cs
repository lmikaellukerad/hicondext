using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class ToggleRendererTest 
{
    private MeshRenderer rend;

    private bool enableRender;

    private GameObject testObject;

	[SetUp]
    public void Setup()
    {
        this.testObject = new GameObject();
        this.rend = this.testObject.AddComponent<MeshRenderer>();
        this.rend.enabled = false;
        testObject.AddComponent<ToggleRenderer>();
        this.enableRender = false;
    }
    
    [Test]
	public void RendererOffTest()
	{
        Assert.False(this.rend.enabled);
	}

    [Test]
    public void RendererOffToOnTest()
    {
        Assert.False(this.rend.enabled);
        this.testObject.GetComponent<ToggleRenderer>().EnableRender = true;
        Assert.False(this.rend.enabled);
        this.testObject.GetComponent<ToggleRenderer>().EnableRenderer();
        Assert.True(this.rend.enabled);
    }

    [Test]
    public void RendererOffToOffTest()
    {
        Assert.False(this.rend.enabled);
        this.testObject.GetComponent<ToggleRenderer>().EnableRenderer();
        Assert.False(this.rend.enabled);
    }

    [Test]
    public void RendererOnToOnTest()
    {
        this.rend.enabled = true;
        this.testObject.GetComponent<ToggleRenderer>().EnableRender = true;
        Assert.True(this.rend.enabled);
        this.testObject.GetComponent<ToggleRenderer>().EnableRenderer();
        Assert.True(this.rend.enabled);
    }

    [Test]
    public void StartTest()
    {
        this.testObject.GetComponent<ToggleRenderer>().EnableRender = true;
        Assert.False(this.rend.enabled);
        this.testObject.GetComponent<ToggleRenderer>().Start();
        Assert.True(this.rend.enabled);
    }
}
