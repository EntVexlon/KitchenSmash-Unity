using UnityEngine;
using System.Collections.Generic;

public class SoundHandler : MonoBehaviour
{
    [SerializeField] private _SoundLibrary SoundLib;

    private Dictionary<SoundId, int> locked_Index = new Dictionary<SoundId, int>();

    public void PlayAudioClip(Transform transform, SoundId sound_id, float volume = 1f, bool RandomClip = false)
    {
        volume = UserSetting.Instance.SoundVolume;
        foreach (var list in SoundLib.SoundList)
        {
            if (list.Id != sound_id) continue;

            int index;
            if (RandomClip)
            {
                if (!locked_Index.ContainsKey(sound_id))
                    locked_Index[sound_id] = Random.Range(0, list.Clip.Length);
                index = locked_Index[sound_id];
            }
            else
                index = Random.Range(0, list.Clip.Length);

            AudioSource.PlayClipAtPoint(list.Clip[index], transform.position, volume);
            return;
        }
    }

    //Like Its For After Restart , like to Pick New One On Restart 
    public void ResetLockedClip(SoundId sound_id) =>
        locked_Index.Remove(sound_id);
}