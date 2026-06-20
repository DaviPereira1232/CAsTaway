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
    
    // Each type has: [hueShift, saturation]
    // These are approximations - adjust for your specific game
    private float[,] settings = {
        { 0f, 0f },     // Normal: no change
        { 30f, -20f },  // Deuteranopia: green deficiency, slight desaturation
        { -30f, -20f }, // Protanopia: red deficiency, slight desaturation
        { 180f, -10f }  // Tritanopia: blue-yellow deficiency
    };

    void Start()
    {
        if (postProcessVolume == null || !postProcessVolume.profile.TryGet(out colorAdjustments))
        {
            return;
        }
        
        // Load saved
        currentIndex = PlayerPrefs.GetInt("ColorBlindnessType", 0);
        colorblindnessText.text = types[currentIndex];
        ApplyColorBlindness(currentIndex);
        
        leftArrow.onClick.AddListener(() => ChangeType(-1));
        rightArrow.onClick.AddListener(() => ChangeType(1));
    }

    void ChangeType(int direction)
    {
        currentIndex = (currentIndex + direction + types.Length) % types.Length;
        colorblindnessText.text = types[currentIndex];
        ApplyColorBlindness(currentIndex);
        PlayerPrefs.SetInt("ColorBlindnessType", currentIndex);
        PlayerPrefs.Save();
    }

    void ApplyColorBlindness(int index)
    {
        if (colorAdjustments == null) return;
        
        // Enable overrides
        colorAdjustments.hueShift.overrideState = true;
        colorAdjustments.saturation.overrideState = true;
        
        // Apply settings
        colorAdjustments.hueShift.value = settings[index, 0];
        colorAdjustments.saturation.value = settings[index, 1];
    }
}