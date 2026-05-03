using UnityEngine;
using System.Collections.Generic;

public class Object_Plate : MonoBehaviour
{
    public Transform PlateTop;
    public List<ScriptableObject> ObjectList;
    public List<ScriptableObject> ValidIngredients;
    [SerializeField] private Recipe recipe;

    public bool AddIngredientToPlate(ScriptableObject IncomingItem)
    {
        if (!ValidIngredients.Contains(IncomingItem) || ObjectList.Contains(IncomingItem))
            return false;

        // Excluding Double MeatPatty
        if (IncomingItem is _CookItem)
            foreach (var item in ObjectList)
                if (item is _CookItem) return false;
            

        ObjectList.Add(IncomingItem);
        return true;
        

    }
}
