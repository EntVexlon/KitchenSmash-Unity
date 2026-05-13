using TMPro;
using UnityEngine;

public class HotKeyUI : MonoBehaviour
{
    [SerializeField] private ClientInput.BindKey BindKey;
    [SerializeField] private TextMeshProUGUI KeyText;



    private void Start() {
        KeyText.text = ClientInput.Instance.GetBindKeyText(BindKey);
        ClientInput.Instance.OnHotKeyChange += () =>
        KeyText.text = ClientInput.Instance.GetBindKeyText(BindKey);
    }

}
