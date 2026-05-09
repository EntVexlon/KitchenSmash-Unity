using UnityEngine;
using System.Collections;
public class AudioHandler : MonoBehaviour
{
    [SerializeField] private _AudioGroup AudioGroup;
    private AudioSource source;

    private void Awake() =>
        source = GetComponent<AudioSource>();
    public void LoopAudio(AudioType Type, bool mode)
    {
        foreach (var audio in AudioGroup.AudioList) { 
            if (audio.type == Type)
            {
                source.clip = audio.clip.Length < 1 ? audio.clip[0] :
                    audio.clip[Random.Range(0, audio.clip.Length)];
                if (mode && !source.isPlaying)
                    source.Play();
                else if (!mode && source.isPlaying)
                    source.Pause();
                break; 
            }
            }

    }
}
