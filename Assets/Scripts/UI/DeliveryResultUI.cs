using System.Collections;
using UnityEngine;

public class DeliveryResultUI : MonoBehaviour
{
    [SerializeField] private DeliveryCounter delivery_counter;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject DeliverySuccessUI;
    [SerializeField] private GameObject DeliveryFailUI;
    private const string PopUp = "PopUp";

    private Coroutine routine;

    private void Start()
    {
        DeliverySuccessUI.SetActive(false);
        DeliveryFailUI.SetActive(false);

        delivery_counter.OnCorrectOrder += () => StartCoroutine(ShowResult(DeliverySuccessUI));
        delivery_counter.OnWrongOrder += () => StartCoroutine(ShowResult(DeliveryFailUI));
    }


    private IEnumerator ShowResult(GameObject @object)
    {
        @object.SetActive(true);
        yield return null;

        anim.SetTrigger(PopUp);
        yield return null;

        //Here Also Can Add For Which Node Should Check But Currenly I Have Only One Node
        yield return new WaitUntil(() =>
        anim.GetCurrentAnimatorStateInfo(0).length < anim.GetCurrentAnimatorStateInfo(0).normalizedTime);

        @object.SetActive(false);
    }
}



