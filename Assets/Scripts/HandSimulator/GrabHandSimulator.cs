using System.Collections;
using UnityEngine;

public class GrabHandSimulator: HandSimulator
{
    private float[] min = new float[5];
    private float[] max = new float[5];

    public override void Start()
    {
        base.Start();
        for (int i = 0; i < min.Length; i ++)
        {
            min[i] = 0;
            max[i] = 1;
        }
    }

    /// <summary>
    /// Updates a skeletal from glove data
    /// </summary>
    public override void Update()
    {
        Quaternion q = glove.Quaternion;
        float[] fingers = glove.Fingers;
        if (UseHandRotation)
        {
            RootTransform.localRotation = q;
        }

        for (int i = 0; i < 5; i++)
        {
            if (fingers[i] > min[i] && fingers[i] < max[i])
            {
                animationClip.SampleAnimation(modelObject, fingers[i] * timeFactor);
                for (int j = 0; j < 4; j++)
                {
                    gameTransforms[i][j].localRotation = modelTransforms[i][j].localRotation;
                }
            }
        }
    }

    public int GetFingerID(Transform fingerTip)
    {
        for (int i = 0; i < FingerTipTransforms.Length; i++)
        {
            if (FingerTipTransforms[i] == fingerTip)
            {
                return i;
            }
        }
        return -1;
    }

    public void ResetFingerLimit(int finger)
    {
        min[finger] = 0;
        max[finger] = 1;
    }

    public void ResetFingerLimit(Transform finger)
    {
        int i = GetFingerID(finger);
        min[i] = 0;
        max[i] = 1;
    }

    public float GetFingerState(int finger)
    {
        return glove.Fingers[finger];
    }

    public float GetFingerState(Transform finger)
    {
        int i = GetFingerID(finger);
        return glove.Fingers[i];
    }

    public void ClampMin(int finger)
    {
        min[finger] = GetFingerState(finger);
    }

    public void ClampMin(Transform finger)
    {
        int i = GetFingerID(finger);
        min[i] = GetFingerState(i);
    }

    public void ClampMax(int finger)
    {
        max[finger] = GetFingerState(finger);
    }

    public void ClampMax(Transform finger)
    {
        int i = GetFingerID(finger);
        max[i] = GetFingerState(i);
    }
}
