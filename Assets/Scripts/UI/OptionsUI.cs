using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }
    [SerializeField] private Button BackBtn;
    [SerializeField] private GameObject OptionsMenu;
    [SerializeField] public Slider MusicVolumeSlider;
    [SerializeField] public Slider SFxVolumeSlider;
    [NonSerialized] public bool IsOptionMenuOpen = false;


    private void Start()
    {
        BackBtn.onClick.AddListener(() => HidePanel());
        Instance = this;
        MusicVolumeSlider.value = UserSetting.Instance.MusicVolume;
        SFxVolumeSlider.value = UserSetting.Instance.SoundVolume;

        // Music slider changed → pass new music value + current sfx value
        MusicVolumeSlider.onValueChanged.AddListener(value =>
            UserSetting.Instance.SetVolume(value, SFxVolumeSlider.value));

        // SFx slider changed → pass current music value + new sfx value
        SFxVolumeSlider.onValueChanged.AddListener(value =>
            UserSetting.Instance.SetVolume(MusicVolumeSlider.value, value));

        //
        OptionsMenu.SetActive(false);

    }




    public void SetPanel() { if (OptionsMenu != null)
            IsOptionMenuOpen = true;
            OptionsMenu.SetActive(true); }
    public void HidePanel() { if (OptionsMenu != null)
            IsOptionMenuOpen = false;
        OptionsMenu.SetActive(false); 
    }
}
