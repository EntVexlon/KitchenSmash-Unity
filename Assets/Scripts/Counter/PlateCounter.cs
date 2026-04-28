using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : MonoBehaviour, IinteractCounter
{
    [SerializeField] public Transform CounterTopPoint;
    [SerializeField] public Transform CounterTop;
    [SerializeField] private _ItemObject ItemObject;
    private KitchenObject Kitchen_Object;
    private List<GameObject> PlateObject;
    private int MaxPlateCount = 4;
    private int PlateCount = 0;
    private float StackOrder = 0.1f;
    private float cooldown = 3f;
    private float NxtPlateSpawnTime = 0;
    public bool CounterHaveItem { get; set; }
    public GameObject CurrentCounterItem { get; set; }

    private void Start()
    {
        PlateObject = new List<GameObject>();
        //StartCoroutine(PlateSpawn());
        //Debug.Log("Hy I am Start");
    }

    public void TryDropItem(GameObject Item) { }
    public void InteractAction() { }


    //public GameObject TryPickUpItem(Player ph)
    //{
    //IngredientObject = Instantiate(ItemObject.Prefab);

    //Kitchen_Object = IngredientObject.GetComponent<KitchenObject>();
    //    Kitchen_Object.CurrentItemName = ItemObject.ObjectName;
    //    Kitchen_Object.SetParent(ph.ItemHolder, ph.ItemHoldPoss.position);

    //    return IngredientObject;
    //}

    public GameObject TryPickUpItem(Player ph)
    {
        if (PlateCount == 0) return null;
        NxtPlateSpawnTime = Time.time + cooldown;
        GameObject item = PlateObject[PlateObject.Count - 1];
        Kitchen_Object = item.GetComponent<KitchenObject>();
        Kitchen_Object.SetParent(ph.ItemHolder, ph.ItemHoldPoss.position);
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


    //private IEnumerator PlateSpawn()
    //{
    //    while (true) // why i do this ? :)
    //    {
    //    Debug.Log("Im running");
    //        if (PlateCount < MaxPlateCount && Time.time >= nextPlateSpawnTime)
    //        {
    //            GameObject item = Instantiate(ItemObject.Prefab);
    //            Debug.Log( "Before: " +PlateObject.Count);
    //            PlateObject.Add(item);
    //            item.GetComponent<KitchenObject>().SetParent(
    //                CounterTop, CounterTopPoint.position + Vector3.up * (PlateCount * StackOrder));
    //            PlateCount++;
    //            nextPlateSpawnTime = Time.time + cooldown;
    //        }
    //        yield return null;
    //    }
    //}

    private void Update()
    {
        if (PlateCount < MaxPlateCount && Time.time >= NxtPlateSpawnTime)
        {
            GameObject item = Instantiate(ItemObject.Prefab);
            PlateObject.Add(item);
            item.GetComponent<KitchenObject>().SetParent(
                CounterTop, CounterTopPoint.position + Vector3.up * (PlateCount * StackOrder));
            PlateCount++;
            NxtPlateSpawnTime = Time.time + cooldown;
        }
    }
}








