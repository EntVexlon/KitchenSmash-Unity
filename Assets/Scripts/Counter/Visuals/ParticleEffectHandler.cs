using UnityEngine;

public class ParticleEffectHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] Objects;

    public void SetVisual(bool set)
    {
        foreach(GameObject obj in Objects) 
        obj.SetActive(set ?  true : false);
    }
}
