using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leap.Unity;
using UnityEngine;

/// <summary>
/// Strategy that handles a grab with one hand.
/// </summary>
public abstract class SingleGrab : GrabStrategy
{
    protected HandModel hand;
    protected GameObject obj;
    private GameObject root;

    /// <summary>
    /// Destroys the root of the object.
    /// </summary>
    public override void Destroy()
    {
        this.root.transform.GetChild(0).transform.parent = this.root.transform.parent;
        //this.hand.palm.gameObject.layer = 0;
        //MonoBehaviour.print("the layer of hand palm has just changed to" + this.hand.palm.gameObject.layer);
        this.left.gameObject.layer = 0;
        this.right.gameObject.layer = 0;
        this.root.gameObject.layer = 0;
        GameObject.Destroy(this.root);
    }

    /// <summary>
    /// Constrains the hands to prevent them from passing through the grabbed object.
    /// </summary>
    /// <param name="grabbingFingers">The grabbing fingers.</param>
    public override void ConstrainHands(List<Transform> grabbingFingers)
    {
        this.HandleClamps(grabbingFingers);
        GrabStrategy.clampedFingers.Clear();
        GrabStrategy.clampedFingers.AddRange(grabbingFingers);
    }

    /// <summary>
    /// Updates the root object his position and rotation.
    /// </summary>
    public override void UpdateObject()
    {
        this.root.transform.position = this.hand.palm.position;
        this.root.transform.rotation = this.hand.palm.rotation;
        

    }

    /// <summary>
    /// Initializes this instance by adding a root for the object on the palm's position.
    /// </summary>
    protected void Init()
    {
        
        this.root = new GameObject("root");
        this.root.transform.parent = this.obj.transform.parent;
        this.root.transform.position = this.hand.palm.position;
        this.root.transform.rotation = this.hand.palm.rotation;
        this.obj.transform.parent = this.root.transform;
        this.left.gameObject.layer = 10;
        this.right.gameObject.layer = 10;
        this.root.gameObject.layer = 10;
        MonoBehaviour.print("the layer of" + this.obj.gameObject.name +"has changed");
    }
}
