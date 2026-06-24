using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ContrastSlider : MonoBehaviour
{
    [Header("References")]
    public Slider contrastSlider;
    public Volume postProcessVolume;
    
    private ColorAdjustments colorAdjustments;
    private const string ContrastKey = "Contrast";

    void Start()
    {
        // Find slider if not assigned
        if (contrastSlider == null)
            contrastSlider = GetComponent<Slider>();

        // Find volume if not assigned
        FindPostProcessVolume();
        
        // Try to get Color Adjustments
        if (postProcessVolume != null && postProcessVolume.profile.TryGet(out colorAdjustments))
        {
            // Load saved value
            float savedValue = PlayerPrefs.GetFloat(ContrastKey, 0f);
            
            // Set slider
            if (contrastSlider != null)
                contrastSlider.value = savedValue;
            
            // Apply
            ApplyContrast(savedValue);
        }
        
        // Add listener
        if (contrastSlider != null)
            contrastSlider.onValueChanged.AddListener(OnSliderChanged);
    }

    // Call this when scene changes to find new volume
    public void RefreshSceneReferences()
    {
        FindPostProcessVolume();
        
        // Re-apply current contrast to the new volume
        float currentContrast = PlayerPrefs.GetFloat(ContrastKey, 0f);
        
        if (postProcessVolume != null && postProcessVolume.profile.TryGet(out colorAdjustments))
        {
            ApplyContrast(currentContrast);
        }
    }

    void FindPostProcessVolume()
    {
        // Option 1: Find by tag (recommended - tag your Global Volume in each scene)
        GameObject volumeObj = GameObject.FindGameObjectWithTag("GlobalVolume");
        if (volumeObj != null)
        {
            postProcessVolume = volumeObj.GetComponent<Volume>();
            return;
        }

        // Option 2: Find first volume in scene
        postProcessVolume = FindFirstObjectByType<Volume>();
        
        // Option 3: Try to find camera's volume
        if (postProcessVolume == null)
        {
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                postProcessVolume = mainCamera.GetComponent<Volume>();
            }
        }
    }

    void OnSliderChanged(float sliderValue)
    {
        ApplyContrast(sliderValue);
    }

    void ApplyContrast(float value)
    {
        if (colorAdjustments != null)
        {
            // Make sure the override is enabled
            colorAdjustments.contrast.overrideState = true;
            
            // Apply the value
            colorAdjustments.contrast.value = value;
        }
        
        // Save regardless (so it persists)
        PlayerPrefs.SetFloat(ContrastKey, value);
        PlayerPrefs.Save();
    }

    public void LoadDefault()
    {
        float defaultValue = 0f;
        
        if (contrastSlider != null)
            contrastSlider.value = defaultValue;
        
        ApplyContrast(defaultValue);
        
        Debug.Log("Contrast reset to default");
    }

    // Clean up listener when destroyed
    void OnDestroy()
    {
        if (contrastSlider != null)
            contrastSlider.onValueChanged.RemoveListener(OnSliderChanged);
    }
}