using UnityEngine;
using UnityEngine.UI;
public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] public Image BarImage;
    [SerializeField] private GameObject[] CanvaObjects;
     public void ProgressBar(float Min,float Max)
    {
        BarImage.fillAmount = Min / Max;
    }

    public void SetProgressbar(bool set)
    {
        foreach (GameObject obj in CanvaObjects)
        {
            if (set) obj.SetActive(true);
            else obj.SetActive(false);
        }
    }

}
