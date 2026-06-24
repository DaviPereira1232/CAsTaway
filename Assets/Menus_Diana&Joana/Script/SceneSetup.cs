using UnityEngine;
using UnityEngine.EventSystems;

public class SceneSetup : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject optionsPanel;
    
    private string returnToMenu = "Main"; // Default return location
    
    void Start()
    {
        // Disable duplicate EventSystem
        EventSystem[] eventSystems = FindObjectsByType<EventSystem>(FindObjectsSortMode.None);
        if (eventSystems.Length > 1)
        {
            for (int i = 1; i < eventSystems.Length; i++)
            {
                eventSystems[i].gameObject.SetActive(false);
            }
        }
        
        int menuToShow = PlayerPrefs.GetInt("ShowMenu", 0);
        
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        
        switch (menuToShow)
        {
            case 0: // Main Menu
                mainMenuPanel.SetActive(true);
                returnToMenu = "Main";
                Time.timeScale = 1f;
                break;
                
            case 1: // Pause Menu
                pauseMenuPanel.SetActive(true);
                returnToMenu = "Pause"; // Set return to Pause
                Time.timeScale = 0f;
                break;
                
            case 2: // Options
                optionsPanel.SetActive(true);
                break;
        }
        
        PlayerPrefs.SetInt("ShowMenu", 0);
        PlayerPrefs.Save();
    }
    
    // Called when opening Options
    public void OpenOptions()
    {
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
        // returnToMenu stays as whatever it was set to
    }
    
    // Called when clicking Exit/Back from Options
    public void ExitOptions()
    {
        optionsPanel.SetActive(false);
        
        if (returnToMenu == "Pause")
        {
            pauseMenuPanel.SetActive(true);
        }
        else
        {
            mainMenuPanel.SetActive(true);
        }
    }
}