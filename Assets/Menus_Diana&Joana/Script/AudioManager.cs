using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("----Audio Sources----")]
    [SerializeField] private AudioSource musicSource;
    //[SerializeField] private AudioSource sfxSource;

    [Header("----SFX Pool----")]
    //[SerializeField] private GameObject sfxSourcePrefab;
    //[SerializeField] private int sfxPoolSize =
    [Header("----Audio Clips----")]

    [SerializeField] private AudioClip background;

    [Header("Level Audio")]

    [SerializeField] private AudioClip damage;
    [SerializeField] private AudioClip bubbles;
    [SerializeField] private AudioClip waterswosh;
    [SerializeField] private AudioClip success;
    [SerializeField] private AudioClip partialSucess;
    [SerializeField] private AudioClip failure;
    [SerializeField] private AudioClip scaredCat;
    [SerializeField] private AudioClip happyCat;
    [SerializeField] private AudioClip unwellCat;

    [Header("Main Menu Audio")]

    [SerializeField] private AudioClip computer;
    [SerializeField] private AudioClip exitingSpaceship;
    [SerializeField] private AudioClip catSleeping;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
}
