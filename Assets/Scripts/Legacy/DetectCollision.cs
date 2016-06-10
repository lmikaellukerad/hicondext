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
    public Collider Collision
    {
        get;
        private set;
    }

    /// <summary>
    /// Called when collision is detected.
    /// </summary>
    /// <param name="c">The Collision object.</param>
    private void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.layer == 8)
        {
            this.Collided = true;
            this.Collision = c;
        }
    }

    /// <summary>
    /// Called when collision has ended.
    /// </summary>
    /// <param name="c">The Collision object.</param>
    private void OnTriggerExit(Collider c)
    {
        this.Collided = false;
        this.Collision = null;
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
