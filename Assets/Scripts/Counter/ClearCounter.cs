using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] public Transform CounterTop;

    public override void TryDropItem(GameObject GetItem)
    {
        if (CounterHaveItem) return;
        CurrentCounterItem = GetItem;
        CurrentCounterItem.GetComponent<ObjectHandler>().SetParent(CounterTop, CounterTop.position);
        CounterHaveItem = true;
    }
    public override GameObject TryPickUpItem(Player ph)
    {
        if (!CounterHaveItem) return null;
        CurrentCounterItem?.GetComponent<ObjectHandler>().SetParent(ph.ItemHolder, ph.ItemHoldPoss.position);
        CounterHaveItem = false;
        GameObject @object = CurrentCounterItem;
        CurrentCounterItem = null;
        return @object;
    }

    public override bool TryAddIngredientToPlate(GameObject Item, Object_Plate PlateObject)
    {
        bool IsIngredientAdded;
        ObjectHandler Object_Handler;

        //if Counter Have Plate
        if (CurrentCounterItem.TryGetComponent(out Object_Plate obj_pl))
            Object_Handler = Item.GetComponent<ObjectHandler>();
        else Object_Handler = CurrentCounterItem.GetComponent<ObjectHandler>();


        //IsIngredientAdded = PlateObject.AddIngredientToPlate(Object_Handler.ObjectSO);
        IsIngredientAdded = PlateObject.AddIngredientToPlate(Object_Handler._Object);

        if (!IsIngredientAdded) return false;
        GameObject Plate = CurrentCounterItem;
        CurrentCounterItem = Item;

        Object_Handler = CurrentCounterItem.GetComponent<ObjectHandler>();
        Object_Handler.SetParent(
            PlateObject.PlateTop, PlateObject.PlateTop.position);
        CurrentCounterItem = Plate;
        return true;
    }

}
