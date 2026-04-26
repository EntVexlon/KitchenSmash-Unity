using UnityEngine;
using System;

public class ContainerCounter : MonoBehaviour , IinteractCounter
{
    [SerializeField] private _Ingredient Ingredient;
    private KitchenObject Kitchen_Object;
    private GameObject IngredientObject;
    public event Action OnTryPickUpIngredient;
    public bool CounterHaveItem { get; set; }
    public GameObject CurrentCounterItem{ get; set; }


    public void TryDropItem(GameObject Item) {}
    public void InteractAction() {}


    public GameObject TryPickUpItem(Player ph)
    {
        IngredientObject = Instantiate(Ingredient.Prefab);

        Kitchen_Object = IngredientObject.GetComponent<KitchenObject>();
        Kitchen_Object.CurrentItemName = Ingredient.ObjectName;
        Kitchen_Object.SetParent(ph.ItemHolder, ph.ItemHoldPoss.position);
        OnTryPickUpIngredient?.Invoke();

        return IngredientObject;
    }
            
    }



