using UnityEngine;
using UnityEngine.UI;

public class ZoomSettings : MonoBehaviour
{
    [Header("Main Camera")]
    public Camera mainCamera;

    [Header("UI Elements")]
    public Button onButton;
    public Button offButton;

    public const string zoomPrefKey = "ZoomState";
    private const int zoomIn = 1;
    private const int zoomOut = 0;

    void Start()
    {
        Debug.Log($"ZoomSettings Start called in scene: {UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}");
        
        // Find camera if not assigned
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            Debug.Log($"Camera auto-found: {mainCamera != null}");
        }
        
        // Add button listeners
        if (onButton != null)
            onButton.onClick.AddListener(ZoomIn);
        if (offButton != null)
            offButton.onClick.AddListener(ZoomOut);
        
        LoadZoomState();
    }

    public void RefreshSceneReferences()
    {
        Debug.Log("ZoomSettings.RefreshSceneReferences called");
        
        // Find the camera in the new scene
        mainCamera = Camera.main;
        
        if (mainCamera == null)
        {
            Debug.LogError("ZoomSettings: No main camera found in scene!");
            return;
        }
        
        Debug.Log($"Camera found: {mainCamera.name}, Current FOV: {mainCamera.fieldOfView}");
        
        // Re-apply current zoom state to the new camera
        LoadZoomState();
    }

    void ZoomIn()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("Cannot zoom in: no camera found!");
                return;
            }
        }
        
        Debug.Log($"Zooming In - FOV: 30");
        mainCamera.fieldOfView = 30f;
        PlayerPrefs.SetInt(zoomPrefKey, zoomIn);
        PlayerPrefs.Save();
        Debug.Log($"Zoom state saved as: {PlayerPrefs.GetInt(zoomPrefKey)}");
    }

    void ZoomOut()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("Cannot zoom out: no camera found!");
                return;
            }
        }
        
        Debug.Log($"Zooming Out - FOV: 60");
        mainCamera.fieldOfView = 60f;
        PlayerPrefs.SetInt(zoomPrefKey, zoomOut);
        PlayerPrefs.Save();
        Debug.Log($"Zoom state saved as: {PlayerPrefs.GetInt(zoomPrefKey)}");
    }

    public void LoadZoomState()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("Cannot load zoom state: no camera found!");
                return;
            }
        }
        
        int zoomState = PlayerPrefs.GetInt(zoomPrefKey, zoomOut);
        Debug.Log($"Loading zoom state: {zoomState} (0=Out, 1=In)");
        
        if (zoomState == zoomIn)
        {
            mainCamera.fieldOfView = 30f;
            Debug.Log($"Applied zoom IN - FOV: {mainCamera.fieldOfView}");
        }
        else
        {
            mainCamera.fieldOfView = 60f;
            Debug.Log($"Applied zoom OUT - FOV: {mainCamera.fieldOfView}");
        }
    }

    void OnDestroy()
    {
        if (onButton != null)
            onButton.onClick.RemoveListener(ZoomIn);
        if (offButton != null)
            offButton.onClick.RemoveListener(ZoomOut);
    }
}