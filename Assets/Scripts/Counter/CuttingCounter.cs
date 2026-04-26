using System;
using UnityEngine;

public class CuttingCounter : MonoBehaviour, IinteractCounter
{
    [SerializeField] private ProgressBarUI Progress_BarUI;
    [SerializeField] public Transform CounterTopPoint;
    [SerializeField] public Transform CounterTop;
    [SerializeField] private _CutItem[] IsSlicebelItem;
    [HideInInspector] public event Action OnItemCut;
    private int SliceCount;
    public bool CounterHaveItem { get; set; } = false;
    public GameObject CurrentCounterItem { get; set; }

    public void TryDropItem(GameObject GetItem)
    {
        if (CounterHaveItem) return;
        foreach (_CutItem item in IsSlicebelItem)
        {
            CurrentCounterItem = GetItem;
            if (CurrentCounterItem?.GetComponent<KitchenObject>().CurrentItemName == item.InputObjectName)
            {
                Progress_BarUI.SetProgressbar(true);
                Progress_BarUI.FillBar(SliceCount, item.RequiredSliceCount);
                CurrentCounterItem.GetComponent<KitchenObject>().SetParent(CounterTop, CounterTopPoint.position);
                CounterHaveItem = true;
            }
        }
  
    }

    public GameObject TryPickUpItem(Player ph)
    {
        if (!CounterHaveItem) return null;
        Progress_BarUI.SetProgressbar(false);
        CurrentCounterItem?.GetComponent<KitchenObject>().SetParent(ph.ItemHolder, ph.ItemHoldPoss.position);
        CounterHaveItem = false;
        GameObject @object = CurrentCounterItem;
        CurrentCounterItem = null;
        return @object;
    }

    public void InteractAction()
    {
        if (!CounterHaveItem) return;
            foreach (_CutItem item in IsSlicebelItem)
            {
            if (CurrentCounterItem?.GetComponent<KitchenObject>().CurrentItemName == item.InputObjectName)
            {
                SliceCount++;
                Progress_BarUI.SetProgressbar(true);
                Progress_BarUI.FillBar(SliceCount, item.RequiredSliceCount);
                OnItemCut?.Invoke();
                if (SliceCount == item.RequiredSliceCount)
                {
                    Destroy(CurrentCounterItem);
                    CurrentCounterItem = Instantiate(item.output);
                    CurrentCounterItem?.GetComponent<KitchenObject>().SetParent(CounterTop, CounterTopPoint.position);
                    CounterHaveItem = true;
                    SliceCount = 0;
                }
            }}
    }
}
