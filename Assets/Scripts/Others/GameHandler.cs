using System;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [HideInInspector] public static GameHandler Instance { get; private set; }
    [HideInInspector] public GameState CurrentState = GameState.Standby;

    [NonSerialized] public float CountdownTime = 3f;
    [NonSerialized] public float PlayTime;
    [NonSerialized] public float MaxPlayTime = 30f;

    public Action OnStateChange;
    [HideInInspector] public enum GameState
    {
        Standby,
        Countdown,
        Playing,
        Paused,
        GameOver
    }

    

    private void Awake()
    => Instance = this;

    private void Start() =>
        ClientInput.Instance.OnInteract += (object sender, EventArgs e) =>
        {
          if (CurrentState == GameState.Standby)
            CurrentState = GameState.Countdown;
          OnStateChange?.Invoke();
        };

    private void Update()
    {
        switch (CurrentState)
        {
            case GameState.Standby:
               
                break;
            case GameState.Countdown:
                CountdownTime -= Time.deltaTime;
                if (CountdownTime < 0)
                {
                    PlayTime = MaxPlayTime;
                    CurrentState = GameState.Playing;
                    OnStateChange?.Invoke();
                }
                break;
            case GameState.Playing:
                PlayTime -= Time.deltaTime;
                if (PlayTime < 0)
                    CurrentState = GameState.GameOver;
                break;
            case GameState.Paused:
                break;
            case GameState.GameOver:
                break;
        }
    }

    public float CurrentPlayTime() =>  1 - (PlayTime / MaxPlayTime);


}
