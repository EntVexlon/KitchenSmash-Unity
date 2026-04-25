using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class ContainerCounter : MonoBehaviour , IinteractCounter
{
    [SerializeField] private _KitchenObjectData kitchenObjectData;
    private KitchenObject Kitchen_Object;
    private GameObject IngredientObject;
    public event Action OnTryPickUpIngredient;
    public bool CounterHaveItem { get; set; }
    public GameObject CurrentCounterItem{ get; set; }


    public void TryDropItem(GameObject Item) {}
    public void InteractAction() {}


    public GameObject TryPickUpItem(Player ph)
    {
        IngredientObject = Instantiate(kitchenObjectData.Prefab);

        Kitchen_Object = IngredientObject.GetComponent<KitchenObject>();
        Kitchen_Object.CurrentItemName = kitchenObjectData.ObjectName;
        Kitchen_Object.SetParent(ph.ItemHolder, ph.ItemHoldPoss.position);
        OnTryPickUpIngredient?.Invoke();

        return IngredientObject;
    }
            
    }



