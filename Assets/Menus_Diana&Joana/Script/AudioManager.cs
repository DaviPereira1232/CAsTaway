using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-----------Audio Sources-----------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("-----------Audio Clips-----------")]
    [SerializeField] private AudioClip background;
    [SerializeField] private AudioClip damage;
    [SerializeField] private AudioClip bubbles;
    [SerializeField] private AudioClip seaurchin;
    [SerializeField] private AudioClip finishlevel;

    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
}
