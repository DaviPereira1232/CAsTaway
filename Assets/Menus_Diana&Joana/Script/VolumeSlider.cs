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
        LoadSavedVolume();
        volumeSlider.onValueChanged.AddListener(OnSliderChanged);
    }

    private void LoadSavedVolume()
    {
        if (isBackgroundMusic)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
            SetMusicVolume(volumeSlider.value);
        } else {
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
            
        } else {
            PlayerPrefs.SetFloat("SFXVolume", value);
            SetSFXVolume(value);
        }
    }
    private void SetMusicVolume(float value)
    {
        float dB = value > 0.0001f ? Mathf.Log10(value) * 20 : -80f;
        audioMixer.SetFloat("Music", dB);
        Debug.Log($"Music Volume: {value} -> {dB}dB");
    }
    private void SetSFXVolume(float value)
    {
        float dB = value > 0.0001f ? Mathf.Log10(value) * 20 : -80f;
        audioMixer.SetFloat("SFX", dB);
        Debug.Log($"SFX Volume: {value} -> {dB}dB");  
    }

    public void LoadDefault()
    {
        if (isBackgroundMusic)
        {
            PlayerPrefs.SetFloat("MusicVolume", 1f);
            SetMusicVolume(1f);
            volumeSlider.value = 1f;
        } else {
            PlayerPrefs.SetFloat("SFXVolume", 1f);
            SetSFXVolume(1f);
            volumeSlider.value = 1f;
        }
    }
}
