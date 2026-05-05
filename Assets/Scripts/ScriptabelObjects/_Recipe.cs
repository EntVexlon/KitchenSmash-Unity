using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Recipe")]

public class _Recipe : ScriptableObject
{

    public List<ScriptableObject> IngredientList;
}
