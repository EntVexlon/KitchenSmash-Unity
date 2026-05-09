using System.Collections;
using UnityEngine;

public class SoundEffectHandler : MonoBehaviour
{


    [SerializeField] private _SoundEffectGroup SoundEffectGroup;
    private float initial_time;

    private void Update()
    {
        if (initial_time > 0)
            initial_time -= Time.deltaTime;
    }

    public void PlayAudioClip(Transform transform, SfxType type, float volume = 1f)
    {
        foreach (var list in SoundEffectGroup.SoundEffectList)
        {
            if (list.Type == type)
                AudioSource.PlayClipAtPoint(list.Clip[Random.Range(0, list.Clip.Length)],
                    transform.position, volume);
        }
    }


    public void TimedAudioPlayer(Transform transform, SfxType type, float volume = 1f, float max_time = .1f)
    {
        if (initial_time > 0) return;
        initial_time = max_time;
        PlayAudioClip(transform, type, volume);
    }


}