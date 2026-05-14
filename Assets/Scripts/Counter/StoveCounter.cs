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

    [Serializable]
    public struct ValidItem
    {
        public _IngredientItem RawItem;
        public _CookItem CookedItem;
        public float CookTime;
        public _CookItem BurnerdItem;
        public float BurnTime;
    }

    private float TotalCookDuration = 0f;
    private bool IsCooking = false;

    private void Update() =>
        GetComponent<SizzleAudio>().PlayAudioClip(transform, IsCooking);

    public override void TryDropItem(GameObject CurrentItem)
    {
        if (CounterHaveItem) return;

        foreach (ValidItem item in ValidItems)
        {
            if (!CurrentItem.TryGetComponent(out ObjectHandler object_handler)) continue;
            if (object_handler._Object != item.RawItem) continue;

            CurrentCounterItem = CurrentItem;
            TotalCookDuration = item.CookTime + item.BurnTime;

            Progress_BarUI.SetProgressbar(true);
            Progress_BarUI.FillBar(object_handler.Progress, TotalCookDuration);

            object_handler.SetParent(CounterTop, CounterTop.position);
            CounterHaveItem = true;
            return;
        }
    }

    public override GameObject TryPickUpItem(Player ph)
    {
        if (!CounterHaveItem) return null;

        Progress_BarUI.SetProgressbar(false);
        EffectHandler.SetVisual(false);
        StopAllCoroutines();

        CurrentCounterItem?.GetComponent<ObjectHandler>()
            .SetParent(ph.ItemHold, ph.ItemHold.position);

        CounterHaveItem = false;
        IsCooking = false;

        GameObject @object = CurrentCounterItem;
        CurrentCounterItem = null;
        return @object;
    }

    public override void InteractAction()
    {
        if (!CounterHaveItem || IsCooking) return;

        foreach (ValidItem item in ValidItems)
        {
            if (CurrentCounterItem.GetComponent<ObjectHandler>()._Object != item.RawItem)
                continue;

            TotalCookDuration = item.CookTime + item.BurnTime;
            StartCoroutine(CookValidation(item));
            return;
        }
    }

    private IEnumerator CookValidation(ValidItem item)
    {
        float initial_progress = CurrentCounterItem.GetComponent<ObjectHandler>().Progress;

        if (initial_progress < item.CookTime)
        {
            yield return CookPhase(
                phase_end: item.CookTime,
                output_object: item.CookedItem.OutputObject,
                item: item
            );
        }
        //else
        //    ReplaceItem(item.CookedItem.OutputObject, item.CookTime);

        if (!CounterHaveItem) yield break;

        float burn_state = CurrentCounterItem.GetComponent<ObjectHandler>().Progress;
        if (burn_state < TotalCookDuration)
        {
            yield return CookPhase(
                phase_end: TotalCookDuration,
                output_object: item.BurnerdItem.OutputObject,
                item: item
            );
        }
    }

    private IEnumerator CookPhase(float phase_end, GameObject output_object, ValidItem item)
    {
        IsCooking = true;
        EffectHandler.SetVisual(true);

        CurrentCounterItem.TryGetComponent(out ObjectHandler object_handler);

        while (object_handler.Progress < phase_end)
        {
            object_handler.Progress += Time.deltaTime;
            object_handler.Progress = Mathf.Min(object_handler.Progress, phase_end);
            Progress_BarUI.FillBar(object_handler.Progress, TotalCookDuration);
            yield return null;
        }

        ReplaceItem(output_object, object_handler.Progress);
        IsCooking = false;
    }


    private void ReplaceItem(GameObject prefab, float progress)
    {
        Destroy(CurrentCounterItem);
        CurrentCounterItem = Instantiate(prefab);

        ObjectHandler obectj_handler = CurrentCounterItem.GetComponent<ObjectHandler>();
        obectj_handler.Progress = progress;        
        obectj_handler.SetParent(CounterTop, CounterTop.position);
        CounterHaveItem = true;
    }

    public override bool TryAddItem(GameObject item, Object_Plate plateObject)
    {
        ObjectHandler handler = CurrentCounterItem.GetComponent<ObjectHandler>();
        if (!plateObject.AddIngredientToPlate(handler._Object as _BaseItem)) return false;

        StopAllCoroutines();
        EffectHandler.SetVisual(false);
        IsCooking = false;
        Progress_BarUI.SetProgressbar(false);

        GameObject plate = CurrentCounterItem;
        CurrentCounterItem = item;

        CurrentCounterItem.GetComponent<ObjectHandler>()
            .SetParent(plateObject.PlateTop, plateObject.PlateTop.position);

        CurrentCounterItem = plate;
        return true;
    }
}