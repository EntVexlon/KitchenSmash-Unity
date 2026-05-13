using UnityEngine;
using System.Collections;

// ─────────────────────────────────────────────
//  VehicleAudioHandler.cs
// ─────────────────────────────────────────────
[RequireComponent(typeof(AudioSource))]
public class VehicleAudioHandler : MonoBehaviour
{
    [Header("Engine")]
    [SerializeField] private AudioClip EngineClip;
    [SerializeField][Range(0f, 1f)] private float EngineVolume = 1f;

    [Header("Horn")]
    [SerializeField] private AudioClip[] HornClips;
    [SerializeField][Range(0f, 0.05f)] private float HornChance = 0.005f;
    [SerializeField][Range(0f, 1f)] private float BumpHonkChance = 0.4f;
    [SerializeField][Range(0f, 1f)] private float HornVolume = 0.55f;

    [Header("3D Spatial")]
    [SerializeField] private float MinDist = 10f;
    [SerializeField] private float MaxDist = 70f;
    [SerializeField] private float DopplerLevel = 0.8f;

    // ── Runtime ──────────────────────────────
    private AudioSource engineSrc;
    private AudioSource hornSrc;
    private bool hornCooldown;
    private bool engineStarted;
    private float userVolume = 1f;
    private bool isDestroyed;

    private const float HornCooldownTime = 8f;

    // ─────────────────────────────────────────
    private void Awake()
    {
        engineSrc = GetComponent<AudioSource>();
        Configure3D(engineSrc, MinDist, MaxDist, DopplerLevel);
        engineSrc.loop = true;
        engineSrc.volume = 0f;
        engineSrc.playOnAwake = false;

        hornSrc = gameObject.AddComponent<AudioSource>();
        Configure3D(hornSrc, MinDist * 1.5f, MaxDist * 1.2f, 0f);
        hornSrc.loop = false;
        hornSrc.playOnAwake = false;

        if (UserSetting.Instance != null)
        {
            userVolume = UserSetting.Instance.SoundVolume;
            UserSetting.Instance.OnAudioUpdate += RefreshVolume; // ← subscribe
        }
    }


    // ── Called by VehicleMoveHandler ─────────
    public void StartEngine()
    {
        if (EngineClip == null || engineStarted) return;
        engineStarted = true;

        engineSrc.clip = EngineClip;
        // Slight pitch variation so every car sounds different
        engineSrc.pitch = Random.Range(0.93f, 1.07f);
        engineSrc.Play();

        // Fade in from 0 — prevents the hard-pop on spawn
        StartCoroutine(FadeVol(engineSrc, 0f, EngineVolume * userVolume, 1.2f));

        if (HornClips != null && HornClips.Length > 0)
            StartCoroutine(PassiveHornLoop());
    }


    // ── Called By UserSetting  ──
    public void RefreshVolume()
    {
        if (UserSetting.Instance == null || isDestroyed) return;
        userVolume = UserSetting.Instance.SoundVolume;

        // update engine volume immediately if playing
        if (engineStarted && engineSrc != null)
            engineSrc.volume = EngineVolume * userVolume;
    }

    // ── Called by VehicleMoveHandler when slowing ──
    public void SetEngineIdle(bool idle)
    {
        if (!engineStarted || isDestroyed) return;

        float targetPitch = idle ? 0.68f : Random.Range(0.93f, 1.07f);
        float targetVol = idle
            ? EngineVolume * userVolume * 0.42f
            : EngineVolume * userVolume;

        StartCoroutine(SmoothAudio(engineSrc, targetPitch, targetVol, 0.5f));
    }

    // ── Called on physics bump ────────────────
    public void TryHonkOnBump()
    {
        if (!hornCooldown && !isDestroyed && Random.value < BumpHonkChance)
            StartCoroutine(PlayHorn());
    }

    // ── Passive random horn ───────────────────
    private IEnumerator PassiveHornLoop()
    {
        yield return new WaitForSeconds(Random.Range(3f, 8f));
        while (!isDestroyed)
        {
            yield return new WaitForSeconds(1f);
            if (!hornCooldown && Random.value < HornChance)
                StartCoroutine(PlayHorn());
        }
    }

    private IEnumerator PlayHorn()
    {
        if (HornClips.Length == 0 || isDestroyed) yield break;
        hornCooldown = true;
        AudioClip clip = HornClips[Random.Range(0, HornClips.Length)];

        // PlayOneShot on the horn AudioSource attached to car
        // → sound moves WITH the car automatically, no PlayClipAtPoint needed
        hornSrc.PlayOneShot(clip, HornVolume * userVolume);

        yield return new WaitForSeconds(clip.length + HornCooldownTime);
        hornCooldown = false;
    }

    // ── Destroy: clean fade, zero stutter ────
    private void OnDestroy()
    {
        isDestroyed = true;
        StopAllCoroutines();

        if (UserSetting.Instance != null)
            UserSetting.Instance.OnAudioUpdate -= RefreshVolume; // ← unsubscribe

        Vector3 lastPos = transform.position;
        if (engineSrc != null && engineSrc.isPlaying)
            SpawnFadeOutGhost(engineSrc, lastPos, fadeDur: 0.4f);
        if (hornSrc != null && hornSrc.isPlaying && hornSrc.clip != null)
            SpawnFadeOutGhost(hornSrc, lastPos, fadeDur: 0.2f);
    }

    // Creates a static ghost AudioSource at the car's last position,
    // fades it out quickly, then destroys itself.
    // This replaces PlayClipAtPoint — same idea but with fade + correct settings.
    private static void SpawnFadeOutGhost(AudioSource src, Vector3 worldPos, float fadeDur)
    {
        // Skip if nothing meaningful to play
        if (src.clip == null || src.volume < 0.01f) return;

        GameObject ghost = new GameObject("_AudioGhost");
        ghost.transform.position = worldPos;

        AudioSource g = ghost.AddComponent<AudioSource>();
        g.clip = src.clip;
        g.volume = src.volume;
        g.pitch = src.pitch;
        g.spatialBlend = src.spatialBlend;
        g.rolloffMode = src.rolloffMode;
        g.minDistance = src.minDistance;
        g.maxDistance = src.maxDistance;
        g.dopplerLevel = 0f;          // no doppler on a static ghost
        g.loop = false;

        // Continue from exact sample position — no restart click
        g.timeSamples = Mathf.Min(src.timeSamples, src.clip.samples - 1);
        g.Play();

        CoroutineRunner.Run(FadeAndDestroy(g, fadeDur));
    }

    private static IEnumerator FadeAndDestroy(AudioSource src, float dur)
    {
        if (src == null) yield break;

        float startVol = src.volume;
        for (float t = 0f; t < dur; t += Time.deltaTime)
        {
            if (src == null) yield break;
            src.volume = Mathf.Lerp(startVol, 0f, t / dur);
            yield return null;
        }

        if (src != null) Destroy(src.gameObject);
    }

    // ── Helpers ───────────────────────────────
    private IEnumerator FadeVol(AudioSource src, float from, float to, float dur)
    {
        for (float t = 0f; t < dur; t += Time.deltaTime)
        {
            if (src == null || isDestroyed) yield break;
            src.volume = Mathf.Lerp(from, to, t / dur);
            yield return null;
        }
        if (src != null && !isDestroyed) src.volume = to;
    }

    private IEnumerator SmoothAudio(AudioSource src, float tPitch, float tVol, float dur)
    {
        if (src == null) yield break;
        float sp = src.pitch, sv = src.volume;
        for (float t = 0f; t < dur; t += Time.deltaTime)
        {
            if (src == null || isDestroyed) yield break;
            float k = t / dur;
            src.pitch = Mathf.Lerp(sp, tPitch, k);
            src.volume = Mathf.Lerp(sv, tVol, k);
            yield return null;
        }
        if (src != null && !isDestroyed)
        {
            src.pitch = tPitch;
            src.volume = tVol;
        }
    }

    private static void Configure3D(AudioSource s, float mn, float mx, float dop)
    {
        s.spatialBlend = 1f;
        s.rolloffMode = AudioRolloffMode.Logarithmic;
        s.minDistance = mn;
        s.maxDistance = mx;
        s.dopplerLevel = dop;
        s.playOnAwake = false;
    }
}