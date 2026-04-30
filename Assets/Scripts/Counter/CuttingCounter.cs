using System;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private ProgressBarUI Progress_BarUI;
    [SerializeField] public Transform CounterTop;
    [HideInInspector] public event Action OnItemCut;
    [SerializeField] private List<ValidItem> ValidItems;
    [Serializable] public struct ValidItem
    {
        public float RequiredSliceCount;
        public _IngredientItem RawItem;
        public _CutItem SlicedItem;
    }
    private int SliceCount;

    public override void TryDropItem(GameObject CurrentItem)
    {
        foreach (ValidItem item in ValidItems)
        {

            if (CurrentItem.GetComponent<ObjectHandler>()._Object == item.RawItem)
            {
                Progress_BarUI.SetProgressbar(true);
                CurrentCounterItem = CurrentItem;
                Progress_BarUI.FillBar(SliceCount, item.RequiredSliceCount);
                CurrentCounterItem.GetComponent<ObjectHandler>().SetParent(CounterTop, CounterTop.position);
                CounterHaveItem = true;
            }
        }

    }

    public override GameObject TryPickUpItem(Player ph)
    {
        if (!CounterHaveItem) return null;
        SliceCount = 0;
        Progress_BarUI.SetProgressbar(false);
        CurrentCounterItem?.GetComponent<ObjectHandler>().SetParent(ph.ItemHold, ph.ItemHold.position);
        CounterHaveItem = false;
        GameObject @object = CurrentCounterItem;
        CurrentCounterItem = null;
        return @object;
    }

    public override void InteractAction()
    {
        if (!CounterHaveItem) return;
        foreach (ValidItem item in ValidItems)
        {
            if (CurrentCounterItem.GetComponent<ObjectHandler>()._Object == item.RawItem)
            {
                SliceCount++;
                Progress_BarUI.SetProgressbar(true);
                Progress_BarUI.FillBar(SliceCount, item.RequiredSliceCount);
                OnItemCut?.Invoke();
                if (SliceCount == item.RequiredSliceCount)
                {
                    Destroy(CurrentCounterItem);
                    CurrentCounterItem = Instantiate(item.SlicedItem.OutputObject);
                    CurrentCounterItem?.GetComponent<ObjectHandler>().SetParent(CounterTop, CounterTop.position);
                    CounterHaveItem = true;
                    SliceCount = 0;
                }
            }}
    }

    public override bool TryAddItemToPlate(GameObject Item, Object_Plate PlateObject)
    {
        bool IsIngredientAdded;
        ObjectHandler Object_Handler;
        Object_Handler = CurrentCounterItem.GetComponent<ObjectHandler>();


        IsIngredientAdded = PlateObject.AddIngredientToPlate(Object_Handler._Object);

        if (!IsIngredientAdded) return false;
        GameObject Plate = CurrentCounterItem;
        CurrentCounterItem = Item;

        Object_Handler = CurrentCounterItem.GetComponent<ObjectHandler>();
        Object_Handler.SetParent(
            PlateObject.PlateTop, PlateObject.PlateTop.position);
        CurrentCounterItem = Plate;
        Progress_BarUI.SetProgressbar(false);
        return true;
    }
}
