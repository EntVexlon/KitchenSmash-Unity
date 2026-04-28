using UnityEngine;
using System;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private _Ingredient Ingredient;
    private ObjectHandler Kitchen_Object;
    private GameObject IngredientObject;
    public event Action OnTryPickUpIngredient;


    public override GameObject TryPickUpItem(Player ph)
    {
        IngredientObject = Instantiate(Ingredient.Prefab);

        Kitchen_Object = IngredientObject.GetComponent<ObjectHandler>();
        Kitchen_Object.CurrentItemName = Ingredient.ObjectName;
        Kitchen_Object.SetParent(ph.ItemHolder, ph.ItemHoldPoss.position);
        OnTryPickUpIngredient?.Invoke();

        return IngredientObject;
    }
            
    }



