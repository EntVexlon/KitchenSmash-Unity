using System;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [HideInInspector] public static GameHandler Instance { get; private set; }
    [HideInInspector] public InGameState CurrentState;

    [NonSerialized] public float CountdownTime = 3f;
    [NonSerialized] public float BootingTime;
    [NonSerialized] public float PlayTime;
    [NonSerialized] public float MaxPlayTime = 30f;
    [HideInInspector] public enum InGameState
    {
        Booting,
        InCountdown,
        Playing,
        Paused,
        GameOver
    }

    private void Awake()
    => Instance = this;

    private void Update()
    {
        switch (CurrentState)
        {
            case InGameState.Booting:
                BootingTime -= Time.deltaTime;
                if (BootingTime < 0)
                    CurrentState = InGameState.InCountdown;
                break;
            case InGameState.InCountdown:
                CountdownTime -= Time.deltaTime;
                if (CountdownTime < 0)
                {
                    PlayTime = MaxPlayTime;
                    CurrentState = InGameState.Playing;
                }
                break;
            case InGameState.Playing:
                PlayTime -= Time.deltaTime;
                if (PlayTime < 0)
                    CurrentState = InGameState.GameOver;
                break;
            case InGameState.Paused:
                break;
            case InGameState.GameOver:
                break;
        }
    }

    public float CurrentPlayTime() =>  1 - (PlayTime / MaxPlayTime);


}
