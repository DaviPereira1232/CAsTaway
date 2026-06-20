using UnityEngine;

public class SettingsPanel : MonoBehaviour
{
    public VolumeSlider volumeSlider;
    public ContrastSlider contrastSlider;
    public ColorBlindness colorBlindness;  // Add this

    public void OnYesClicked()
    {
        // Changes stay
    }

    public void OnNoClicked()
    {  
        // Reset volume
        PlayerPrefs.DeleteKey("MusicVolume");
        volumeSlider.LoadDefault();

        // Reset contrast
        PlayerPrefs.DeleteKey("Contrast");
        contrastSlider.LoadDefault();

        // Reset color blindness
        PlayerPrefs.DeleteKey("ColorBlindnessType");
        colorBlindness.LoadDefault();

        // Save all deletions at once
        PlayerPrefs.Save();
    }
}