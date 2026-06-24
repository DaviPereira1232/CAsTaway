using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Add this for IEnumerator

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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
        FindSettingReferences();
        LoadAllSettings();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // This is the ONLY OnSceneLoaded method - it handles everything
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(InitializeSceneSettings());
    }

    // Helper method that handles both old and new Unity versions
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

    IEnumerator InitializeSceneSettings()
    {
    // Wait one frame for all scene objects to initialize
    yield return null;
    
    FindSettingReferences();
    
    // Refresh scene-specific references and log results
    if (contrastSlider != null)
    {
        Debug.Log("Refreshing ContrastSlider references...");
        contrastSlider.RefreshSceneReferences();
    }
    else
    {
        Debug.Log("ContrastSlider not found in scene");
    }
    
    if (volumeSlider != null)
    {
        Debug.Log("Refreshing VolumeSlider references...");
        volumeSlider.RefreshSceneReferences();
    }
    else
    {
        Debug.Log("VolumeSlider not found in scene");
    }
    
    if (colorBlindness != null)
    {
        Debug.Log("Refreshing ColorBlindness references...");
        colorBlindness.RefreshSceneReferences();
    }
    else
    {
        Debug.Log("ColorBlindness not found in scene");
    }
    
    if (zoomSettings != null)
    {
        Debug.Log($"Refreshing ZoomSettings references... Current zoom state: {PlayerPrefs.GetInt(ZoomSettings.zoomPrefKey, 0)}");
        zoomSettings.RefreshSceneReferences();
    }
    else
    {
        Debug.Log("ZoomSettings not found in scene");
    }
    
    LoadAllSettings();
    
    Debug.Log("Scene references refreshed for: " + SceneManager.GetActiveScene().name);
    }

    public void LoadAllSettings()
    {
        Debug.Log("Loading all settings from PlayerPrefs...");

        if (volumeSlider != null)
            volumeSlider.LoadDefault();

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

    public void SaveAllSettings()
    {
        PlayerPrefs.Save();
        Debug.Log("All settings saved!");
    }

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
            PlayerPrefs.DeleteKey("UpKey");
            PlayerPrefs.DeleteKey("DownKey");
            PlayerPrefs.DeleteKey("LeftKey");
            PlayerPrefs.DeleteKey("RightKey");
            PlayerPrefs.DeleteKey("CaptureKey");
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