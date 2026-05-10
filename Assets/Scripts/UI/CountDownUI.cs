using TMPro;
using UnityEngine;

public class CountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TextUI;

    private void Update()
    {
        if(GameHandler.Instance.CurrentState is
            GameHandler.InGameState.InCountdown)
            SetText();
        else
            HideText();
    }

    private void SetText() {
    TextUI.gameObject.SetActive(true);
    TextUI.text = Mathf.CeilToInt(GameHandler.Instance.CountdownTime).ToString();
    }
    private void HideText() {
    TextUI.gameObject.SetActive(false);
    }
}
