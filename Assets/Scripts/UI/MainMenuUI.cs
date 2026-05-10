using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button QuitButton;

    private void Awake()
    {
        PlayButton.onClick.AddListener(() =>
        {
            MainSceneLoader.LoadScene(MainSceneLoader.Scene.GameScene);
        });
        QuitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        //Reset TimeScale to 1 in case the player quit the game while it was paused
        Time.timeScale = 1;
    }
}
