using UnityEngine;
using System.Collections.Generic;

public class Object_Plate : MonoBehaviour
{
    public Transform PlateTop;
    public List<ScriptableObject> objects;
    public List<ScriptableObject> ValidIngredients;
    private bool IsItemAdded;
    [SerializeField] private Recipe recipe;

    public bool AddIngredientToPlate(ScriptableObject SO)
    {
        if (!ValidIngredients.Contains(SO) || objects.Contains(SO))
            return false;
        objects.Add(SO);
        return true;
    }

}
