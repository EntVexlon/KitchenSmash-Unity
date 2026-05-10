using UnityEngine;
using UnityEngine.UI;

public class ProgressRingUI : MonoBehaviour
{
    [SerializeField] private Image ProgressRing;

    private void Update() =>
        ProgressRing.fillAmount = GameHandler.Instance.CurrentPlayTime();
}
