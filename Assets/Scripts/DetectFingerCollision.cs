using System.Collections;
using UnityEngine;

/// <summary>
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

        Gizmos.DrawSphere(transform.position, this.Radius);
    }

    /// <summary>
    /// Checks the finger for collision using Physics.OverlapSphere.
    /// </summary>
    /// <returns>true if collision is found, else false</returns>
    public bool CheckFinger()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, this.Radius, 1 << 8);
        float minimumDistance = float.MaxValue;

        foreach (Collider c in colliders)
        {
            float currentDistance = Vector3.SqrMagnitude(transform.position - c.transform.position);
            minimumDistance = this.CheckDistance(minimumDistance, currentDistance, c);
        }

        return this.LastCollider != null;
    }

    /// <summary>
    /// Checks the distance.
    /// </summary>
    /// <param name="m">The minimum found distance.</param>
    /// <param name="c">The current found distance.</param>
    /// <param name="collider">The current collider.</param>
    /// <returns>Current distance if less than minimum distance, else the minimum distance</returns>
    private float CheckDistance(float m, float c, Collider collider) 
    {
        if (c < m)
        {
            this.LastCollider = collider;
            return c;
        }

        return m;
    }
}
