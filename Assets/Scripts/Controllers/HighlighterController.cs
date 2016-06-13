using System.Collections;
using Interfaces;
using UnityEngine;

/// <summary>
/// This is the controller for the Highlighter class, all logic is here.
/// </summary>
public class HighlighterController
{
    public float Radius = 0.01f;

    public IHighlighterController Controller;
    public IOverlapSphere OverlapSphere;

    public GameObject NearestObject
    {
        get;
        private set;
    }

    public GameObject PreviousObject
    {
        get;
        private set;
    }

    /// <summary>
    /// Detects the nearby objects.
    /// </summary>
    public void DetectObjects()
    {
        Collider[] cols = this.OverlapSphere.FindObjects();
        if (cols.Length > 0)
        {
            Debug.Log("Detecting");
            this.Controller.FindNearestObject(cols);
        }
        else
        {
            this.Controller.ResetObjects();
        }
    }

    /// <summary>
    /// Update the nearest object.
    /// </summary>
    /// <param name="colliders">The colliders to search the nearest object for.</param>
    public void UpdateNearestObject(Collider[] colliders)
    {
        this.PreviousObject = this.NearestObject;
        float minimumDistance = float.MaxValue;
        foreach (Collider collider in colliders)
        {
            float currentDistance = (this.Controller.GetPosition() 
                - this.Controller.GetPosition(collider)).sqrMagnitude;
            if (currentDistance < minimumDistance)
            {
                this.NearestObject = collider.gameObject;
                minimumDistance = currentDistance;
            }
        }

        this.Controller.Check();
    }

    /// <summary>
    /// Resets the nearest object and the previous object.
    /// </summary>
    public void ResetObjects()
    {
        this.ResetObject(this.PreviousObject);
        this.ResetObject(this.NearestObject);
        this.PreviousObject = null;
        this.NearestObject = null;
    }

    /// <summary>
    /// Resets the object.
    /// </summary>
    /// <param name="obj">The object.</param>
    public void ResetObject(GameObject obj)
    {
        if (obj != null)
        {
            if (!this.Controller.CompareShaders(obj, Shader.Find("Standard")))
            {
                this.Controller.SetShader(obj, Shader.Find("Standard"));
            }
        }
    }

    /// <summary>
    /// Checks if the nearest object changed and update.
    /// </summary>
    public void Check()
    {
        if (this.PreviousObject != null)
        {
            if (!this.Controller.CompareObjects(this.PreviousObject, this.NearestObject))
            {
                this.Controller.ResetObjects();
            }
        }

        this.Controller.Highlight();
    }

    /// <summary>
    /// Highlights the nearest object.
    /// </summary>
    public void Highlight()
    {
        if (this.NearestObject != null &&
            !this.Controller.CompareShaders(
                this.NearestObject, 
                Shader.Find("Outlined/Silhouetted Bumped Diffuse")))
        {
            this.Controller.SetShader(
                this.NearestObject, 
                Shader.Find("Outlined/Silhouetted Bumped Diffuse"));
        }
    }
}
