using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections;

public class IKTestScript {
	private Transform start;
	private Transform center;
	private Transform end;
	private Transform goal;
	private Transform pole;
	private IKScript script;

	[SetUp]
	public void Setup()
	{
		start = new GameObject("root").transform;
		center = new GameObject("first").transform;
		end = new GameObject("second").transform;
		pole = new GameObject("pole").transform;
		goal = new GameObject("goal").transform;

		center.parent = start;
		end.parent = center;

		start.position = new Vector3(0, 0, 0);
		center.position = new Vector3(0, 5, 0);
		end.position = new Vector3(0, 10, 0);
		pole.position = new Vector3(0, 0, -5);
		goal.position = new Vector3(0, 10, 0);

		script = (new GameObject()).AddComponent<IKScript>();

		script.ChainStart = start;
		script.ChainEnd = end;
		script.ConnectPole = false;
		script.Goal = goal;
		script.Pole = pole;
	}

	[Test]
	public void ChainFoundTest()
	{
		script.Start();
		Assert.True(script.chainFound);
		center.parent = null;
		end.parent = null;
		script.Start();
		Assert.False(script.chainFound);
	}

    [Test]
    public void IKTestOne()
    {
        goal.position = new Vector3(0, 5, 5);
        script.Start();
        script.Update();
        Assert.True(Vector3.Distance(goal.position, end.position) < 0.001);
    }

    [Test]
    public void PoleTestOne()
    {
        goal.position = new Vector3(0, 5, 0);
        script.Start();
        script.Update();
        Assert.Greater(center.position.z,0);
    }

    [Test]
    public void PoleTestTwo()
    {
        goal.position = new Vector3(0, 5, 0);
        pole.position = new Vector3(0, 0, 5);
        script.Start();
        script.Update();
        Assert.Greater(0, center.position.z);
    }
}
