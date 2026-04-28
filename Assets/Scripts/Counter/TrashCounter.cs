using UnityEngine;
using System.Collections;

public class TrashCounter : BaseCounter
{
    public override void TryDropItem(GameObject Item) {
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
