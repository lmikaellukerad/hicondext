using System.Collections;
using UnityEngine;

public class ToggleRenderer : MonoBehaviour
{
    /// <summary>
    /// The mesh renderer object
    /// </summary>
    public MeshRenderer Rend;

    /// <summary>
    /// A boolean to turn this scripts functionality on or off.
    /// </summary>
    public bool EnableRender;

    /// <summary>
    /// Starts this instance.
    /// </summary>
    public void Start()
    {
        this.EnableRenderer();
    }

    /// <summary>
    /// Enables the renderer.
    /// </summary>
    public void EnableRenderer()
    {
        this.Rend = this.GetComponent<MeshRenderer>();
        if (this.EnableRender)
        {
            this.Rend.enabled = true;
        }
    }
}
