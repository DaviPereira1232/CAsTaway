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

    void Start()
    {
        
        // Check if Volume is assigned
        if (postProcessVolume == null)
        {
            return;
        }
        
        // Try to get Color Adjustments
        if (postProcessVolume.profile.TryGet(out colorAdjustments))
        {
            
            // Load saved value
            float savedValue = PlayerPrefs.GetFloat("Contrast", 0f);
            
            // Set slider
            contrastSlider.value = savedValue;
            
            // Apply
            ApplyContrast(savedValue);
        }  
        // Add listener
        contrastSlider.onValueChanged.AddListener(OnSliderChanged);

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
            
            // Save
            PlayerPrefs.SetFloat("Contrast", value);
            PlayerPrefs.Save();
        }
    }
}