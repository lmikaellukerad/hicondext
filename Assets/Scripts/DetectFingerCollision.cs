using System.Collections;
using UnityEngine;

/// <summary>
/// Detect collision in whole finger
/// </summary>
public class DetectFingerCollision : MonoBehaviour
{
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

    public float radius = 0.01f;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(transform.position, radius);
    }

    /// <summary>
    /// Detects if there is a collision in the GameObject and saves the Collider if so.
    /// </summary>
    /// <param name="obj">The object to check.</param>
    /// <returns>true if collision is detected, otherwise false.</returns>
    private bool Detect(GameObject obj)
    {
        DetectCollision detector = obj.GetComponent<DetectCollision>();
        if (detector.Collided)
        {
            this.LastCollider = detector.Collision;
            return true;
        }

        return false;
    }

    public bool CheckFinger()
    {

        Collider[] objects = Physics.OverlapSphere(transform.position, radius, 1 << 8);
        float minimumDistance = float.MaxValue;
        Collider col = null;
        // check what object is closest to our pinch, this object is the grabbed object
        for (int i = 0; i < objects.Length; i++)
        {
            Collider o = objects[i];
            float currentDistance = Vector3.Distance(transform.position, o.GetComponent<Transform>().position);
            if (currentDistance < minimumDistance)
            {
                col = o;
                minimumDistance = currentDistance;
            }
        }
        LastCollider = col;
        return col != null;
    }

    public void SetRadius(GameObject obj, float rad)
    {
        this.radius = rad;
        if (obj.GetComponent<SphereCollider>() != null)
        {
            obj.GetComponent<SphereCollider>().radius = radius;
            this.SetRadius(obj.transform.parent.gameObject,radius);
        }
    }

    /// <summary>
    /// Checks the finger for collision, should start at the tip.
    /// </summary>
    /// <param name="obj">The object to check.</param>
    /// <returns>true if collision is detected, otherwise false.</returns>
    public bool CheckFinger(GameObject obj)
    {
        if (obj.GetComponent<SphereCollider>()  != null)
        {
            if (this.Detect(obj))
            {
                return true;
            }
            else
            {
                return this.CheckFinger(obj.transform.parent.gameObject);
            }
        }
        else
        {
            LastCollider = null;
            return false;
        }
    }
}
