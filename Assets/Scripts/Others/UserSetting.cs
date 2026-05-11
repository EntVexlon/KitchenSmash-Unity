using System;
using UnityEngine;

public  class UserSetting  : MonoBehaviour
{
    public static UserSetting Instance { get; private set; }

    [NonSerialized] public  float MusicVolume;
    [NonSerialized] public  float SoundVolume;
    private float DefaultVolume = 1f;
    //Sound Effect Volume ^

    // Data Keys
    private const string User_MusicVolume = "MusicVolume";
    private const string User_SoundVolume = "SoundVolume";

    private void Awake() { Instance = this; LoadSetting(); }

    public void SetVolume(float music_volume, float sound_volume)
    {
      PlayerPrefs.SetFloat(User_MusicVolume, music_volume);
        MusicVolume = music_volume;
      PlayerPrefs.SetFloat(User_SoundVolume, sound_volume);
        SoundVolume = sound_volume;

        PlayerPrefs.Save();
    }

    private void LoadSetting()
    {
        MusicVolume = PlayerPrefs.GetFloat(User_MusicVolume , DefaultVolume);
        SoundVolume = PlayerPrefs.GetFloat(User_SoundVolume, DefaultVolume);
    }
}
