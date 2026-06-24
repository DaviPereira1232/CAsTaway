using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject pauseMenuPanel;
    
    private GameObject returnPanel; // Track where to go back to
    
    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void TogglePanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }

    public void Jogar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Teste");
    }
    
    // Call this when opening Options from Main Menu
    public void SetReturnToMainMenu()
    {
        returnPanel = mainMenuPanel;
        mainMenuPanel.SetActive(false); // Hide Main Menu
    }
    
    // Call this when opening Options from Pause Menu
    public void SetReturnToPauseMenu()
    {
        returnPanel = pauseMenuPanel;
        pauseMenuPanel.SetActive(false); // Hide Pause Menu
    }
    
    // Call this on the YES button
    public void ReturnToPreviousMenu()
    {
        if (returnPanel != null)
        {
            returnPanel.SetActive(true); // Show the correct menu
        }
    }
}