using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("----Audio Sources----")]
    [SerializeField] private AudioSource musicSource;
    //[SerializeField] private AudioSource sfxSource;
    [Header("----Audio Mixer----")]
    [SerializeField] private AudioMixer mixer;

    [Header("----SFX Pool----")]
    //[SerializeField] private GameObject sfxSourcePrefab;
    [SerializeField] private int sfxPoolSize = 17;
    private List<AudioSource> sfxPool = new List<AudioSource>();
    private AudioMixerGroup sfxMixerGroup;

    [Header("----Audio Clips----")]
    [SerializeField] private AudioClip background;

    [Header("Level Audio")]
    [SerializeField] private AudioClip damage;
    [SerializeField] private AudioClip bubbles;
    [SerializeField] private AudioClip waterswosh;

    [SerializeField] private AudioClip success;
    [SerializeField] private AudioClip partialSucess;
    [SerializeField] private AudioClip scaredCat;
    [SerializeField] private AudioClip happyCat;
    [SerializeField] private AudioClip unwellCat;

    [Header("Main Menu Audio")]

    [SerializeField] private AudioClip computer;
    [SerializeField] private AudioClip exitingSpaceship;
    [SerializeField] private AudioClip catSleeping;

    [Header ("Glub Glubs")]

    [SerializeField] private AudioClip glubAnna;
    [SerializeField] private AudioClip glubDavi;
    [SerializeField] private AudioClip glubDiana;
    [SerializeField] private AudioClip glubJoana;
    [SerializeField] private AudioClip glubJoão;
    [SerializeField] private AudioClip glubJorge;
    [SerializeField] private AudioClip glubLeandro;

    private List<AudioClip> glubClips;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSfxPool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeSfxPool()
    {
        // Instead of loading from Resources, assign via Inspector
        // Find the SFX mixer group
        if (mixer != null)  // Use your mixer variable name here
        {
            sfxMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        }

        // Create pool of AudioSources
        for (int i = 0; i < sfxPoolSize; i++)
        {
            GameObject sfxObj = new GameObject($"SFX_Source_{i}");
            sfxObj.transform.parent = transform;
            
            AudioSource source = sfxObj.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.spatialBlend = 0f;
            source.outputAudioMixerGroup = sfxMixerGroup;
            
            sfxPool.Add(source);
        }
    }

    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();

        InitializeGlubClips();
    }

    // Play a sound effect - won't overlap because it uses the pool
    public void PlaySFX(AudioClip clip, float volume = 1f, Vector3? position = null, float pitch = 1f)
    {
        if (clip == null) return;

        AudioSource availableSource = GetAvailableSFXSource();
        if (availableSource == null) return;

        if (position.HasValue)
        {
            availableSource.transform.position = position.Value;
            availableSource.spatialBlend = 1f; // 3D sound
        }
        else
        {
            availableSource.spatialBlend = 0f; // 2D sound
        }

        availableSource.pitch = pitch;
        availableSource.PlayOneShot(clip, volume);
    }

    // Find an available source that isn't playing
    private AudioSource GetAvailableSFXSource()
    {
        // First, try to find a source that's not playing
        foreach (AudioSource source in sfxPool)
        {
            if (!source.isPlaying)
                return source;
        }
        //Cut off oldest sound
        AudioSource oldestSource = sfxPool[0];
        float oldestTime = float.MaxValue;
        
        foreach (AudioSource source in sfxPool)
        {
            if (source.time < oldestTime)
            {
                oldestTime = source.time;
                oldestSource = source;
            }
        }
        
        Debug.LogWarning("All SFX sources in use! Cutting off oldest sound.");
        return oldestSource;
    }

    private void InitializeGlubClips()
    {
        glubClips = new List<AudioClip>
        {
            glubAnna,
            glubDavi,
            glubDiana,
            glubJoana,
            glubJoão,
            glubJorge,
            glubLeandro
        };
    }

    public void PlayRandomGlubSound(float volume = 1f, Vector3? position = null, float pitch = 1f)
    {
        if (glubClips == null || glubClips.Count == 0)
        {
            Debug.LogWarning("No glub clips available!");
            return;
        }
        
        int randomIndex = Random.Range(0, glubClips.Count);
        AudioClip randomGlub = glubClips[randomIndex];
        
        PlaySFX(randomGlub, volume, position, pitch);
    }

    // Convenience methods for your specific sounds
    public void PlayDamage() => PlaySFX(damage, 0.1f);
    public void PlayBubbles(float volume = 1f) => PlaySFX(bubbles, volume);
    public void PlaySuccess() => PlaySFX(success, 1f);
    public void PlayPartialSuccess() => PlaySFX(partialSucess, 1f);
    public void PlayScaredCat() => PlaySFX(scaredCat, 1f);
    public void PlayHappyCat() => PlaySFX(happyCat, 1f);
    public void PlayUnwellCat() => PlaySFX(unwellCat, 0.9f);
    public void PlayComputer() => PlaySFX(computer, 0.6f);
    public void PlayExitingSpaceship() => PlaySFX(exitingSpaceship, 0.2f);
    public void PlayCatSleeping(float volume = 0.8f) => PlaySFX(catSleeping, volume);

    // Play sounds at specific positions for 3D audio
    public void PlayBubblesAtPosition(Vector3 position) => PlaySFX(bubbles, 0.6f, position);

    public void PlayWaterSwoshAtPosition(Vector3 position) => PlaySFX(waterswosh, 0.7f, position);

    public void PlayRandomGlub() => PlayRandomGlubSound(0.8f);
    public void PlayRandomGlubAtPosition(Vector3 position) => PlayRandomGlubSound(0.8f, position);
}
