using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorBlindness : MonoBehaviour
{
    [Header("UI")]
    public Button leftArrow;
    public Button rightArrow;
    public TMP_Text colorblindnessText;
    
    [Header("Volume Reference")]
    public Volume postProcessVolume;
    
    private string[] types = {"Trichromacy", "Deuteranopia", "Protanopia", "Tritanopia"};
    
    private int currentIndex = 0;
    private ColorAdjustments colorAdjustments;
    
    private float[,] settings = {
        { 0f, 0f },     // Normal: no change
        { 30f, -20f },  // Deuteranopia: green deficiency
        { -30f, -20f }, // Protanopia: red deficiency
        { 180f, -10f }  // Tritanopia: blue-yellow deficiency
    };

    void Start()
    {
        // Find volume if not assigned
        FindPostProcessVolume();
        
        if (postProcessVolume == null || !postProcessVolume.profile.TryGet(out colorAdjustments))
        {
            Debug.LogWarning("ColorBlindness: No post process volume found!");
            return;
        }
        
        // Load saved
        currentIndex = PlayerPrefs.GetInt("ColorBlindnessType", 0);
        if (colorblindnessText != null)
            colorblindnessText.text = types[currentIndex];
        ApplyColorBlindness(currentIndex);
        
        // Add button listeners
        if (leftArrow != null)
            leftArrow.onClick.AddListener(() => ChangeType(-1));
        if (rightArrow != null)
            rightArrow.onClick.AddListener(() => ChangeType(1));
    }

    // Call this when scene changes to find new volume
    public void RefreshSceneReferences()
    {
        FindPostProcessVolume();
        
        // Re-apply current color blindness setting to the new volume
        if (postProcessVolume != null && postProcessVolume.profile.TryGet(out colorAdjustments))
        {
            ApplyColorBlindness(currentIndex);
        }
    }

    void FindPostProcessVolume()
    {
        // Option 1: Find by tag
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

    void ChangeType(int direction)
    {
        currentIndex = (currentIndex + direction + types.Length) % types.Length;
        if (colorblindnessText != null)
            colorblindnessText.text = types[currentIndex];
        ApplyColorBlindness(currentIndex);
        PlayerPrefs.SetInt("ColorBlindnessType", currentIndex);
        PlayerPrefs.Save();
    }

    void ApplyColorBlindness(int index)
    {
        if (colorAdjustments == null) return;
        
        colorAdjustments.hueShift.overrideState = true;
        colorAdjustments.saturation.overrideState = true;
        
        colorAdjustments.hueShift.value = settings[index, 0];
        colorAdjustments.saturation.value = settings[index, 1];
    }

    public void LoadDefault()
    {
        currentIndex = 0;  // back to Trichromacy (normal)
        if (colorblindnessText != null)
            colorblindnessText.text = types[currentIndex];
        ApplyColorBlindness(currentIndex);
    }

    // Clean up listeners when destroyed
    void OnDestroy()
    {
        if (leftArrow != null)
            leftArrow.onClick.RemoveAllListeners();
        if (rightArrow != null)
            rightArrow.onClick.RemoveAllListeners();
    }
}