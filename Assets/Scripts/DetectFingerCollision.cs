using System.Collections;
using UnityEngine;

/// <summary>
/// Author: Luke
/// Detect collision in whole finger
/// </summary>
public class DetectFingerCollision : MonoBehaviour
{
    public float Radius = 0.01f;

    /// <summary>
    /// Gets the object.
    /// </summary>
    /// <value>
    /// The Collision object.
    /// </value>
    public Collider LastCollider
    {
        get;
        private set;
    }

    /// <summary>
    /// Used for debug.
    /// </summary>
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 offset = new Vector3(0, 0, -0.01f);
        Gizmos.DrawSphere(transform.position + (transform.rotation * offset), this.Radius);
    }

    /// <summary>
    /// Checks the finger for collision using Physics.OverlapSphere.
    /// </summary>
    /// <returns>true if collision is found, else false</returns>
    public bool CheckFinger()
    {
        Vector3 offset = new Vector3(0, 0, -0.01f);
        Collider[] colliders = Physics.OverlapSphere(
            transform.position + (transform.rotation * offset), 
            this.Radius, 
            1 << 8 | 1 << 11);
     
        float minimumDistance = float.MaxValue;

        this.LastCollider = null;

        foreach (Collider collider in colliders)
        {
            float currentDistance = (transform.position - collider.transform.position).sqrMagnitude;
            minimumDistance = this.CheckDistance(minimumDistance, currentDistance, collider);
        }

        return this.LastCollider != null;
    }

    /// <summary>
    /// Checks the distance.
    /// </summary>
    /// <param name="minimum">The minimum found distance.</param>
    /// <param name="current">The current found distance.</param>
    /// <param name="collider">The current collider.</param>
    /// <returns>Current distance if less than minimum distance, else the minimum distance</returns>
    private float CheckDistance(float minimum, float current, Collider collider) 
    {
        if (current < minimum)
        {
            this.LastCollider = collider;
            return current;
        }

        return minimum;
    }
}
