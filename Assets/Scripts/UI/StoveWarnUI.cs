using UnityEngine;
using UnityEngine.UI;

public class StoveWarnUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stove_counter;
    [SerializeField] private ProgressBarUI Progress_BarUI;
    [SerializeField] private Animator WarnAnim;
    [SerializeField] private Animator BarAnim;
    [SerializeField] private Image WarnIcon;

    private const string SetWarnIcon = "SetWarnIcon";
    private const string FlashBar = "FlashBar";


    private void Start()
    {
        WarnIcon.gameObject.SetActive(false);


        stove_counter.OnCookStateChange += () =>
        {
            StoveCounter.CookState state = stove_counter.GetCookState();

            if (state is StoveCounter.CookState.Burning)
            {
                WarnIcon.gameObject.SetActive(true);
                WarnAnim.SetTrigger(SetWarnIcon);
                BarAnim.enabled = true;
                BarAnim.SetBool(FlashBar, true);
            }
            else if (state is StoveCounter.CookState.Burned)
            {
                WarnIcon.gameObject.SetActive(false);
                BarAnim.SetBool(FlashBar, false);
                BarAnim.enabled = false;
                Progress_BarUI.SetBarColor(Color.red);
            }
            else if (state is StoveCounter.CookState.Idle || !stove_counter.CounterHaveItem)
            {
                WarnIcon.gameObject.SetActive(false);
                BarAnim.SetBool(FlashBar, false);
                Progress_BarUI.ResetBarColor();
            }
        }; ;
    }
}
