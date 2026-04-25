using UnityEngine;
[CreateAssetMenu(menuName = "KitchenObject/Ingredient" , order = 1)]
public class _KitchenObjectData : ScriptableObject{
    public GameObject Prefab;
    public Sprite Sprite;
    public string ObjectName;
}
