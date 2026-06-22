using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    // References to your settings scripts (will be assigned in each scene)
    public VolumeSlider volumeSlider;
    public ContrastSlider contrastSlider;
    public ColorBlindness colorBlindness;
    public ZoomSettings zoomSettings;
    public ControlsManager controlsManager;

    void Awake()
    {
        // Singleton pattern - only one GameManager exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}