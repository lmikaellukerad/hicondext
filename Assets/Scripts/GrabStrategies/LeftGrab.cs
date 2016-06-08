using Leap.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class LeftGrab : SingleGrab
{
    public LeftGrab(HandModel left, GameObject obj)
    {
        this.hand = left;
        this.obj = obj;
        Init();
    }
}
