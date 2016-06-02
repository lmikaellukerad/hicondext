using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class FingerLimit : ScriptableObject
{

    public int Finger
    {
        get;
        private set;
    }

    public float MinLimit
    {
        get;
        private set;
    }

    public float MaxLimit
    {
        get;
        private set;
    }
    private float maxLimit;

    public FingerLimit(int finger, float min, float max)
    {
        this.Finger = finger;
        this.MinLimit = min;
        this.MaxLimit = max;
    }
}
