using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] public Transform CounterTop;
    [SerializeField] private ProgressBarUI Progress_BarUI;
    [SerializeField] private ParticleEffectHandler EffectHandler;
    [SerializeField] private List<ValidItem> ValidItems;
    [Serializable] public struct ValidItem
    {
        public _IngredientItem RawItem;
        public _CookItem  CookedItem;
        public _CookItem  BurnerdItem;
        public float CookTime;
        public float BurnTime;
    }
    private float CookTime = 0;
    private bool IsCooking = false;



    public override void TryDropItem(GameObject CurrentItem)
    {
        if (CounterHaveItem) return;

        foreach (ValidItem item in ValidItems)
        {
            if(CurrentItem.GetComponent<ObjectHandler>()._Object == item.RawItem)
            {
                CurrentCounterItem = CurrentItem;
                Progress_BarUI.SetProgressbar(true);
                Progress_BarUI.FillBar(CookTime, CookTime > item.CookTime ? item.CookTime : item.BurnTime);
                CurrentCounterItem.GetComponent<ObjectHandler>().SetParent(
                    CounterTop, CounterTop.position);
                CounterHaveItem = true;
            }
        }
    }

    public override GameObject TryPickUpItem(Player ph){
        if (!CounterHaveItem) return null;
        CookTime = 0;
        Progress_BarUI.SetProgressbar(false);
        EffectHandler.SetVisual(false);
        StopAllCoroutines();
        CurrentCounterItem?.GetComponent<ObjectHandler>().SetParent(ph.ItemHold, ph.ItemHold.position);
        CounterHaveItem = false;
        IsCooking = false;
        GameObject @object = CurrentCounterItem;
        CurrentCounterItem = null;
        return @object;
    }

    // I know so much bools but 
    // maybe in future i will refactor it maybe :) 

    public override void InteractAction()
    {
        if (!CounterHaveItem || IsCooking) return;
        foreach (var item in ValidItems)
        {
            if (CurrentCounterItem.GetComponent<ObjectHandler>()._Object == item.RawItem)
                StartCoroutine(AssignCookMethod(item.CookedItem));
        }
    }

    private IEnumerator AssignCookMethod(_CookItem item)
    {
        if (!CounterHaveItem) yield break;
        foreach (var items in ValidItems)
        {
            yield return Cook(items.CookTime, items.CookedItem.OutputObject);
            yield return Cook(items.BurnTime, items.BurnerdItem.OutputObject);
        }

    }
    private IEnumerator Cook(float time, GameObject item)
    {
        IsCooking = true;
        while (CookTime < time)
        {
            CookTime += Time.deltaTime;
            EffectHandler.SetVisual(true);
            Progress_BarUI.FillBar(CookTime, time);
            yield return null;
        }
        Destroy(CurrentCounterItem);
        CurrentCounterItem = Instantiate(item);
        CurrentCounterItem.GetComponent<ObjectHandler>().SetParent(CounterTop,
        CounterTop.position);
        CounterHaveItem = true;
        IsCooking = false;
        CookTime = 0;

    }

    public override bool TryAddItemToPlate(GameObject Item, Object_Plate PlateObject)
    {
        bool IsIngredientAdded;
        ObjectHandler Object_Handler;
        Object_Handler = CurrentCounterItem.GetComponent<ObjectHandler>();


        IsIngredientAdded = PlateObject.AddIngredientToPlate(Object_Handler._Object);

        if (!IsIngredientAdded) return false;


        StopAllCoroutines();
        EffectHandler.SetVisual(false);
        IsCooking = false;
        CookTime = 0;

        GameObject Plate = CurrentCounterItem;
        CurrentCounterItem = Item;

        Object_Handler = CurrentCounterItem.GetComponent<ObjectHandler>();
        Object_Handler.SetParent(
            PlateObject.PlateTop, PlateObject.PlateTop.position);
        CurrentCounterItem = Plate;
        Progress_BarUI.SetProgressbar(false);
        return true;
    }



}
