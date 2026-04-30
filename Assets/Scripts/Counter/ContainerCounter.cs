using UnityEngine;
using System;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private _IngredientItem Ingredient;
    private ObjectHandler Kitchen_Object;
    private GameObject IngredientObject;
    public event Action OnTryPickUpIngredient;


    public override GameObject TryPickUpItem(Player ph)
    {
        IngredientObject = Instantiate(Ingredient.Prefab);

        Kitchen_Object = IngredientObject.GetComponent<ObjectHandler>();
        Kitchen_Object.SetParent(ph.ItemHold, ph.ItemHold.position);
        OnTryPickUpIngredient?.Invoke();

        return IngredientObject;
    }
            
    }



