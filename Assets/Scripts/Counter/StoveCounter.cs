using System.Collections;
using UnityEngine;

public class StoveCounter : MonoBehaviour ,IinteractCounter
{
    [SerializeField] public Transform CounterTopPoint;
    [SerializeField] public Transform CounterTop;
    [SerializeField] private ProgressBarUI ProgressBar;
    [SerializeField] private ParticleEffectHandler EffectHandler;
    [SerializeField] private _CookItem[] CookItmes;
    private float CookTime = 0;
    private bool IsCooking = false;
    public bool CounterHaveItem { get; set; } = false;
    public GameObject CurrentCounterItem { get; set; }
    public void TryDropItem(GameObject Item) {
        if (CounterHaveItem) return;
        foreach (_CookItem item in CookItmes)
        {
            CurrentCounterItem = Item;
            if (CurrentCounterItem?.GetComponent<KitchenObject>().CurrentItemName == item.InputObjectName)
            {
                ProgressBar.SetProgressbar(true);
                ProgressBar.FillBar(CookTime, CookTime > item.RequiredCookTime ? item.RequiredCookTime : item.BurnTime);
                CurrentCounterItem.GetComponent<KitchenObject>().SetParent(
                    CounterTop, CounterTopPoint.position);
                CounterHaveItem = true;
            }
        }
    }
    public GameObject TryPickUpItem(Player ph){
        if (!CounterHaveItem) return null;
        ProgressBar.SetProgressbar(false);
        EffectHandler.SetVisual(false);
        StopAllCoroutines();
        CurrentCounterItem?.GetComponent<KitchenObject>().SetParent(ph.ItemHolder, ph.ItemHoldPoss.position);
        CounterHaveItem = false;
        IsCooking = false;
        GameObject @object = CurrentCounterItem;
        CurrentCounterItem = null;
        return @object;
    }

    // I know so much bools but 
    // maybe in future i will refactor it maybe :) 

    public void InteractAction() {
    if(!CounterHaveItem || IsCooking) return;
        foreach (_CookItem item in CookItmes)
        {
            if (item.InputObjectName == CurrentCounterItem?.GetComponent<KitchenObject>().CurrentItemName)
                StartCoroutine(AssignCookMethod(item));
        }
    }


    private IEnumerator AssignCookMethod(_CookItem item)
    {
        if (!CounterHaveItem) yield break;
        yield return Cook(item.RequiredCookTime, item.CookedObject);
        yield return Cook(item.BurnTime, item.BurrnedObject);

    }

    private IEnumerator Cook(float time, GameObject item)
    {
        IsCooking = true;
        while (CookTime < time)
        {
            CookTime += Time.deltaTime;
            EffectHandler.SetVisual(true);
            ProgressBar.FillBar(CookTime, time);
            yield return null;
        }
        Destroy(CurrentCounterItem);
        CurrentCounterItem = Instantiate(item);
        CurrentCounterItem.GetComponent<KitchenObject>().SetParent(CounterTopPoint,
        CounterTopPoint.position);
        CounterHaveItem = true;
        IsCooking = false;
        CookTime = 0;

    }



}
