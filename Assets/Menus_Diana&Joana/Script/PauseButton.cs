using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{   
    public void OnPauseButtonClicked()
    {
        PauseGame();
    }
    
    private void PauseGame()
    {
        // Freeze the game
        Time.timeScale = 0f;
    }

    public void LoadPauseMenu(int buildIndex)
    {
        // Set flag to show Pause Menu
        PlayerPrefs.SetInt("ShowMenu", 1); // 1 = Pause Menu
        PlayerPrefs.Save();
        
        // Load the menus scene
        SceneManager.LoadScene(buildIndex);
    }
}