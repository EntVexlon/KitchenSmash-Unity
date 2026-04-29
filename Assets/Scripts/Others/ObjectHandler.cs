using UnityEngine;
using System.Collections.Generic;

public class ObjectHandler : MonoBehaviour 
{
    public ScriptableObject _Object;
    public void SetParent(Transform parent, Vector3 position)
    {
        transform.SetParent(parent);
        transform.position = position;
    }

    public void UnSetParent() => transform.SetParent(null);
}
