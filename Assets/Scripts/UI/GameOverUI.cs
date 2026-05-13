using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject Container;
    [SerializeField] private TextMeshProUGUI TotalCompletedTasks;

    private bool IsGameOver = false;

    private void Update()
    {
        bool isGameOver = GameHandler.Instance.CurrentState is GameHandler.GameState.GameOver;

        if (isGameOver && !IsGameOver)
        {
            IsGameOver = true;
            SetPanel(); 
        }
        else if (!isGameOver && IsGameOver)
        {
            IsGameOver = false;
            HidePanel(); 
        }
    }

    private void SetPanel()
    {
        TotalCompletedTasks.text = DeliveryCounter.Instance.TotalCompletedTasks.ToString();
        Container.gameObject.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    private void HidePanel() =>
        Container.gameObject.SetActive(false);
   
}