using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] public Transform CounterTopPoint;
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
        if (CounterHaveItem && CurrentCounterItem.TryGetComponent(out Object_Plate ObjectPlate))
        {
            //ObjectPlate.AddIngredientToPlate();
        }
        else if (CounterHaveItem) return;
        CurrentCounterItem = GetItem;
        CurrentCounterItem.GetComponent<ObjectHandler>().SetParent(CounterTop, CounterTopPoint.position);
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

}
