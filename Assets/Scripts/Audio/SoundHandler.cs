using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    [SerializeField] private _SoundLibrary SoundLib;

    public void PlayAudioClip(Transform transform, SoundId sound_id, float volume = 1f)
    {
        volume = UserSetting.Instance.SoundVolume;
        foreach (var list in SoundLib.SoundList)
        {
            if (list.Id == sound_id)
                AudioSource.PlayClipAtPoint(list.Clip[Random.Range(0, list.Clip.Length)],
                    transform.position, volume);
        }
    }



}