using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    [SerializeField] public Transform CounterTop;
    [SerializeField] private _ItemObject ItemObject;
    private ObjectHandler Kitchen_Object;
    private List<GameObject> PlateObject;
    private int MaxPlateCount = 4;
    private int PlateCount = 0;
    private float StackOrder = 0.1f;
    private float PlateSpawnCooldown = 3f;
    private float NxtPlateSpawnTime = 0;

    private void Start() =>
        PlateObject = new List<GameObject>();



    public override GameObject TryPickUpItem(Player ph)
    {
        if (PlateCount == 0) return null;
        NxtPlateSpawnTime = Time.time + PlateSpawnCooldown;
        GameObject item = PlateObject[PlateObject.Count - 1];
        Kitchen_Object = item.GetComponent<ObjectHandler>();
        Kitchen_Object.SetParent(ph.ItemHold, ph.ItemHold.position);
        PlateObject.Remove(PlateObject[^1]);
        PlateCount--;
        return item;
    }

    // Yeah The [^1]
    // The this ^ operator is suggested by visual studio
    // when visual studio saw i am hardcoding the it the suggested that 
    // i mean this 
    //  PlateObject.Remove(PlateObject[PlateObject.Count - 1]);
    // and that operator called index-from-end operator
    //mean if it ^1 then it point to the index last elment 
    // else if ^2 then it point the second last elemt


    private void Update()
    {
        if (PlateCount < MaxPlateCount && Time.time >= NxtPlateSpawnTime)
        {
            GameObject item = Instantiate(ItemObject.Prefab);
            PlateObject.Add(item);
            item.GetComponent<ObjectHandler>().SetParent(
                CounterTop, CounterTop.position + Vector3.up * (PlateCount * StackOrder));
            PlateCount++;
            NxtPlateSpawnTime = Time.time + PlateSpawnCooldown;
        }
    }
}








