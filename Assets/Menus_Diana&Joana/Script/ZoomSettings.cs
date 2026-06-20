using UnityEngine;
using UnityEngine.UI;

public class ZoomSettings : MonoBehaviour
{
    [Header("Main Camera")]
    public Camera mainCamera;

    [Header("UI Elements")]
    public Button onButton;
    public Button offButton;

    void Start()
    {
        // Add listeners to the buttons
        onButton.onClick.AddListener(ZoomIn);
        offButton.onClick.AddListener(ZoomOut);
    }

    void ZoomIn()
    {
        mainCamera.fieldOfView = 30f; // Zoom in
        Debug.Log("Zoom In: Field of View set to 30");
    }

    void ZoomOut()
    {
        mainCamera.fieldOfView = 60f; // Zoom out
        Debug.Log("Zoom Out: Field of View set to 60");
    }
}