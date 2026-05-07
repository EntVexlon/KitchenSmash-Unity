using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Recipe")]

public class _Recipe : ScriptableObject
{

    public List<_BaseItem> ItemList;
    public ItemCategory Category;
}

public enum ItemCategory
{
    Burger,
    Salad,
    CheeseBurger,
    MegaBurger,
}
