using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public Transform ItemHold;
    [HideInInspector] public GameObject CurrentItem;
    private bool IsHoldItem =  false;

    public void TryInteractCounter(ICounter Counter)
    {
        if(Counter == null) return;
        if (IsHoldItem && CurrentItem != null && !(Counter is ContainerCounter))
        {
            //If Holding a Plate And Counter Have Item Then Try To Add the Item To The Holding Plate
            bool IsItemAdded;
            if(CurrentItem.TryGetComponent(out Object_Plate Object_Plate)
                && Counter.CounterHaveItem){
                IsItemAdded = Counter.TryAddItem(Counter.CurrentCounterItem,Object_Plate);
                if (!IsItemAdded) return;
                IsHoldItem = true;
                Counter.CounterHaveItem = false;
                return;
            }
            else if (Counter.CounterHaveItem && Counter.CurrentCounterItem.TryGetComponent(
                out Object_Plate Plate)){
                //If Holding a Item And Counter Have Plate Then Try To Add the Item To The Counter Plate
                IsItemAdded = Counter.TryAddItem(CurrentItem, Plate);
                if (!IsItemAdded) return;

                IsHoldItem = false;
                CurrentItem = null;
                Counter.CounterHaveItem = true;
                return;
            }
            else if (Counter.CounterHaveItem) return;
            // Else Just Try To Place
            Counter.TryDropItem(CurrentItem);
            if (!Counter.CounterHaveItem) return;

            CurrentItem = null;
            IsHoldItem = false;

        }
        else if (!IsHoldItem && CurrentItem == null)
        {
            //Pickup / Drop (Object/Item)
            CurrentItem = Counter.TryPickUpItem(this);
            if (CurrentItem != null) IsHoldItem = true;
        }
    }

    public void TryInteractExecute(ICounter Counter)
    {
        if (Counter.CounterHaveItem)
        Counter.InteractAction();
        
    }
}

