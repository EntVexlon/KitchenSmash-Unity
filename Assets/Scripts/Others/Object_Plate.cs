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
        ObjectList.Add(SO);
        return true;
    }

}
