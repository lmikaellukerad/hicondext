﻿/******************************************************************************\
* Copyright (C) Leap Motion, Inc. 2011-2014.                                   *
* Leap Motion proprietary. Licensed under Apache 2.0                           *
* Available at http://www.apache.org/licenses/LICENSE-2.0.html                 *
\******************************************************************************/

using UnityEngine;
using System.Collections;
using Leap;

namespace Leap.Unity
{
    // Class to setup a rigged hand based on a model.
    public class RiggedHand : HandModel
    {
        public override ModelType HandModelType
        {
            get
            {
                return ModelType.Graphics;
            }
        }
        public Vector3 modelFingerPointing = Vector3.forward;
        public Vector3 modelPalmFacing = -Vector3.up;

        public override void InitHand()
        {
            UpdateHand();
        }

        public Quaternion Reorientation()
        {
            return Quaternion.Inverse(Quaternion.LookRotation(modelFingerPointing, -modelPalmFacing));
        }

        public override void UpdateHand()
        {
            if (IsTracked)
            {
                if (palm != null)
                {
                    palm.position = GetWristPosition();
                    palm.rotation = GetPalmRotation() * Reorientation();
                }

                if (forearm != null)
                {
                    if (IsTracked)
                    {
                        Quaternion armRotation = GetArmRotation();
                        forearm.rotation = Quaternion.Euler(0f, 180f, 0f) * (new Quaternion(-armRotation.x, armRotation.y, -armRotation.z, armRotation.w) * Reorientation());
                    }
                }

                for (int i = 0; i < fingers.Length; ++i)
                {
                    if (fingers[i] != null)
                    {
                        fingers[i].fingerType = (Finger.FingerType)i;
                        fingers[i].UpdateFinger();
                    }
                }
            }
        }
    }
}