using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        LoadSavedVolume();
        volumeSlider.onValueChanged.AddListener(OnSliderChanged);
    }

    private void LoadSavedVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        audioMixer.SetFloat("Music", Mathf.Log10(volumeSlider.value) * 20);
    }

    private void OnSliderChanged(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        audioMixer.SetFloat("Music", Mathf.Log10(value) * 20);
    }

    public void LoadDefault()
    {
        volumeSlider.value = 1f;  // your default
        audioMixer.SetFloat("Music", Mathf.Log10(1f) * 20);
    }
}
