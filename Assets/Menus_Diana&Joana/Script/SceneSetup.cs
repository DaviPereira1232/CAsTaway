using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject optionsPanel;
    
    void Start()
    {
        int menuToShow = PlayerPrefs.GetInt("ShowMenu", 0);
        
        // First, hide ALL menus
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        
        // Then show only the right one
        switch (menuToShow)
        {
            case 0: // Default - show Main Menu
                mainMenuPanel.SetActive(true);
                Time.timeScale = 1f; // Normal time for main menu
                break;
                
            case 1: // Show Pause Menu
                pauseMenuPanel.SetActive(true);
                Time.timeScale = 0f; // Freeze the game
                break;
                
            case 2: // Show Options
                optionsPanel.SetActive(true);
                break;
        }
        
        // Reset the flag
        PlayerPrefs.SetInt("ShowMenu", 0);
        PlayerPrefs.Save();
    }
}