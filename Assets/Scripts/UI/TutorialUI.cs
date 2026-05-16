using UnityEngine;

public class TutorialUI :MonoBehaviour
{

    [SerializeField] private GameObject TutorialPanel;

    private void Start()
    {
        if (GameHandler.Instance.CurrentState is GameHandler.GameState.Standby)
        {
            TutorialPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        GameHandler.Instance.OnStateChange += () =>
            {
             if (GameHandler.Instance.CurrentState is GameHandler.GameState.Countdown)
                 TutorialPanel.SetActive(false);
                Time.timeScale = 1f;
            };
    }
}
