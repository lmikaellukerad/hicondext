using System.Collections;
using Leap;
using Leap.Unity;
using UnityEngine;

/// <summary>
/// Author: Luke
/// This class adds a object highlight functionality. 
/// It looks for nearby objects within a radius, with the center being the transform position.
/// For the most optimal behavior in conjunction with FixedJointGrab, 
/// this script should be attached to the thumb transform of the hand model with the radius set to 0.01.
/// </summary>
public class HighlightObject : MonoBehaviour
{
    public float Radius = 0.01f;

    private int interactable = 8;
    private GameObject previousObject;
    private GameObject nearestObject;
    private Shader old;
    private Collider[] objects;

    /// <summary>
    /// This method detects objects within range.
    /// </summary>
    public void DetectObject()
    {
        Vector3 pos = transform.position;
        objects = Physics.OverlapSphere(pos, this.Radius, 1 << this.interactable);
        if (objects.Length > 0)
        {
            this.FindNearest(objects, pos);
        }
        else
        {
            this.Clear();
        }    
    }

    /// <summary>
    /// Resets the previous and nearest object.
    /// </summary>
    private void Clear()
    {
        if (this.previousObject != null)
        {
            this.previousObject.GetComponent<Renderer>().material.shader = this.old;
        }

        if (this.nearestObject != null)
        {
            this.nearestObject.GetComponent<Renderer>().material.shader = this.old;
            this.nearestObject = null;
        }
    }

    /// <summary>
    /// Finds the nearest object within the objects found within range.
    /// </summary>
    /// <param name="objects">The objects.</param>
    /// <param name="pos">The position.</param>
    private void FindNearest(Collider[] objects, Vector3 pos)
    {
        this.previousObject = this.nearestObject;
        float minimumDistance = float.MaxValue;
        foreach (Collider o in objects)
        {
            float currentDistance = (pos - o.GetComponent<Transform>().position).sqrMagnitude;
            if (currentDistance < minimumDistance)
            {
                this.nearestObject = o.gameObject;
                minimumDistance = currentDistance;
            }
        }

        this.Check();
    }

    /// <summary>
    /// Checks if the nearest object was changed so that the old object can be reset and the new object highlighted.
    /// </summary>
    private void Check()
    {
        if (this.previousObject != null)
        { 
            if (!this.previousObject.Equals(this.nearestObject))
            {
                this.Clear();
                this.Highlight();
            }
        }
        else
        {
            this.Highlight();
        }
    }

    /// <summary>
    /// Highlights the nearest object by changing the shader.
    /// </summary>
    private void Highlight()
    {
        if (this.nearestObject != null)
        {
            this.old = this.nearestObject.GetComponent<Renderer>().material.shader;
            this.nearestObject.GetComponent<Renderer>().material.shader = 
                Shader.Find("Outlined/Silhouetted Bumped Diffuse");
        }
    }

    /// <summary>
    /// Updates this instance every frame.
    /// </summary>
    private void Update()
    {
        this.DetectObject();
    }

    public GameObject PreviousObject
    {
        get { return previousObject; }
        set { previousObject = value; }
    }

    public GameObject NearestObject
    {
        get { return nearestObject; }
        set { nearestObject = value; }
    }

    public Collider[] Objects
    {
        get { return objects; }
        set { objects = value; }
    }
}
