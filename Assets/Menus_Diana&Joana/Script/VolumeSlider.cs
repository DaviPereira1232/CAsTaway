using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private bool isBackgroundMusic = true;

    private void Start()
    {
        // Find slider if not assigned
        if (volumeSlider == null)
            volumeSlider = GetComponent<Slider>();
        
        // Find audio mixer if not assigned
        if (audioMixer == null)
            FindAudioMixer();
        
        LoadSavedVolume();
        
        if (volumeSlider != null)
            volumeSlider.onValueChanged.AddListener(OnSliderChanged);
    }

    // Call this when scene changes
    public void RefreshSceneReferences()
    {
        // Find audio mixer if lost
        if (audioMixer == null)
            FindAudioMixer();
        
        // Re-apply saved volume
        LoadSavedVolume();
    }

    void FindAudioMixer()
    {
        // Try loading from Resources folder
        audioMixer = Resources.Load<AudioMixer>("MasterMixer");
        
        if (audioMixer == null)
        {
            Debug.LogWarning("VolumeSlider: AudioMixer not found in Resources/MasterMixer");
        }
    }

    private void LoadSavedVolume()
    {
        if (volumeSlider == null) return;
        
        if (isBackgroundMusic)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
            SetMusicVolume(volumeSlider.value);
        }
        else
        {
            volumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
            SetSFXVolume(volumeSlider.value);
        }
    }

    private void OnSliderChanged(float value)
    {
        if (isBackgroundMusic)
        {
            PlayerPrefs.SetFloat("MusicVolume", value);
            SetMusicVolume(value);
        }
        else
        {
            PlayerPrefs.SetFloat("SFXVolume", value);
            SetSFXVolume(value);
        }
    }

    private void SetMusicVolume(float value)
    {
        if (audioMixer == null) return;
        
        float dB = value > 0.0001f ? Mathf.Log10(value) * 20 : -80f;
        audioMixer.SetFloat("Music", dB);
    }

    private void SetSFXVolume(float value)
    {
        if (audioMixer == null) return;
        
        float dB = value > 0.0001f ? Mathf.Log10(value) * 20 : -80f;
        audioMixer.SetFloat("SFX", dB);
    }

    public void LoadDefault()
    {
        if (volumeSlider == null)
            volumeSlider = GetComponent<Slider>();
        
        if (isBackgroundMusic)
        {
            PlayerPrefs.SetFloat("MusicVolume", 1f);
            SetMusicVolume(1f);
            if (volumeSlider != null)
                volumeSlider.value = 1f;
        }
        else
        {
            PlayerPrefs.SetFloat("SFXVolume", 1f);
            SetSFXVolume(1f);
            if (volumeSlider != null)
                volumeSlider.value = 1f;
        }
        
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        if (volumeSlider != null)
            volumeSlider.onValueChanged.RemoveListener(OnSliderChanged);
    }
}