using System.Collections;
using UnityEngine;

public class GrabHandSimulator : HandSimulator
{
    private float[] min = new float[5];
    private float[] max = new float[5];

    /// <summary>
    /// Constructor which loads the HandModel
    /// And sets default values for the this.Min/this.Max limits
    /// </summary>
    public override void Start()
    {
        base.Start();
        for (int i = 0; i < this.min.Length; i++)
        {
            this.min[i] = 0;
            this.max[i] = 1;
        }
    }

    /// <summary>
    /// Updates a skeletal from glove data
    /// </summary>
    public override void Update()
    {
        Quaternion q = this.glove.Quaternion;
        float[] fingers = this.glove.Fingers;
        if (this.UseHandRotation)
        {
            this.RootTransform.localRotation = q;
        }

        for (int i = 0; i < 5; i++)
        {
            if (fingers[i] > this.min[i] && fingers[i] < this.max[i])
            {
                animationClip.SampleAnimation(this.modelObject, fingers[i] * GrabHandSimulator.timeFactor);
                for (int j = 0; j < 4; j++)
                {
                    this.gameTransforms[i][j].localRotation = this.modelTransforms[i][j].localRotation;
                }
            }
        }
    }

    /// <summary>
    /// Gets the finger identifier by matching the fingertip transform.
    /// </summary>
    /// <param name="fingerTip">The finger tip to match with.</param>
    /// <returns> Finger id, if not found returns -1</returns>
    public int GetFingerID(Transform fingerTip)
    {
        for (int i = 0; i < this.FingerTipTransforms.Length; i++)
        {
            if (this.FingerTipTransforms[i] == fingerTip)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// Resets the finger limit of finger with a certain id.
    /// </summary>
    /// <param name="finger">The finger identifier.</param>
    public void ResetFingerLimit(int finger)
    {
        this.min[finger] = 0;
        this.max[finger] = 1;
    }

    /// <summary>
    /// Resets the finger limit of a specific finger by matching a transform.
    /// </summary>
    /// <param name="finger">The finger Transform.</param>
    public void ResetFingerLimit(Transform finger)
    {
        int i = this.GetFingerID(finger);
        this.min[i] = 0;
        this.max[i] = 1;
    }

    /// <summary>
    /// Gets the current state of the finger.
    /// </summary>
    /// <param name="finger">The finger identifier.</param>
    /// <returns>Returns current bending state of a finger ranging from 0 to 1</returns>
    public float GetFingerState(int finger)
    {
        return glove.Fingers[finger];
    }

    /// <summary>
    /// Gets the current bending state of the finger.
    /// </summary>
    /// <param name="finger">The finger transform.</param>
    /// <returns>Returns current bending state of a finger ranging from 0 to 1</returns>
    public float GetFingerState(Transform finger)
    {
        int i = this.GetFingerID(finger);
        return glove.Fingers[i];
    }

    /// <summary>
    /// Sets the this.Minimum finger bend value to its current state.
    /// </summary>
    /// <param name="finger">The finger identifier.</param>
    public void ClampMin(int finger)
    {
        this.min[finger] = this.GetFingerState(finger);
    }

    /// <summary>
    /// Sets the this.Minimum finger bend value to its current state.
    /// </summary>
    /// <param name="finger">The finger transform.</param>
    public void ClampMin(Transform finger)
    {
        int i = this.GetFingerID(finger);
        this.min[i] = this.GetFingerState(i);
    }

    /// <summary>
    /// Sets the this.Maximum finger bend value to its current state.
    /// </summary>
    /// <param name="finger">The finger identifier.</param>
    public void ClampMax(int finger)
    {
        this.max[finger] = this.GetFingerState(finger);
    }

    /// <summary>
    /// Sets the this.Maximum finger bend value to its current state.
    /// </summary>
    /// <param name="finger">The finger transform.</param>
    public void ClampMax(Transform finger)
    {
        int i = this.GetFingerID(finger);
        this.max[i] = this.GetFingerState(i);
    }
}
