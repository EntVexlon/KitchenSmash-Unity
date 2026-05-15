using UnityEngine;

public class StoveWarnSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private float sound_timer;


    private void Update()
    {
        if (stoveCounter.GetCookState() is StoveCounter.CookState.Burning)
        {
            sound_timer -= Time.deltaTime;
            if (sound_timer <= 0)
            {
                float maxsound_timer = 0.2f;
                sound_timer = maxsound_timer;
                GetComponent<SoundHandler>().PlayAudioClip(transform, SoundId.Warning );
            }
        }
    }
}
