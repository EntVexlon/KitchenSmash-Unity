using Unity.VisualScripting;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        if (UserSetting.Instance != null)
            source.volume = UserSetting.Instance.MusicVolume;
    }

    private void Update() =>
    source .volume =  UserSetting.Instance.MusicVolume;

}