using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivity : MonoBehaviour
{
    public Slider sensitivitySlider;
    private float minSensitivity = 0.1f;
    private float maxSensitivity = 1f;
    private float defaultSensitivity = 0.5f;

    public static float MouseSensitivityControls {get; private set; } = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

        Debug.Log("Mouse sensitivity is getting called");

    if (sensitivitySlider == null)
        {
            Debug.LogError("Sensitivity slider is not assigned in the inspector");
            return;
        }

            sensitivitySlider.minValue = minSensitivity;
            sensitivitySlider.maxValue = maxSensitivity;
            
            // Load saved sensitivity or use default
            float savedSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", defaultSensitivity);
            sensitivitySlider.value = savedSensitivity;
            MouseSensitivityControls = savedSensitivity;
            
            sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);

            Debug.Log($"Mouse Sensitivity initialized. slider value: {sensitivitySlider.value}");
        
    }    

    // Update is called once per frame
    private void OnSensitivityChanged(float value)
    {
        Debug.Log($"Mouse Sensitivity set to: {value}");
        MouseSensitivityControls = value;
        PlayerPrefs.SetFloat("MouseSensitivity", value);
        
    }

    public void LoadSavedSensitivity()
    {
        float savedSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
        // Apply the saved sensitivity to your slider/controller
        // ... your existing code to update UI and apply sensitivity
        
        Debug.Log($"Mouse sensitivity loaded: {savedSensitivity}");
    }

    public void ResetToDefault()
    {
        // Reset to default sensitivity
        PlayerPrefs.SetFloat("MouseSensitivity", 1f);
        PlayerPrefs.Save();
        LoadSavedSensitivity(); // Reload after reset
    }
}
