/******************************************************************************\
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
        private Quaternion InitOrient;
        private GameObject temp;

        void Start()
        {
            InitOrient = Quaternion.Inverse(Quaternion.LookRotation(modelFingerPointing, -modelPalmFacing));
            CreateArm();
        }

        public override void InitHand()
        {
            UpdateHand();
        }

        public Quaternion Reorientation()
        {
            return InitOrient;
        }

        public override void UpdateHand()
        {
            if (IsTracked)
            {
                if (palm != null)
                {
                    palm.position = GetWristPosition();
                    palm.rotation = GetPalmRotation() * InitOrient;
                }

                if (forearm != null)
                {
                    if (IsTracked)
                    {
                        temp.transform.rotation = GetArmRotation();
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

        /// <summary>
        /// Sets up the pole for the arm.
        /// </summary>
        void CreateArm()
        {
            if (temp == null)
            {
                temp = new GameObject("temp");
                temp.transform.position = forearm.position;
                temp.transform.rotation = forearm.rotation;
                temp.transform.parent = forearm.parent;
                forearm.parent = temp.transform;
            }
        }
    }
}