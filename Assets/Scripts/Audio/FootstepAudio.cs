using UnityEngine;

// Its a Audio handler For Player Footstep
public class FootstepAudio : MonoBehaviour
{
    private float initial_time;
    private float max_time = .1f;

    private void Update()
    {
        if (initial_time > 0)
            initial_time -= Time.deltaTime;
    }
    public void TryPlayAudio(Transform transform)
    {
        if (initial_time > 0) return;
        initial_time = max_time;
        GetComponent<SoundHandler>().PlayAudioClip(transform, SoundId.Footstep);
    }
}
