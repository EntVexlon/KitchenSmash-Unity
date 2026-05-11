using TMPro;
using UnityEditor.iOS.Xcode;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindEntry : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI KeyText;
    [SerializeField] private TextMeshProUGUI TipText;
    [SerializeField] private Button KeyBindBtn;
    [SerializeField] private ClientInput.BindKey BindKey;

    private void Awake()
    {
        TipText.gameObject.SetActive(false);
        KeyBindBtn.onClick.AddListener(StartReBind);
    }

    private void StartReBind()
    {
        KeyText.gameObject.SetActive(false);
        TipText.gameObject.SetActive(true);

        ClientInput.Instance.Rebind(BindKey, OnRebindComplete, OnRebindCancle);
    }

    private void OnRebindComplete() {
        RefreshKeyText();
        ResetVisual();
    }
    private void OnRebindCancle() => ResetVisual();

    private void RefreshKeyText() =>
        KeyText.text = ClientInput.Instance.GetBindKeyText(BindKey);

    private void ResetVisual()
    {
        KeyText.gameObject.SetActive(true);
        TipText.gameObject.SetActive(false);
    }
}
