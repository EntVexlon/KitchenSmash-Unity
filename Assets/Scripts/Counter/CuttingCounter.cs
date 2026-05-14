using System;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private ProgressBarUI Progress_BarUI;
    [SerializeField] public Transform CounterTop;
    public event EventHandler OnItemCut;
    [SerializeField] private List<ValidItem> ValidItems;
    [Serializable] public struct ValidItem
    {
        public float RequiredSliceCount;
        public _IngredientItem RawItem;
        public _CutItem SlicedItem;
    }


    public override void TryDropItem(GameObject CurrentItem)
    {
        foreach (ValidItem item in ValidItems)
        {

            if (CurrentItem.TryGetComponent(out  ObjectHandler object_handler) && object_handler._Object == item.RawItem)
            {
                Progress_BarUI.SetProgressbar(true);
                CurrentCounterItem = CurrentItem;
                Progress_BarUI.FillBar(object_handler.Progress, item.RequiredSliceCount);
                CurrentCounterItem.GetComponent<ObjectHandler>().SetParent(CounterTop, CounterTop.position);
                CounterHaveItem = true;
            }
        }

    }


    public override GameObject TryPickUpItem(Player ph)
    {
        if (!CounterHaveItem) return null;
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
            if (CurrentCounterItem.TryGetComponent(out ObjectHandler object_handler) && object_handler._Object == item.RawItem)
            {
                object_handler.Progress++;
                GetComponent<SoundHandler>().PlayAudioClip(transform, SoundId.Item_Cut);
                Progress_BarUI.SetProgressbar(true);
                Progress_BarUI.FillBar(object_handler.Progress, item.RequiredSliceCount);
                OnItemCut?.Invoke(this, EventArgs.Empty);
                if (object_handler.Progress == item.RequiredSliceCount)
                {
                    Destroy(CurrentCounterItem);
                    CurrentCounterItem = Instantiate(item.SlicedItem.OutputObject);
                    CurrentCounterItem?.GetComponent<ObjectHandler>().SetParent(CounterTop, CounterTop.position);
                    CounterHaveItem = true;
                }
            }
        }
    }

    public override bool TryAddItem(GameObject Item, Object_Plate PlateObject)
    {
        bool IsIngredientAdded;
        ObjectHandler Object_Handler;
        Object_Handler = CurrentCounterItem.GetComponent<ObjectHandler>();


        IsIngredientAdded = PlateObject.AddIngredientToPlate(Object_Handler._Object as _BaseItem);

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
