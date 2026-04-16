using UnityEngine;

public class KitchenObject : MonoBehaviour 
{
    public string CurrentItemName;
    public void SetParent(Transform parent, Vector3 position)
    {
        transform.SetParent(parent);
        transform.position = position;
    }

    public void UnSetParent() => transform.SetParent(null);
}
