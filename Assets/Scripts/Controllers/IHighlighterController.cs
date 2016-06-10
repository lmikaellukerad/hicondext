using UnityEngine;
using System.Collections;

namespace Interfaces
{
    public interface IHighlighterController
    {

        Vector3 GetPosition();

        Vector3 GetPosition(Collider col);

        Collider[] FindObjects();

        void SetShader(GameObject obj, Shader shader);

        bool CompareShaders(GameObject obj, Shader shader);

        bool CompareObjects(GameObject obj, GameObject other);



    }
}
