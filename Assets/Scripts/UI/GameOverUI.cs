using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject Container;
    [SerializeField] private TextMeshProUGUI TotalCompletedTasks;

    private void Update()
    {
        if (GameHandler.Instance.CurrentState is
            GameHandler.InGameState.GameOver)
            SetPanel();
        else
            HidePanel();
    }

    private void SetPanel()
    {
        TotalCompletedTasks.text = DeliveryCounter.Instance.TotalCompletedTasks.ToString();
        Container.gameObject.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }
    private void HidePanel()
    {
        Container.gameObject.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;

    }
}
