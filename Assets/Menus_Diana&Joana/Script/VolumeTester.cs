using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeTester : MonoBehaviour
{
    void Start()
    {
        // Find all volumes in scene
        Volume[] volumes = FindObjectsByType<Volume>(FindObjectsSortMode.None);
        Debug.Log($"Found {volumes.Length} volumes in scene");
        
        foreach (Volume vol in volumes)
        {
            Debug.Log($"Volume: {vol.gameObject.name}, IsGlobal: {vol.isGlobal}, Priority: {vol.priority}");
            
            if (vol.profile != null)
            {
                ColorAdjustments ca;
                if (vol.profile.TryGet(out ca))
                {
                    Debug.Log($"  - Has ColorAdjustments: contrast={ca.contrast.value}");
                }
                else
                {
                    Debug.Log("  - Missing ColorAdjustments override!");
                }
            }
        }
    }
}