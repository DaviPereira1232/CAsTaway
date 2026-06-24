using UnityEngine;

public class SaveSettings : MonoBehaviour
{
    public VolumeSlider volumeSlider;
    public ContrastSlider contrastSlider;
    public ColorBlindness colorBlindness;
    public ZoomSettings zoomSettings;
    public ControlsManager controlsManager;
    public MouseSensitivity mouseSensitivityController;

    public void OnYesClicked()
    {
        // Save all settings
        if (GameManager.Instance != null)
            GameManager.Instance.SaveAllSettings();
    }

    public void OnNoClicked()
    {
        // Use GameManager's reset if available, otherwise fall back to local reset
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetAllSettings();
        }
        else
        {
            LocalReset();
        }
    }

    void LocalReset()
    {
        PlayerPrefs.DeleteKey("MusicVolume");
        volumeSlider.LoadDefault();

        PlayerPrefs.DeleteKey("Contrast");
        contrastSlider.LoadDefault();

        PlayerPrefs.DeleteKey("ColorBlindnessType");
        colorBlindness.LoadDefault();

        PlayerPrefs.DeleteKey(ZoomSettings.zoomPrefKey);
        zoomSettings.LoadZoomState();

        DeleteControlKeys();
        controlsManager.ResetToDefaults();

        PlayerPrefs.DeleteKey("MouseSensitivity");
        if (mouseSensitivityController != null)
            mouseSensitivityController.ResetToDefault();

        PlayerPrefs.Save();
    }

    void DeleteControlKeys()
    {
        PlayerPrefs.DeleteKey("UpKey");
        PlayerPrefs.DeleteKey("DownKey");
        PlayerPrefs.DeleteKey("LeftKey");
        PlayerPrefs.DeleteKey("RightKey");
        PlayerPrefs.DeleteKey("CaptureKey");
    }
}