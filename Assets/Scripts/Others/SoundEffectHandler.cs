using UnityEngine;

public class SoundEffectHandler : MonoBehaviour
{
    [SerializeField] private _SoundEffectGroup SoundEffectGroup;

    public void OneTimeAudio(Transform transform, SfxType type, float volume = 1f)
    {
        foreach (var list in SoundEffectGroup.SoundEffectList)
        {
            if (list.Type == type)
                AudioSource.PlayClipAtPoint(list.Clip[Random.Range(0, list.Clip.Length)],
                    transform.position, volume);
        }
    }

  

}