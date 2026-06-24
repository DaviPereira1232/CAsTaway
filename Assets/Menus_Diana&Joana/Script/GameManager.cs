using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }
    // References to your settings managers (assign in inspector or find automatically)
    [Header("Settings References")]
    public VolumeSlider volumeSlider;
    public ContrastSlider contrastSlider;
    public ColorBlindness colorBlindness;
    public ZoomSettings zoomSettings;
    public ControlsManager controlsManager;
    public MouseSensitivity mouseSensitivityController;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // If these aren't assigned in inspector, try to find them
        FindSettingReferences();
        
        // Load all PlayerPrefs through the settings managers
        LoadAllSettings();
    }

    // Called when loading a new scene
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // After scene loads, re-find references and reapply settings
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindSettingReferences();
        LoadAllSettings();
    }

        private T FindComponent<T>() where T : Component
    {
#if UNITY_2023_1_OR_NEWER
        return FindFirstObjectByType<T>();
#else
        return FindObjectOfType<T>();
#endif
    }

    void FindSettingReferences()
    {
        // Auto-find if not assigned
        if (volumeSlider == null)
            volumeSlider = FindComponent<VolumeSlider>();
        if (contrastSlider == null)
            contrastSlider = FindComponent<ContrastSlider>();
        if (colorBlindness == null)
            colorBlindness = FindComponent<ColorBlindness>();
        if (zoomSettings == null)
            zoomSettings = FindComponent<ZoomSettings>();
        if (controlsManager == null)
            controlsManager = FindComponent<ControlsManager>();
        if (mouseSensitivityController == null)
            mouseSensitivityController = FindComponent<MouseSensitivity>();
    }

    // Tell all settings managers to load their values from PlayerPrefs
    public void LoadAllSettings()
    {
        Debug.Log("Loading all settings from PlayerPrefs...");

        if (volumeSlider != null)
            volumeSlider.LoadDefault(); // Assuming LoadDefault loads from PlayerPrefs

        if (contrastSlider != null)
            contrastSlider.LoadDefault();

        if (colorBlindness != null)
            colorBlindness.LoadDefault();

        if (zoomSettings != null)
            zoomSettings.LoadZoomState();

        if (controlsManager != null)
            controlsManager.LoadSavedControls();

        if (mouseSensitivityController != null)
            mouseSensitivityController.LoadSavedSensitivity();
    }

    // Save all current settings to PlayerPrefs
    public void SaveAllSettings()
    {
        PlayerPrefs.Save();
        Debug.Log("All settings saved!");
    }

    // Method to reset everything (calls your existing OnNoClicked logic)
    public void ResetAllSettings()
    {
        if (volumeSlider != null)
        {
            PlayerPrefs.DeleteKey("MusicVolume");
            volumeSlider.LoadDefault();
        }

        if (contrastSlider != null)
        {
            PlayerPrefs.DeleteKey("Contrast");
            contrastSlider.LoadDefault();
        }

        if (colorBlindness != null)
        {
            PlayerPrefs.DeleteKey("ColorBlindnessType");
            colorBlindness.LoadDefault();
        }

        if (zoomSettings != null)
        {
            PlayerPrefs.DeleteKey(ZoomSettings.zoomPrefKey);
            zoomSettings.LoadZoomState();
        }

        if (controlsManager != null)
        {
            DeleteControlKeys();
            controlsManager.ResetToDefaults();
        }

        if (mouseSensitivityController != null)
        {
            PlayerPrefs.DeleteKey("MouseSensitivity");
            mouseSensitivityController.ResetToDefault();
        }

        PlayerPrefs.Save();
        Debug.Log("All settings reset to defaults!");
    }

    void DeleteControlKeys()
    {
        PlayerPrefs.DeleteKey("UpKey");
        PlayerPrefs.DeleteKey("DownKey");
        PlayerPrefs.DeleteKey("LeftKey");
        PlayerPrefs.DeleteKey("RightKey");
        PlayerPrefs.DeleteKey("CaptureKey");
    }

    // Scene loading helper
    public void LoadScene(string sceneName)
    {
        SaveAllSettings();
        SceneManager.LoadScene(sceneName);
    }

    private void OnApplicationQuit()
    {
        SaveAllSettings();
    }
}
