using System.Collections;
using UnityEngine;

/// <summary>
/// Allows us to emulate the manus with the use of the left and right mouse button.
/// </summary>
public class ManusEmulator : MonoBehaviour
{
    public GrabHandSimulator RightHand;

    /// <summary>
    /// Starts this instance.
    /// </summary>
    public void Start()
    {
    }
    
    /// <summary>
    /// Updates this instance.
    /// </summary>
    public void Update() 
    {
        if (Input.GetButton("Fire1"))
        {
            for (int i = 0; i < this.RightHand.EmulateValues.Length; i++)
            {
                this.RightHand.EmulateValues[i] = Mathf.Min(1f, this.RightHand.EmulateValues[i] + (0.3f * Time.deltaTime));
            }
        }
        else if (Input.GetButton("Fire2"))
        {
            for (int i = 0; i < this.RightHand.EmulateValues.Length; i++)
            {
                this.RightHand.EmulateValues[i] = Mathf.Max(0f, this.RightHand.EmulateValues[i] - (0.3f * Time.deltaTime));
            }
        }
    }
}
