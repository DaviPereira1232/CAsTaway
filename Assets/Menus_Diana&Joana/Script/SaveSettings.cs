using UnityEngine;

public class SettingsPanel : MonoBehaviour
{
    public VolumeSlider volumeSlider; //volume slider for the music
    public ContrastSlider contrastSlider; //slider for the contrast
    public ColorBlindness colorBlindness; //different filters for color blindness 
    public ZoomSettings zoomSettings; //on & off button for zoom in or zoom out
    public ControlsManager controlsManager; // controls input manager
    public MouseSensitivity mouseSensitivityController; //mouse sensitivity

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

        // Reset mouse Sensitivity
        PlayerPrefs.DeleteKey("MouseSensitivity");
        if (mouseSensitivityController != null)
        {
            mouseSensitivityController.ResetToDefault();
        }

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