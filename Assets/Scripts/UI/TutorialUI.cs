using UnityEngine;

public class TutorialUI :MonoBehaviour
{

    [SerializeField] private GameObject TutorialPanel;

    private void Start()
    {
        if(GameHandler.Instance.CurrentState is GameHandler.GameState.Standby)
            TutorialPanel.SetActive(true);
        GameHandler.Instance.OnStateChange += () =>
            {
             if (GameHandler.Instance.CurrentState is GameHandler.GameState.Countdown)
                 TutorialPanel.SetActive(false);
            };
    }
}
