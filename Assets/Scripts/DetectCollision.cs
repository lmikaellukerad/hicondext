using System.Collections;
using UnityEngine;

/// <summary>
/// Author: Luke
/// Script to detect collision of SphereCollider.
/// </summary>
public class DetectCollision : MonoBehaviour
{

    /// <summary>
    /// Gets a value indicating whether this <see cref="DetectCollision"/> has collided.
    /// </summary>
    /// <value>
    ///   <c>true</c> if collided; otherwise, <c>false</c>.
    /// </value>
    public bool Collided
    {
        get;
        private set;
    }

    /// <summary>
    /// Gets the object.
    /// </summary>
    /// <value>
    /// The Collision object.
    /// </value>
    public Collision Object
    {
        get;
        private set;
    }

    /// <summary>
    /// Called when collision is detected.
    /// </summary>
    /// <param name="c">The Collision object.</param>
    private void OnCollisionEnter(Collision c)
    {
        this.Collided = true;
        this.Object = c;
    }

    /// <summary>
    /// Called when collision has ended.
    /// </summary>
    /// <param name="c">The Collision object.</param>
    private void OnCollisionExit(Collision c)
    {
        this.Collided = false;
        this.Object = null;
    }

    /// <summary>
    /// Called when collision is detected. Used for testing purposes.
    /// </summary>
    /// <param name="c">The Collision object.</param>
    public void CollisionEnterSimulator(Collision c)
    {
        OnCollisionEnter(c);
    }

    /// <summary>
    /// Called when collision has ended. Used for testing purposes.
    /// </summary>
    /// <param name="c">The Collision object.</param>
    public void CollisionExitSimulator(Collision c)
    {
        OnCollisionExit(c);
    }
}
