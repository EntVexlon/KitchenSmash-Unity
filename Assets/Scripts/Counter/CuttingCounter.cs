using System;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private ProgressBarUI Progress_BarUI;
    [SerializeField] public Transform CounterTop;
    [SerializeField] private _CutItem[] IsSlicebelItem;
    [HideInInspector] public event Action OnItemCut;
    private int SliceCount;

    public override void TryDropItem(GameObject Item)
    {
        if (CounterHaveItem) return;
        foreach (_CutItem item in IsSlicebelItem)
        {
            CurrentCounterItem = Item;
            if (CurrentCounterItem?.GetComponent<ObjectHandler>().CurrentItemName == item.InputObjectName)
            {
                Progress_BarUI.SetProgressbar(true);
                Progress_BarUI.FillBar(SliceCount, item.RequiredSliceCount);
                CurrentCounterItem.GetComponent<ObjectHandler>().SetParent(CounterTop, CounterTop.position);
                CounterHaveItem = true;
            }
        }
  
    }

    public override GameObject TryPickUpItem(Player ph)
    {
        if (!CounterHaveItem) return null;
        Progress_BarUI.SetProgressbar(false);
        CurrentCounterItem?.GetComponent<ObjectHandler>().SetParent(ph.ItemHolder, ph.ItemHoldPoss.position);
        CounterHaveItem = false;
        GameObject @object = CurrentCounterItem;
        CurrentCounterItem = null;
        return @object;
    }

    public override void InteractAction()
    {
        if (!CounterHaveItem) return;
            foreach (_CutItem item in IsSlicebelItem)
            {
            if (CurrentCounterItem?.GetComponent<ObjectHandler>().CurrentItemName == item.InputObjectName)
            {
                SliceCount++;
                Progress_BarUI.SetProgressbar(true);
                Progress_BarUI.FillBar(SliceCount, item.RequiredSliceCount);
                OnItemCut?.Invoke();
                if (SliceCount == item.RequiredSliceCount)
                {
                    Destroy(CurrentCounterItem);
                    CurrentCounterItem = Instantiate(item.output);
                    CurrentCounterItem?.GetComponent<ObjectHandler>().SetParent(CounterTop, CounterTop.position);
                    CounterHaveItem = true;
                    SliceCount = 0;
                }
            }}
    }
}
