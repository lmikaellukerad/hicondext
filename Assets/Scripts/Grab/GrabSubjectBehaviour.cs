using System;
using System.Collections.Generic;
using Leap;
using Leap.Unity;
using UnityEngine;

public abstract class GrabSubjectBehaviour : MonoBehaviour
{
    public List<GrabObserver> Grabs = new List<GrabObserver>();
    private GrabSubjectState previousState;

    public GrabSubjectState State
    {
        get;
        set;
    }

    /// <summary>
    /// Subscribes the specified observer.
    /// </summary>
    /// <param name="observer">The observer.</param>
    public void Subscribe(GrabObserver observer)
    {
        this.Grabs.Add(observer);
    }

    /// <summary>
    /// UnSubscribes the specified observer.
    /// </summary>
    /// <param name="observer">The observer.</param>
    public void UnSubscribe(GrabObserver observer)
    {
        this.Grabs.Remove(observer);
    }

    /// <summary>
    /// Call the initialize function on start.
    /// </summary>
    public void Start()
    {
        this.Initialize();
    }

    /// <summary>
    /// Handle the state on update.
    /// </summary>
    public void Update()
    {
        this.State.Handle(this);

        // Debug log
        if (!this.State.Equals(this.previousState))
        {
            Debug.Log(this.State);
        }

        this.previousState = this.State;
    }

    public abstract void Initialize();

    public abstract void Detect();

    public abstract void NotifyGrabs();
}