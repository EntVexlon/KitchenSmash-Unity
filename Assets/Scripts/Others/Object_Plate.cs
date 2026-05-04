using UnityEngine;
using System.Collections.Generic;

public class Object_Plate : MonoBehaviour
{
    public Transform PlateTop;
    [SerializeField] private ItemIconUI IconUI;
    //[SerializeField] private Recipe recipe;
    public List<ScriptableObject> ObjectList;
    public List<ScriptableObject> ValidIngredients;

    public bool AddIngredientToPlate(_BaseItem IncomingItem)
    {
        if (!ValidIngredients.Contains(IncomingItem) || ObjectList.Contains(IncomingItem))
            return false;

        // Excluding Double MeatPatty
        if (IncomingItem is _CookItem)
            foreach (var item in ObjectList)
                if (item is _CookItem) return false;
        IconUI.SetItemIcon(IncomingItem);
        ObjectList.Add(IncomingItem);
        return true;
        

    }
}
