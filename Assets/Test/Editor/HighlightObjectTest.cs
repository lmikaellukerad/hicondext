using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;
using Interfaces;

public class HighlightObjectTest : MonoBehaviour {

    [SetUp]
    public void setup()
    {

    }

    private HighlighterController GetMock()
    {
        var mock = Substitute.For<HighlighterController>();
        mock.Controller = this.GetController();
        return mock;
    }

    private IHighlighterController GetController()
    {
        return Substitute.For<IHighlighterController>();
    }

    [Test]
    public void DetectObjectObjectsLengthNotZeroTest()
    {
        Collider[] cols = new Collider[1];
        cols[0] = new GameObject().AddComponent<BoxCollider>();
        var mock = GetMock();
        mock.FindObjects().Returns(cols);
        mock.GetPosition().Returns(Vector3.zero);
        mock.ClearReceivedCalls();
        mock.DetectObjects();
        mock.DidNotReceive().FindNearestObject(cols);
    }

    [Test]
    public void DetectObjectObjectsLengthZeroTest()
    {
        Collider[] cols = new Collider[0];
        var mock = GetMock();
        mock.FindObjects().Returns(cols);
        mock.GetPosition().Returns(Vector3.zero);
        mock.ClearReceivedCalls();
        mock.DetectObjects();
        mock.Received().ResetObjects();
    }

    [Test]
    public void FindNearestObjectEmptyCols()
    {
        Collider[] cols = new Collider[0];
        var mock = GetMock();
        mock.FindObjects().Returns(cols);
        mock.GetPosition().Returns(Vector3.zero);
        mock.Controller.ClearReceivedCalls();
        mock.FindNearestObject(cols);
        mock.Controller.DidNotReceive().GetPosition();

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
        mock.FindObjects().Returns(cols);
        controller.GetPosition().Returns(Vector3.zero);
        controller.ClearReceivedCalls();
        mock.FindNearestObject(cols);
        controller.DidNotReceive().GetPosition();
    }


}
