using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance; 
    [SerializeField] private _RecipeList RecipeList;
    [HideInInspector] public int TotalCompletedTasks;
    public EventHandler<OrderData> OnOrder;
    public EventHandler<OrderData> OnTryConfirmOrder;
    private List<_Recipe> QueueOrder;
    private int MaxOrder = 4;
    private float NextOrderTime = 2;
    private float NextOrderCoolDown = 2;
 




    public class OrderData : EventArgs
    {
        public _Recipe current_order;
    }

    private void Awake()
    {
        QueueOrder = new List<_Recipe>();
        Instance = this;
    }



    private void Update()
    {
        if (QueueOrder.Count < MaxOrder && Time.time > NextOrderTime)
        {
            _Recipe new_order = RecipeList.List[UnityEngine.Random.Range(0, RecipeList.List.Count)];
            OnOrder?.Invoke(this, new OrderData
            {
                current_order = new_order,
            });

            NextOrderTime = NextOrderCoolDown + Time.time;
            QueueOrder.Add(new_order);
        }
    }


    public override void TryDropItem(GameObject Item)
    {
        //If the Item Is Not a Plate then Return
        if (Item.GetComponentInChildren<Object_Plate>()) 
            OrderConfirm(Item.GetComponent<Object_Plate>());
        else return;

        Destroy(Item);
        CounterHaveItem = true;
        StartCoroutine(ResetCounter());
    }

    private IEnumerator ResetCounter()
    {
        yield return null;
        CounterHaveItem = false;
    }
    private void OrderConfirm(Object_Plate Plate_Object)
    {
        for (int i = 0; i < QueueOrder.Count; i++)
        {
            _Recipe first_order = QueueOrder[i];    

            if (first_order.ItemList.Count != Plate_Object.ObjectList.Count)
                continue;
            bool IsCorrectOrder = true;


            foreach (ScriptableObject item in Plate_Object.ObjectList)
            {
                if (!first_order.ItemList.Contains(item as _BaseItem))
                {
                    IsCorrectOrder = false;
                    break;
                }
            }


            if (IsCorrectOrder)
            {
                _Recipe confirmed_order = QueueOrder[i];
                OnTryConfirmOrder?.Invoke(this, new OrderData
                {
                    current_order = confirmed_order,
                });
                QueueOrder.RemoveAt(i);
                TotalCompletedTasks++;
                Debug.Log("Correct Order!");
                GetComponent<SoundHandler>().PlayAudioClip(transform, SoundId.CorrectDelivery);
                return;
            }
        }

        //If the Order Is Not Correct Then
        GetComponent<SoundHandler>().PlayAudioClip(transform, SoundId.WrongDelivery);
        Debug.Log("Wrong Order!");
    }



    /* Audio Handling */


}

