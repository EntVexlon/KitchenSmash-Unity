using UnityEngine;

public class StoveCounter : MonoBehaviour ,IinteractCounter
{
    [SerializeField] public Transform CounterTopPoint;
    [SerializeField] public Transform CounterTop;
    [SerializeField] private _CookItem[] CookItmes;

    public bool CounterHaveItem { get; set; } = false;
    public GameObject CurrentCounterItem { get; set; }
    public void TryDropItem(GameObject Item) {
        if (CounterHaveItem) return;
        foreach (_CookItem item in CookItmes)
        {
            CurrentCounterItem = Item;
            if (CurrentCounterItem?.GetComponent<KitchenObject>().CurrentItemName == item.InputObjectName)
            {
                CurrentCounterItem.GetComponent<KitchenObject>().SetParent(CounterTop, CounterTopPoint.position);
                CounterHaveItem = true;
            }
        }
    }
    public GameObject TryPickUpItem(Player ph){
        if (!CounterHaveItem) return null;
        CurrentCounterItem?.GetComponent<KitchenObject>().SetParent(ph.ItemHolder, ph.ItemHoldPoss.position);
        CounterHaveItem = false;
        GameObject @object = CurrentCounterItem;
        CurrentCounterItem = null;
        return @object;
    }
    public void InteractAction() {
    
    }
}
