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

    /// <summary>
    /// Detects if there is a collision in the GameObject and saves the Collider if so.
    /// </summary>
    /// <param name="obj">The object to check.</param>
    /// <returns>true if collision is detected, otherwise false.</returns>
    public bool Detect(GameObject obj)
    {
        DetectCollision detector = obj.GetComponent<DetectCollision>();
        if (detector.Collided)
        {
            this.LastCollider = detector.Collision;
            return true;
        }
        
        return false;
    }

    /// <summary>
    /// Checks the finger for collision, should start at the tip.
    /// </summary>
    /// <param name="obj">The object to check.</param>
    /// <returns>true if collision is detected, otherwise false.</returns>
    private bool CheckFinger(GameObject obj)
    {
        if (obj.GetComponent<SphereCollider>().isTrigger)
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
            return false;
        }
    }
}
