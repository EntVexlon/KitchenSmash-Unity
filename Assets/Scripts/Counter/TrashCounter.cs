using UnityEngine;
using System.Collections;

public class TrashCounter : BaseCounter
{
    public override void TryDropItem(GameObject Item) {
        Destroy(Item);
        CounterHaveItem = true;
        GetComponent<SoundHandler>().PlayAudioClip(transform, SoundId.TrashBin);
        StartCoroutine(ResetCounter());
    }

    private IEnumerator ResetCounter()
    {
        yield return null;  
        CounterHaveItem = false;
    }
}
