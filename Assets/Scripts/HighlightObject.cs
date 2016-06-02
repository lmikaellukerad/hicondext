using Leap;
using Leap.Unity;
using System.Collections;
using UnityEngine;

public class HighlightObject : MonoBehaviour
{
    private int interactable = 8;
    private GameObject previousObject;
    private GameObject nearestObject;
    private Shader old;

    public float Radius;

    public void DetectObject()
    {
        Vector3 pos = transform.position;
        Collider[] objects = Physics.OverlapSphere(pos, this.Radius, 1 << this.interactable);
        if (objects.Length > 0)
        {
            findNearest(objects, pos);
        }
        else
        {
            clear();
        }
            
    }

    private void clear()
    {
        if (this.previousObject != null)
        {
            this.previousObject.GetComponent<Renderer>().material.shader = old;
        }
        if (this.nearestObject != null)
        {
            this.nearestObject.GetComponent<Renderer>().material.shader = old;
            this.nearestObject = null;
        }
    }

    private void findNearest(Collider[] objects, Vector3 pos)
    {
        previousObject = nearestObject;
        float minimumDistance = float.MaxValue;
        foreach (Collider o in objects)
        {
            float currentDistance = Vector3.Distance(pos, o.GetComponent<Transform>().position);
            if (currentDistance < minimumDistance)
            {
                this.nearestObject = o.gameObject;
                minimumDistance = currentDistance;
            }
        }
        check();
    }

    private void check()
    {
        if (previousObject != null) { 
            if (!previousObject.Equals(nearestObject))
            {
                this.clear();
                this.highlight();
            }
        }
        else
        {
            this.highlight();
        }
    }

    private void highlight()
    {
        if (this.nearestObject != null)
        {
            this.old = this.nearestObject.GetComponent<Renderer>().material.shader;
            this.nearestObject.GetComponent<Renderer>().material.shader = 
                Shader.Find("Outlined/Silhouetted Bumped Diffuse");
        }
    }

    private void Update()
    {
        DetectObject();
    }
}
