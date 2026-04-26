using UnityEngine;

public class ClearCounter : MonoBehaviour , IinteractCounter
{
    [SerializeField] public Transform CounterTopPoint;
    [SerializeField] public Transform CounterTop;
    public bool CounterHaveItem { get; set; } = false;
    public GameObject CurrentCounterItem { get; set; }


    public void InteractAction() {}

    public void TryDropItem(GameObject GetItem)
    {
        if (CounterHaveItem) return;
        CurrentCounterItem = GetItem;
        CurrentCounterItem.GetComponent<KitchenObject>().SetParent(CounterTop, CounterTopPoint.position);
        CounterHaveItem = true;
    }
    public GameObject TryPickUpItem(Player ph) {
        if(!CounterHaveItem) return null;
        CurrentCounterItem?.GetComponent<KitchenObject>().SetParent(ph.ItemHolder, ph.ItemHoldPoss.position);
        CounterHaveItem = false;
        GameObject @object = CurrentCounterItem;
        CurrentCounterItem = null;
        return @object;
    }


}
