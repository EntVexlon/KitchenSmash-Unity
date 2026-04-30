using UnityEngine;

public class CounterVisualHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] IndicatorObject;
    [SerializeField] ContainerCounter Container_Counter;
    [SerializeField] private Animator CounterModelAnim;
    [SerializeField] private Animator KnifeAnim;
    [SerializeField] private CuttingCounter Ctting_Counter;
    private void Start()
    {
        if (Container_Counter != null)
            Container_Counter.OnTryPickUpIngredient += OnPickUpObject;
        if (Ctting_Counter != null)
            Ctting_Counter.OnItemCut += KnifePlay;
    }
    public void SetIndicator()
    {
        foreach (GameObject SelectionObject in IndicatorObject)
        {
            SelectionObject.gameObject.SetActive(true);
        }}

    public void HideIndicator()
    {
        if (IndicatorObject == null) return;
        foreach (GameObject SelectionObject in IndicatorObject)
            SelectionObject.gameObject.SetActive(false);
    }

    //For Cutting Counter
    public void KnifePlay()
    {
        KnifeAnim.SetTrigger("Cut");
    }

    //
    private void OnPickUpObject()
    { CounterModelAnim.SetTrigger("OpenClose"); }

    ///<summary>
    /// Line 38:
    /// Yeah I Also Wished to Not use Strings i Just Hate But theirs is no
    /// Othey Way So..
    ///</summary>
}