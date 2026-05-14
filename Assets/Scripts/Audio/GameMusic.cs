using UnityEngine;

public class GameMusic : MonoBehaviour
{
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        GameHandler.Instance.OnStateChange += () =>
        {
            if (GameHandler.Instance.CurrentState is GameHandler.GameState.Countdown) 
                source.Play();
        };

        source.volume = UserSetting.Instance.MusicVolume;

        UserSetting.Instance.OnAudioUpdate += () => { 
            source.volume = UserSetting.Instance.MusicVolume;
        };
    }
}