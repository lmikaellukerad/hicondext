﻿/*
   Copyright 2015 Manus VR

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

     http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 */

using UnityEngine;
using ManusMachina;

public class HandSimulator : MonoBehaviour
{
    protected const float timeFactor = 10.0f;

    public bool UseHandRotation;
    public GLOVE_HAND hand;
    public Transform RootTransform;

    protected Glove glove;
    public GameObject modelObject;
    public AnimationClip animationClip;
    protected Transform[][] gameTransforms;
    protected Transform[][] modelTransforms;
    public Transform[] FingerTipTransforms;

    /// <summary>
    /// Finds a deep child in a transform
    /// </summary>
    /// <param name="aParent">Transform to be searched</param>
    /// <param name="aName">Name of the (grand)child to be found</param>
    /// <returns></returns>
    private static Transform FindDeepChild(Transform aParent, string aName)
    {
        var result = aParent.Find(aName);
        if (result != null)
            return result;
        foreach (Transform child in aParent)
        {
            result = FindDeepChild(child, aName);
            if (result != null)
                return result;
        }
        return null;
    }

    /// <summary>
    /// Constructor which loads the HandModel
    /// </summary>
    public virtual void Start()
    {
        // Ensure the library initialized correctly.
        Manus.ManusInit();

        // Initialize the glove and the associated skeletal model.
        glove = new Glove(hand);

        // Re-center the glove for the start position.
        // FIXME: This works well for monitor demos, but for VR you should use a different
        // centering mechanism.
        glove.Recenter();

        Transform modelRoot = FindDeepChild(modelObject.transform, RootTransform.name);
        // Associate the game transforms with the skeletal model.
        gameTransforms = new Transform[5][];
        modelTransforms = new Transform[5][];
        FingerTipTransforms = new Transform[5];
        for (int i = 0; i < 5; i++)
        {
            gameTransforms[i] = new Transform[4];
            modelTransforms[i] = new Transform[4];
            for (int j = 0; j < 4; j++)
            {
                if (GLOVE_HAND.GLOVE_LEFT == glove.GloveHand)
                {
                    gameTransforms[i][j] = FindDeepChild(RootTransform, "L_Finger_" + i.ToString() + j.ToString());
                    modelTransforms[i][j] = FindDeepChild(modelRoot, "L_Finger_" + i.ToString() + j.ToString());
                    if (gameTransforms[i][j] == null)
                    {
                        gameTransforms[i][j] = FindDeepChild(RootTransform, "L_Finger_" + i.ToString() + (j - 1).ToString() + "_end");
                        modelTransforms[i][j] = FindDeepChild(modelRoot, "L_Finger_" + i.ToString() + (j - 1).ToString() + "_end");
                        FingerTipTransforms[i] = gameTransforms[i][j];
                    }
                }
                else
                {
                    gameTransforms[i][j] = FindDeepChild(RootTransform, "R_Finger_" + i.ToString() + j.ToString());
                    modelTransforms[i][j] = FindDeepChild(modelRoot, "R_Finger_" + i.ToString() + j.ToString());
                    if (gameTransforms[i][j] == null)
                    {
                        gameTransforms[i][j] = FindDeepChild(RootTransform, "R_Finger_" + i.ToString() + (j - 1).ToString() + "_end");
                        modelTransforms[i][j] = FindDeepChild(modelRoot, "R_Finger_" + i.ToString() + (j - 1).ToString() + "_end");
                        FingerTipTransforms[i] = gameTransforms[i][j];
                    }
                }
            }
        }

        modelObject.SetActive(true);
    }

    /// <summary>
    /// Updates a skeletal from glove data
    /// </summary>
    public virtual void Update()
    {
        Quaternion q = glove.Quaternion;
        float[] fingers = glove.Fingers;
        if (UseHandRotation)
        {
            RootTransform.localRotation = q;
        }

        for (int i = 0; i < 5; i++)
        {
            animationClip.SampleAnimation(modelObject, fingers[i] * timeFactor);
            for (int j = 0; j < 4; j++)
            {
                gameTransforms[i][j].localRotation = modelTransforms[i][j].localRotation;
            }
        }
    }
}
