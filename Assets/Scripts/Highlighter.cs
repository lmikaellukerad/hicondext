using System.Collections;
using Leap;
using Leap.Unity;
using UnityEngine;
using Interfaces;

/// <summary>
/// Author: Luke
/// This class adds a object highlight functionality. 
/// It looks for nearby objects within a radius, with the center being the transform position.
/// For the most optimal behavior in conjunction with FixedJointGrab, 
/// this script should be attached to the thumb transform of the hand model with the radius set to 0.01.
/// </summary>
public class Highlighter : MonoBehaviour, IHighlighterController
{
    public float Radius = 0.01f;

    private int interactable = 8;
    private HighlighterController controller = new HighlighterController();

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }

    public Vector3 GetPosition(Collider col)
    {
        return col.transform.position;
    }

    public bool CompareShaders(GameObject obj, Shader shader)
    {
        return obj.GetComponent<Renderer>().material.shader.Equals(shader);
    }

    public void SetShader(GameObject obj, Shader shader)
    {
        obj.GetComponent<Renderer>().material.shader =
                shader;
    }

    public bool CompareObjects(GameObject obj, GameObject other)
    {
        return obj.Equals(other);
    }

    public Collider[] FindObjects()
    {
        return Physics.OverlapSphere(this.transform.position, this.Radius, 1 << this.interactable); 
    }

    private void OnEnable()
    {
        controller.Controller = this;
    }

    /// <summary>
    /// Updates this instance every frame.
    /// </summary>
    private void Update()
    {
        controller.DetectObjects();
    }
}
