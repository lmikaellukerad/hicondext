using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class ToggleRendererTest {

    private MeshRenderer rend;

    private bool enableRender;

    private GameObject testObject;

    private ToggleRenderer testToggler;

	[SetUp]
    public void Setup()
    {
        this.testObject = new GameObject();
        this.rend = testObject.AddComponent<MeshRenderer>();
        this.rend.enabled = false;
        this.testToggler = testObject.AddComponent<ToggleRenderer>();
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
        testToggler.EnableRender = true;
        Assert.False(this.rend.enabled);
        testToggler.EnableRenderer();
        Assert.True(this.rend.enabled);
    }

    [Test]
    public void RendererOffToOffTest()
    {
        Assert.False(this.rend.enabled);
        testToggler.EnableRenderer();
        Assert.False(this.rend.enabled);
    }

    [Test]
    public void RendererOnToOnTest()
    {
        this.rend.enabled = true;
        testToggler.EnableRender = true;
        Assert.True(this.rend.enabled);
        testToggler.EnableRenderer();
        Assert.True(this.rend.enabled);
    }
}
