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
            audioMixer.SetFloat("Music", Mathf.Log10(volumeSlider.value) * 20);
        } else {
            volumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
            audioMixer.SetFloat("SFX", Mathf.Log10(volumeSlider.value) * 20);
        }
    }

    private void OnSliderChanged(float value)
    {
        if (isBackgroundMusic)
        {
             PlayerPrefs.SetFloat("MusicVolume", value);
            audioMixer.SetFloat("Music", Mathf.Log10(value) * 20);
             Debug.Log("Volume Background Music set to: " + value);
        } else {
            PlayerPrefs.SetFloat("SFXVolume", value);   
            audioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
            Debug.Log("Volume SFX set to: " + value);
        }
    }

    public void LoadDefault()
    {
        if (isBackgroundMusic)
        {
            PlayerPrefs.SetFloat("MusicVolume", 1f);
            audioMixer.SetFloat("Music", Mathf.Log10(1f) * 20);
        } else {
            PlayerPrefs.SetFloat("SFXVolume", 1f);
            audioMixer.SetFloat("SFX", Mathf.Log10(1f) * 20);
        }
    }
}
