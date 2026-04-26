using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public Transform ItemHoldPoss;
    [SerializeField] public Transform ItemHolder;
    [HideInInspector] public GameObject CurrentItem;
    private bool IsHoldItem =  false;
    public void TryInteractCounter(IinteractCounter Counter)
    {
        if (IsHoldItem && CurrentItem != null)
        {
            //Place
            if (Counter is ContainerCounter || Counter.CounterHaveItem) return;
            Counter.TryDropItem(CurrentItem);
            if(!Counter.CounterHaveItem) return;

            /// This behavior is funny. 
            /// I originally wanted to allow placing only sliceable items 
            /// like vegetables and restrict placing items like burgers, meat, etc. 
            /// But now an extra condition has appeared sliced items also cannot be placed, 
            /// which is logically correct, but not what I intended.

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

    public void TryInteractExecute(IinteractCounter Counter)
    {
        if (Counter.CounterHaveItem)
        Counter.InteractAction();
        
    }
}

