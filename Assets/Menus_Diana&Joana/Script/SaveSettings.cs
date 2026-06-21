using UnityEngine;

public class SettingsPanel : MonoBehaviour
{
    public VolumeSlider volumeSlider;
    public ContrastSlider contrastSlider;
    public ColorBlindness colorBlindness;  
    public ZoomSettings zoomSettings;
    public ControlsManager controlsManager; // Add this reference

    public void OnYesClicked()
    {
        // Changes stay - do nothing
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

        // Reset zoom
        PlayerPrefs.DeleteKey(ZoomSettings.zoomPrefKey);
        zoomSettings.LoadZoomState();

        // Reset controls - Delete all control keys from PlayerPrefs
        DeleteControlKeys();
        
        // Reset controls to defaults
        controlsManager.ResetToDefaults();

        // Save all deletions at once
        PlayerPrefs.Save();
    }

    void DeleteControlKeys()
    {
        // Delete all control keys from PlayerPrefs
        PlayerPrefs.DeleteKey("UpKey");
        PlayerPrefs.DeleteKey("DownKey");
        PlayerPrefs.DeleteKey("LeftKey");
        PlayerPrefs.DeleteKey("RightKey");
        PlayerPrefs.DeleteKey("CaptureKey");
    }
}