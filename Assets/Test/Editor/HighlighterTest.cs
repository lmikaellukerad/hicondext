﻿using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;
using Interfaces;

public class HighlighterTest {

    private HighlighterController GetMock()
    {
        var mock = Substitute.For<HighlighterController>();
        mock.Controller = Substitute.For<IHighlighterController>();
        mock.OverlapSphere = Substitute.For<IOverlapSphere>();
        return mock;
    }

    [Test]
    public void DetectObjectObjectsLengthNotZeroTest()
    {
        Collider[] cols = new Collider[1];
        cols[0] = new GameObject().AddComponent<BoxCollider>();
        var mock = GetMock();
        var controller = mock.Controller;
        mock.OverlapSphere.FindObjects().Returns(cols);
        controller.GetPosition().Returns(Vector3.zero);
        controller.ClearReceivedCalls();
        mock.DetectObjects();
        controller.Received().FindNearestObject(cols);
    }

    [Test]
    public void DetectObjectObjectsLengthZeroTest()
    {
        Collider[] cols = new Collider[0];
        var mock = GetMock();
        var controller = mock.Controller;
        mock.OverlapSphere.FindObjects().Returns(cols);
        controller.GetPosition().Returns(Vector3.zero);
        controller.ClearReceivedCalls();
        mock.DetectObjects();
        controller.Received().ResetObjects();
    }

    [Test]
    public void FindNearestObjectEmptyCols()
    {
        Collider[] cols = new Collider[0];
        var mock = GetMock();
        var controller = mock.Controller;
        mock.OverlapSphere.FindObjects().Returns(cols);
        controller.GetPosition().Returns(Vector3.zero);
        mock.UpdateNearestObject(cols);
        Assert.IsNull(mock.NearestObject);

    }

    [Test]
    public void FindNearestObjectNonEmptyCols()
    {
        Collider[] cols = new Collider[2];
        cols[0] = new GameObject().AddComponent<BoxCollider>();
        cols[1] = new GameObject().AddComponent<BoxCollider>();
        cols[0].transform.position = Vector3.zero;
        cols[1].transform.position = new Vector3(0, 0.1f, 0);
        var mock = GetMock();
        var controller = mock.Controller;
        mock.OverlapSphere.FindObjects().Returns(cols);
        controller.GetPosition().Returns(Vector3.zero);
        mock.UpdateNearestObject(cols);
        Assert.AreSame(cols[0], mock.NearestObject.GetComponent<Collider>());
    }

    [Test]
    public void ResetObjectIsNull()
    {
        var mock = GetMock();
        var controller = mock.Controller;
        mock.ResetObject(null);
        controller.DidNotReceive().CompareShaders(Arg.Any<GameObject>(), 
            Arg.Any<Shader>());
    }

    [Test]
    public void ResetObjectIsNotNull()
    {
        var mock = GetMock();
        var controller = mock.Controller;
        GameObject obj = new GameObject();
        mock.ResetObject(obj);
        controller.Received().SetShader(Arg.Any<GameObject>(),
            Arg.Any<Shader>());
    }

    [Test]
    public void CheckHiglightAlwaysCalled()
    {
        var mock = GetMock();
        var controller = mock.Controller;
        mock.Check();
        controller.Received().Highlight();
    }

    [Test]
    public void CheckPreviousObjectIsNull()
    {
        var mock = GetMock();
        var controller = mock.Controller;
        mock.Check();
        controller.DidNotReceive().CompareObjects(Arg.Any<GameObject>(),
            Arg.Any<GameObject>());
    }

    [Test]
    public void CheckPreviousObjectIsNotNull()
    {
        Collider[] cols = new Collider[2];
        cols[0] = new GameObject().AddComponent<BoxCollider>();
        cols[1] = new GameObject().AddComponent<BoxCollider>();
        cols[0].transform.position = Vector3.zero;
        cols[1].transform.position = new Vector3(0, 0.1f, 0);
        var mock = GetMock();
        var controller = mock.Controller;
        mock.UpdateNearestObject(cols);
        cols[0].transform.position = new Vector3(0, 0.1f, 0); 
        cols[1].transform.position = Vector3.zero;
        mock.UpdateNearestObject(cols);
        mock.Check();
        controller.Received().CompareObjects(Arg.Any<GameObject>(),
            Arg.Any<GameObject>());
    }

    [Test]
    public void CheckCompareAndReset()
    {
        Collider[] cols = new Collider[2];
        cols[0] = new GameObject().AddComponent<BoxCollider>();
        cols[1] = new GameObject().AddComponent<BoxCollider>();
        cols[0].transform.position = Vector3.zero;
        cols[1].transform.position = new Vector3(0, 0.1f, 0);
        var mock = GetMock();
        var controller = mock.Controller;
        mock.UpdateNearestObject(cols);
        cols[0].transform.position = new Vector3(0, 0.1f, 0);
        cols[1].transform.position = Vector3.zero;
        mock.UpdateNearestObject(cols);
        controller.CompareObjects(Arg.Any<GameObject>(),
            Arg.Any<GameObject>()).Returns(false);
        mock.Check();
        controller.Received().ResetObjects();
    }

    [Test]
    public void HighlightNearestObjectNull()
    {
        var mock = GetMock();
        var controller = mock.Controller;
        mock.Highlight();
        controller.DidNotReceive().SetShader(Arg.Any<GameObject>(), 
            Arg.Any<Shader>());
    }

    [Test]
    public void HighlightNearestObjectNotNull()
    {
        Collider[] cols = new Collider[2];
        cols[0] = new GameObject().AddComponent<BoxCollider>();
        var mock = GetMock();
        var controller = mock.Controller;
        mock.UpdateNearestObject(cols);
        mock.Highlight();
        controller.Received().SetShader(Arg.Any<GameObject>(),
            Arg.Any<Shader>());
    }




}
