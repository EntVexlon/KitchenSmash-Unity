using UnityEngine;
using System.Collections;

public class TrashCounter : MonoBehaviour , IinteractCounter
{
    public GameObject TryPickUpItem(Player ph) => null;
    public bool CounterHaveItem { get; set; } = false;
    public GameObject CurrentCounterItem { get; set; }
     
    public void InteractAction() { }
    public void TryDropItem(GameObject Item) {
        Destroy(Item);
        CounterHaveItem = true;
        StartCoroutine(ResetCounter());
    }

    private IEnumerator ResetCounter()
    {
        yield return null;  
        CounterHaveItem = false;
    }
}
