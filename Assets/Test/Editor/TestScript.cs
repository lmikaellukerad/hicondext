using UnityEngine;
using UnityEditor;
using NUnit.Framework;
/// <summary>
/// Example test script showing a failing and succeeding test.
/// </summary>
public class EditorTest {

    [Test]
    public void FailingTest()
    {
        var gameObject = new GameObject();
        var newGameObjectName = "Fail";
        gameObject.name = newGameObjectName;
        Assert.AreEqual("My game object", gameObject.name);
    }

    [Test]
    public void SucceedingTest()
    {   
        var gameObject = new GameObject();
        var newGameObjectName = "My game object";
        gameObject.name = newGameObjectName;
        Assert.AreEqual("My game object", gameObject.name);
    }
}
