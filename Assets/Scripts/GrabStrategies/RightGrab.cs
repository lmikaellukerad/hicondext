using Leap.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class RightGrab : SingleGrab
{
    public RightGrab(HandModel right, GameObject obj)
    {
        this.hand = right;
        this.obj = obj;
        Init();
    }
}
