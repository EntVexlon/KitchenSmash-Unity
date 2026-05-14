using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private Button ResumeBtn;
    [SerializeField] private Button RestartBtn;
    [SerializeField] private Button MainMenuBtn;
    [SerializeField] private Button OptionsBtn;
    [SerializeField] private OptionsUI OptionsPanel;
    private bool IsGamePaused = false;


    private void Awake()
    {
        ResumeBtn.onClick.AddListener(() => EscapeHandler(this, EventArgs.Empty));
        RestartBtn.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            AudioListener.pause = false;
        });
        MainMenuBtn.onClick.AddListener(() =>
        {
            MainSceneLoader.LoadScene(MainSceneLoader.Scene.MainMenu);
            AudioListener.pause = false;
        });
        OptionsBtn.onClick.AddListener(() => {
            OptionsPanel.SetPanel();

        });

        ClientInput.Instance.OnGamePause += EscapeHandler;

    }


    private void Start() =>
        PausePanel.SetActive(false);


    private void SetPanel() { if (PausePanel != null) PausePanel.SetActive(true);  }
    private void HidePanel() { if (PausePanel != null) PausePanel.SetActive(false); }


    private void EscapeHandler(object sender, EventArgs e)
    {
        if (OptionsPanel.IsOptionMenuOpen)
        {
            OptionsPanel.HidePanel();
            return;
        }
        ToggleGamePausePanel();
    }
    private void ToggleGamePausePanel()
    {
        if (OptionsPanel.IsOptionMenuOpen) return;
        IsGamePaused = !IsGamePaused;

        Time.timeScale = IsGamePaused ? 0 : 1;
        AudioListener.pause = IsGamePaused; 

        if (IsGamePaused) SetPanel();
        else HidePanel();
    }

}
