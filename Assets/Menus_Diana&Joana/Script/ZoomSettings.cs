using UnityEngine;
using UnityEngine.UI;

public class ZoomSettings : MonoBehaviour
{
    [Header("Main Camera")]
    public Camera mainCamera;

    [Header("UI Elements")]
    public Button onButton;
    public Button offButton;

    // Make this public static so other scripts can access it
    public const string zoomPrefKey = "ZoomState";
    private const int zoomIn = 1;
    private const int zoomOut = 0;

    void Start()
    {
        onButton.onClick.AddListener(ZoomIn);
        offButton.onClick.AddListener(ZoomOut);
        LoadZoomState();
    }

    void ZoomIn()
    {
        mainCamera.fieldOfView = 30f;
        PlayerPrefs.SetInt(zoomPrefKey, zoomIn);
        PlayerPrefs.Save();
    }

    void ZoomOut()
    {
        mainCamera.fieldOfView = 60f;
        PlayerPrefs.SetInt(zoomPrefKey, zoomOut);
        PlayerPrefs.Save();
    }

    public void LoadZoomState()
    {
        int zoomState = PlayerPrefs.GetInt(zoomPrefKey, zoomOut);
        
        if (zoomState == zoomIn)
        {
            mainCamera.fieldOfView = 30f;
        }
        else
        {
            mainCamera.fieldOfView = 60f;
        }
    }
}