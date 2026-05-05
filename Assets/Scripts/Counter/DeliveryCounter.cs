using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DeliveryCounter : BaseCounter
{
    [SerializeField] private _RecipeList RecipeList;
    private List<_Recipe> QueueOrder;
    private int MaxOrder = 4;
    private float NextOrder = 2;


    private void Start() =>
        QueueOrder = new List<_Recipe>();



    private void Update()
    {
        if (QueueOrder.Count < MaxOrder && Time.time > NextOrder)
        {
            _Recipe new_order = RecipeList.List[Random.Range(0, RecipeList.List.Count)];

            NextOrder = NextOrder + Time.time;
            QueueOrder.Add(new_order);
            Debug.Log(new_order);
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

            if (first_order.IngredientList.Count != Plate_Object.ObjectList.Count)
                continue;
            bool IsCorrectOrder = false;


            foreach (ScriptableObject item in Plate_Object.ObjectList)
                if (first_order.IngredientList.Contains(item))
                    IsCorrectOrder = true; 

            if (IsCorrectOrder)
            {
                Debug.Log("Correct Order!");
                QueueOrder.RemoveAt(i); 
                return; 
            }
        }

        //If the Order Is Not Correct Then
        Debug.Log("Wrong Order!");
    }
}
