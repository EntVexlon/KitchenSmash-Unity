using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public Transform ItemHoldPoss;
    [SerializeField] public Transform ItemHolder;
    [HideInInspector] public GameObject CurrentItem;
    private bool IsHoldItem =  false;
    public void TryInteractCounter(ICounter Counter)
    {
        if (IsHoldItem && CurrentItem != null)
        {
            //Place
            if (Counter is ContainerCounter || Counter.CounterHaveItem) return;
            Counter.TryDropItem(CurrentItem);
            if(!Counter.CounterHaveItem) return;

            CurrentItem = null;
            IsHoldItem = false;
        }
        else if(!IsHoldItem && CurrentItem == null)
        {
            //Hold
            CurrentItem =  Counter.TryPickUpItem(this);
            if(CurrentItem != null) IsHoldItem = true;
        }
    }

    public void TryInteractExecute(ICounter Counter)
    {
        if (Counter.CounterHaveItem)
        Counter.InteractAction();
        
    }
}

