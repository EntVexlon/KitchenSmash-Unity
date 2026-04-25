using UnityEngine;

[CreateAssetMenuAttribute(menuName = "KitchenObject/CookItem", order = 3)]

public class _CookItem : ScriptableObject
{
        public string InputObjectName;
        public float RequiredCookTime;
        public GameObject CookedObject;
        public GameObject BurrnedObject;
    
}
