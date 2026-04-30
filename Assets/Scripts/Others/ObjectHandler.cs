using UnityEngine;

public class ObjectHandler : MonoBehaviour 
{
    public ScriptableObject _Object;
    public void SetParent(Transform parent, Vector3 position)
    {
        transform.SetParent(parent);
        transform.position = position;
    }

}
