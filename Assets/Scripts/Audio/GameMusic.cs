using UnityEngine;

public class GameMusic : MonoBehaviour
{
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.volume = UserSetting.Instance.MusicVolume;

        UserSetting.Instance.OnAudioUpdate += () => { 
            source.volume = UserSetting.Instance.MusicVolume;
        };
    }
}