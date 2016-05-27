using System.Collections;
using UnityEngine;

public class ToggleRenderer : MonoBehaviour
{
    /// <summary>
    /// The mesh renderer object
    /// </summary>
    public Renderer Rend;

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
    private void EnableRenderer()
    {
        this.Rend = this.GetComponent<Renderer>();
        if (this.EnableRender)
        {
            this.Rend.enabled = true;
        }
    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    public void Update()
    {
    }
}
