using UnityEngine;
using UnityEngine.UI;
public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] public Image BarImage;
    [SerializeField] private GameObject[] CanvaObjects;
    private Color default_color;

    private void Start() =>
        default_color = BarImage.color;
    public void FillBar(float Min, float Max)
    {
        BarImage.fillAmount = Min / Max;
    }

    public void SetProgressbar(bool set)
    {
        foreach (GameObject obj in CanvaObjects)
        obj.SetActive(set ? true : false);
    }


    public void SetBarColor(Color color) => BarImage.color = color;
    public void ResetBarColor() => BarImage.color = default_color;
}
