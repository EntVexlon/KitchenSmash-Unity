using UnityEngine;

//Its a Audio handler For Stove Couner
public class SizzleAudio : MonoBehaviour
{
    [SerializeField] private _SoundLibrary SoundLib;
    [SerializeField] private AudioSource source;
    public void PlayAudioClip(Transform transform, bool mode, float volume = 1f)
    {
        volume = UserSetting.Instance.SoundVolume;
        source.volume = volume;

        foreach (var list in SoundLib.SoundList)
        {
            if (list.Id == SoundId.PanSizzle)
            {
                source.clip = list.Clip.Length < 1 ? list.Clip[0] : list.Clip[Random.Range(0, list.Clip.Length)];


                if (!source.isPlaying && mode)
                    source.Play();
                else if (source.isPlaying && !mode)
                    source.Pause();
            }
        }
    }
}
