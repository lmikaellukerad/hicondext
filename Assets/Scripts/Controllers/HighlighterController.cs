﻿using UnityEngine;
using System.Collections;
using Interfaces;

public class HighlighterController {

    public float Radius = 0.01f;

    public IHighlighterController Controller;

    public GameObject nearestObject
    {
        get;
        private set;
    }
    public GameObject previousObject
    {
        get;
        private set;
    }

    public void DetectObjects()
    {
        Collider[] cols = Controller.FindObjects();
        if (cols.Length > 0)
        {
            this.FindNearestObject(cols);
        }
        else
        {
            this.ResetObjects();
        }
    }

    public Collider[] FindObjects()
    {
        return Controller.FindObjects();
    }

    public Vector3 GetPositionOfCollider(Collider col)
    {
        return col.transform.position;
    }

    public void FindNearestObject(Collider[] cols)
    {
        this.previousObject = this.nearestObject;
        float minimumDistance = float.MaxValue;
        foreach (Collider col in cols)
        {
            float currentDistance = (Controller.GetPosition() 
                - Controller.GetPosition(col)).sqrMagnitude;
            
            if (currentDistance < minimumDistance)
            {
                this.nearestObject = col.gameObject;
                minimumDistance = currentDistance;
            }
        }
    }

    public void ResetObjects()
    {
        this.previousObject = null;
        this.nearestObject = null;
        this.ResetObject(this.previousObject);
        this.ResetObject(this.nearestObject);  
    }

    public void ResetObject(GameObject obj)
    {
        if (obj != null)
        {
            if (!Controller.CompareShaders(obj, Shader.Find("Standard")))
            {
                Controller.SetShader(obj, Shader.Find("Standard"));
            }
        }
    }

    public void Check()
    {
        if (this.previousObject != null)
        {
            if (!Controller.CompareObjects(this.previousObject, this.nearestObject))
            {
                this.ResetObjects();
            }
        }

        this.Highlight();
    }



    public void Highlight()
    {
        if (this.nearestObject != null &&
            !Controller.CompareShaders(this.nearestObject, 
            Shader.Find("Outlined/Silhouetted Bumped Diffuse")))
        {
            Controller.SetShader(this.nearestObject, 
                Shader.Find("Outlined/Silhouetted Bumped Diffuse"));
        }
    }
}
