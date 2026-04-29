using System.Collections;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] public Transform CounterTop;
    [SerializeField] private ProgressBarUI Progress_BarUI;
    [SerializeField] private ParticleEffectHandler EffectHandler;
    [SerializeField] private _CookItem[] CookItmes;
    private float CookTime = 0;
    private bool IsCooking = false;
    public override void TryDropItem(GameObject Item) {
        if (CounterHaveItem) return;
        foreach (_CookItem item in CookItmes)
        {
            CurrentCounterItem = Item;
            if (CurrentCounterItem?.GetComponent<ObjectHandler>().CurrentItemName == item.InputObjectName)
            {
                Progress_BarUI.SetProgressbar(true);
                Progress_BarUI.FillBar(CookTime, CookTime > item.RequiredCookTime ? item.RequiredCookTime : item.BurnTime);
                CurrentCounterItem.GetComponent<ObjectHandler>().SetParent(
                    CounterTop, CounterTop.position);
                CounterHaveItem = true;
            }
        }
    }
    public override GameObject TryPickUpItem(Player ph){
        if (!CounterHaveItem) return null;
        Progress_BarUI.SetProgressbar(false);
        EffectHandler.SetVisual(false);
        StopAllCoroutines();
        CurrentCounterItem?.GetComponent<ObjectHandler>().SetParent(ph.ItemHolder, ph.ItemHoldPoss.position);
        CounterHaveItem = false;
        IsCooking = false;
        GameObject @object = CurrentCounterItem;
        CurrentCounterItem = null;
        return @object;
    }

    // I know so much bools but 
    // maybe in future i will refactor it maybe :) 

    public override void InteractAction() {
    if(!CounterHaveItem || IsCooking) return;
        foreach (_CookItem item in CookItmes)
        {
            if (item.InputObjectName == CurrentCounterItem?.GetComponent<ObjectHandler>().CurrentItemName)
                StartCoroutine(AssignCookMethod(item));
        }
    }


    //private IEnumerator AssignCookMethod(_CookItem item)
    //{
    //    if (!CounterHaveItem) yield break;
    //    yield return Cook(item.RequiredCookTime, item.CookedObject);
    //    yield return Cook(item.BurnTime, item.BurrnedObject);

    //}


    private IEnumerator AssignCookMethod(_CookItem item)
    {
        if (!CounterHaveItem) yield break;
        yield return Cook(item.RequiredCookTime, item.OutputObject);
        yield return Cook(item.BurnTime, item.OutputObject);

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

    public override bool TryAddIngredientToPlate(GameObject Item, Object_Plate PlateObject)
    {
        bool IsIngredientAdded;
        ObjectHandler Object_Handler;
        Object_Handler = CurrentCounterItem.GetComponent<ObjectHandler>();


        IsIngredientAdded = PlateObject.AddIngredientToPlate(Object_Handler.ObjectSO);

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
