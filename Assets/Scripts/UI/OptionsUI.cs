using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private Button BackBtn;
    [SerializeField] private GameObject OptionsMenu;
    [NonSerialized] public bool IsOptionMenuOpen = false;

    private void Awake()
    {
        BackBtn.onClick.AddListener(() => HidePanel());

    }

    private void Start() =>
        OptionsMenu.SetActive(false);


    public void SetPanel() { if (OptionsMenu != null)
            IsOptionMenuOpen = true;
            OptionsMenu.SetActive(true); }
    public void HidePanel() { if (OptionsMenu != null)
            IsOptionMenuOpen = false;
        OptionsMenu.SetActive(false); }
}
