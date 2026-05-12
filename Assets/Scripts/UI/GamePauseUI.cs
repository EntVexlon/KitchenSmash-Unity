using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

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
        });
        MainMenuBtn.onClick.AddListener(() => MainSceneLoader.LoadScene(MainSceneLoader.Scene.MainMenu));
        OptionsBtn.onClick.AddListener(() => {
            OptionsPanel.SetPanel();

        });

        ClientInput.Instance.OnGamePause += EscapeHandler;

    }


    private void Start()
    {
        PausePanel.SetActive(false);
        // This is new for me. I didn’t know before that we could pass the
        // (s, e) event parameter using a lambda expression;
    }
    private void SetPanel() { if (PausePanel != null) PausePanel.SetActive(true); AudioListener.pause = true; }
    private void HidePanel() { if (PausePanel != null) PausePanel.SetActive(false); AudioListener.pause = false; }


    private void EscapeHandler(object sender, EventArgs e)
    {
        if (OptionsPanel.IsOptionMenuOpen)
        {
            OptionsPanel.HidePanel();
            return;
        }
        ToggleGamePausePanel();
    }
    public void ToggleGamePausePanel()
    {
        if(OptionsPanel.IsOptionMenuOpen) return;
        IsGamePaused = !IsGamePaused;
        if (IsGamePaused) { Time.timeScale = 0; SetPanel(); }
        else { Time.timeScale = 1; HidePanel(); }
    }

}
