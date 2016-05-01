// <copyright file="TestScript.cs" company="CondExt">
//     CondExt. All rights reserved.
// </copyright>
// <author>Jasper van Esveld</author>
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Example test cases showing a failing and succeeding test.
/// </summary>
public class TestScript
{
    /// <summary>
    /// Failing test.
    /// </summary>
    /*[Test]
    public void FailingTest()
    {
        var gameObject = new GameObject();
        var newGameObjectName = "Fail";
        gameObject.name = newGameObjectName;
        Assert.AreEqual("My game object", gameObject.name);
    }*/

    /// <summary>
    /// Succeeding test.
    /// </summary>
    [Test]
    public void SucceedingTest()
    {   
        var gameObject = new GameObject();
        var newGameObjectName = "My game object";
        gameObject.name = newGameObjectName;
        Assert.AreEqual("My game object", gameObject.name);
    }
}
