using UnityEngine;
using System.Collections.Generic;

public class Object_Plate : MonoBehaviour
{
    public Transform PlateTop;
    public List<ScriptableObject> ObjectList;
    public List<ScriptableObject> ValidIngredients;
    [SerializeField] private Recipe recipe;

    public bool AddIngredientToPlate(ScriptableObject SO)
    {
        if (!ValidIngredients.Contains(SO) || ObjectList.Contains(SO))
            return false;

        // To Check The Incoming Item is a Same Category Item if true then return
        _BaseItem IncomingItem = SO as _BaseItem;
        if (IncomingItem != null)
        {
            foreach (ScriptableObject ExistingItem in ObjectList)
            {
                _BaseItem Existing_Item = ExistingItem as _BaseItem;
                if (Existing_Item != null && Existing_Item.Item_Category == IncomingItem.Item_Category)
                return false;
            }
        }

        ObjectList.Add(SO);

        return true;
    }

}
