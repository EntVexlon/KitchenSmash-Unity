using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] public Transform ItemHold;
    [HideInInspector] public GameObject CurrentItem;
    private bool IsHoldItem =  false;

    private void Awake() =>
        Instance = this;


    private void Update() =>
        Debug.DrawRay(transform.position, GetComponent<PlayerMoveHandler>().LastMoveDir * 2f, Color.red);
  

    public void TryInteractCounter(ICounter Counter)
    {
        if(Counter == null) return;


        if (IsHoldItem && CurrentItem != null && !(Counter is ContainerCounter))
        {
            //If Holding a Plate And Counter Have Item Then Try To Add the Item To The Holding Plate
            bool IsItemAdded;
            if (CurrentItem.TryGetComponent(out Object_Plate Object_Plate)
                && Counter.CounterHaveItem)
            {
                IsItemAdded = Counter.TryAddItem(Counter.CurrentCounterItem, Object_Plate);
                if (!IsItemAdded) return;
                IsHoldItem = true;
                Counter.CounterHaveItem = false;
                GetComponent<SoundEffectHandler>().PlayAudioClip(transform, SfxType.PickUp_Item);
                return;
            }
            else if (Counter.CounterHaveItem && Counter.CurrentCounterItem.TryGetComponent(
                out Object_Plate Plate))
            {
                //If Holding a Item And Counter Have Plate Then Try To Add the Item To The Counter Plate
                IsItemAdded = Counter.TryAddItem(CurrentItem, Plate);
                if (!IsItemAdded) return;
                IsHoldItem = false;
                CurrentItem = null;
                Counter.CounterHaveItem = true;
                GetComponent<SoundEffectHandler>().PlayAudioClip(transform, SfxType.Drop_Item);
                return;
            }
            else if (Counter.CounterHaveItem) return;


            // Else Just Try To Place
            Counter.TryDropItem(CurrentItem);
            if (!Counter.CounterHaveItem) return;
            GetComponent<SoundEffectHandler>().PlayAudioClip(transform, SfxType.Drop_Item);
            CurrentItem = null;
            IsHoldItem = false;

        }
        else if (!IsHoldItem && CurrentItem == null)
        {
            //Pickup / Drop (Object/Item)
            CurrentItem = Counter.TryPickUpItem(this);
            if (CurrentItem != null)
            {
                IsHoldItem = true;
                GetComponent<SoundEffectHandler>().PlayAudioClip(transform, SfxType.PickUp_Item);
            }
        }
    }

    public void TryInteractExecute(ICounter Counter)
    {
        if (Counter.CounterHaveItem)
        Counter.InteractAction();
        
    }
}

