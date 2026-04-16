using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenuAttribute(fileName = "KitchenObject", menuName = "KitchenObject/Vegetable")]
public class _KitchenObjectData : ScriptableObject{
    public GameObject Prefab;
    public Sprite Sprite;
    public string ObjectName;
}
