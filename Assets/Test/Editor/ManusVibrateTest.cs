using System.Collections;
using System.Diagnostics;
using System.Threading;
using ManusMachina;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using WindowsInput;

public class ManusVibrateTest 
{
    /// <summary>
    /// A ManusVibrate object for testing purposes.
    /// </summary>
    private ManusVibrate testVib;

    [SetUp]
    public void Setup()
    {
        this.testVib = new ManusVibrate();
    }

	[Test]
	public void InitTest()
	{
        Assert.False(this.testVib.GetVibrating());
	}

    [Test]
    public void OnTest()
    {
        this.testVib.VibrateOn();
        Assert.True(this.testVib.GetVibrating());
    }

    [Test]
    public void OffTest()
    {
        this.testVib.VibrateOn();
        this.testVib.VibrateOff();
        Assert.False(this.testVib.GetVibrating());
    }

    [Test]
    public void VibrateForTest()
    {
        Assert.False(this.testVib.GetVibrating());
        Stopwatch timer = new Stopwatch();
        timer.Start();
        Assert.AreNotEqual(null, this.testVib.VibrateForCoroutine(0.5f));
        while (timer.ElapsedMilliseconds < 510) 
        { 
            Thread.Sleep(1); 
        }

        Assert.False(this.testVib.GetVibrating());
    }

    [Test]
    public void DoubleShortVibrationCoroutineTest()
    {
        Assert.False(this.testVib.GetVibrating());
        Stopwatch timer = new Stopwatch();
        timer.Start();
        Assert.AreNotEqual(null, this.testVib.DoubleShortVibrationCoroutine());
        while (timer.ElapsedMilliseconds < 410) 
        { 
            Thread.Sleep(1); 
        }

        Assert.False(this.testVib.GetVibrating());
    }
}
