using System;
using System.Collections;
using Interfaces;
using Leap;
using Leap.Unity;
using UnityEngine;

/// <summary>
/// Author: Luke
/// This class adds a object highlight functionality. 
/// It looks for nearby objects within a radius, with the center being the transform position.
/// For the most optimal behavior in conjunction with FixedJointGrab, 
/// this script should be attached to the thumb transform of the hand model with the radius set to 0.01.
/// This class is using the Humble Object pattern to improve testing of code. 
/// All logic is located in the HighlighterController
/// </summary>
public class Highlighter : MonoBehaviour, IHighlighterController, IOverlapSphere
{
    public float Radius = 0.01f;

    private int interactable = 8;
    private HighlighterController controller = new HighlighterController();

    /// <summary>
    /// Return the position of this transform.
    /// </summary>
    /// <returns>Position in Vector3 of this transform.</returns>
    public Vector3 GetPosition()
    {
        return this.transform.position;
    }

    /// <summary>
    /// Return the position of the given collider.
    /// </summary>
    /// <param name="collider">The collider.</param>
    /// <returns>Position in Vector3 of given collider.</returns>
    public Vector3 GetPosition(Collider collider)
    {
        return collider.transform.position;
    }

    /// <summary>
    /// Finds the nearest object.
    /// </summary>
    /// <param name="cols">The cols.</param>
    public void FindNearestObject(Collider[] cols)
    {
        this.controller.UpdateNearestObject(cols);
    }

    /// <summary>
    /// Resets the objects.
    /// </summary>
    public void ResetObjects()
    {
        this.controller.ResetObjects();
    }

    /// <summary>
    /// Compares the shaders.
    /// </summary>
    /// <param name="obj">The object with the actual.</param>
    /// <param name="shader">The expected shader.</param>
    /// <returns>True if shaders are equal.</returns>
    public bool CompareShaders(GameObject obj, Shader shader)
    {
        return obj.GetComponent<Renderer>().material.shader.Equals(shader);
    }

    /// <summary>
    /// Sets the shader of a given object.
    /// </summary>
    /// <param name="obj">The object to set the shader to.</param>
    /// <param name="shader">The shader to set.</param>
    public void SetShader(GameObject obj, Shader shader)
    {
        obj.GetComponent<Renderer>().material.shader =
                shader;
    }

    /// <summary>
    /// Compares the objects.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <param name="other">The other.</param>
    /// <returns>True if objects are equal.</returns>
    public bool CompareObjects(GameObject obj, GameObject other)
    {
        return obj.Equals(other);
    }

    /// <summary>
    /// Finds the objects.
    /// </summary>
    /// <returns>Array of found colliders.</returns>
    public Collider[] FindObjects()
    {
        return Physics.OverlapSphere(this.transform.position, this.Radius, 1 << this.interactable); 
    }

    /// <summary>
    /// Highlights the nearest object.
    /// </summary>
    public void Highlight()
    {
        this.controller.Highlight();
    }

    /// <summary>
    /// Checks if the nearest object is changed.
    /// </summary>
    public void Check()
    {
        this.controller.Check();
    }

    /// <summary>
    /// Called when [enable].
    /// </summary>
    private void OnEnable()
    {
        this.controller.Controller = this;
        this.controller.OverlapSphere = this;
    }

    /// <summary>
    /// Updates this instance every frame.
    /// </summary>
    private void Update()
    {
        this.controller.DetectObjects();
    }
}
