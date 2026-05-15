using TMPro;
using UnityEngine;

public class CountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TextUI;
    private Animator anim;
    private int prev_num;
    private const string NumPopUp = "NumPopUp";


    private void Start() => anim = GetComponent<Animator>();

    private void Update()
    {
        bool isCountdown = GameHandler.Instance.CurrentState is GameHandler.GameState.Countdown;
        TextUI.gameObject.SetActive(isCountdown);
        if (isCountdown) UpdateText();
    }

    private void UpdateText()
    {
        int current_num = Mathf.CeilToInt(GameHandler.Instance.CountdownTime);
        if (current_num == prev_num) return;
        prev_num = current_num;
        TextUI.text = current_num.ToString();
        GetComponent<SoundHandler>().PlayAudioClip(Camera.main.transform ,SoundId.CountDown, OneClip : true);
        anim.SetTrigger(NumPopUp);
    }
}