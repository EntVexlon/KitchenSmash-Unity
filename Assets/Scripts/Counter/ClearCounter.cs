using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] public Transform CounterTop;

    //public override void TryDropItem(GameObject GetItem)
    //{
    //    if (CounterHaveItem) return;
    //    CurrentCounterItem = GetItem;
    //    CurrentCounterItem.GetComponent<ObjectHandler>().SetParent(CounterTop, CounterTopPoint.position);
    //    CounterHaveItem = true;
    //}
    //public override GameObject TryPickUpItem(Player ph)
    //{
    //    if (!CounterHaveItem) return null;
    //    CurrentCounterItem?.GetComponent<ObjectHandler>().SetParent(ph.ItemHolder, ph.ItemHoldPoss.position);
    //    CounterHaveItem = false;
    //    GameObject @object = CurrentCounterItem;
    //    CurrentCounterItem = null;
    //    return @object;
    //}



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

    //public override void TryAddIngredientToPlate(GameObject Item, Object_Plate PlateObject)
    //{

    //    CurrentCounterItem = Item;
    //    ObjectHandler Object_Handler = CurrentCounterItem.GetComponent<ObjectHandler>();
    //    Object_Handler.SetParent(
    //        PlateObject.PlateTop, PlateObject.PlateTop.position);
    //    PlateObject.AddIngredientToPlate(Object_Handler.ObjectSO);
    //}

    //public override void TryAddIngredientToPlate(GameObject Item, Object_Plate PlateObject)
    //{
    //    GameObject Plate = CurrentCounterItem;
    //    CurrentCounterItem = Item;
    //    ObjectHandler Object_Handler = CurrentCounterItem.GetComponent<ObjectHandler>();
    //    Object_Handler.SetParent(
    //        PlateObject.PlateTop, PlateObject.PlateTop.position);
    //    PlateObject.AddIngredientToPlate(Object_Handler.ObjectSO);
    //    CurrentCounterItem = Plate;
    //}

    public override bool TryAddIngredientToPlate(GameObject Item, Object_Plate PlateObject)
    {
        bool IsIngredientAdded;
        ObjectHandler Object_Handler;

        //if Counter Have Plate
        if (CurrentCounterItem.TryGetComponent(out Object_Plate obj_pl))
            Object_Handler = Item.GetComponent<ObjectHandler>();
        else Object_Handler = CurrentCounterItem.GetComponent<ObjectHandler>();


        IsIngredientAdded = PlateObject.AddIngredientToPlate(Object_Handler.ObjectSO);

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
