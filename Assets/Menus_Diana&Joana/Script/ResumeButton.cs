using UnityEngine;
using UnityEngine.SceneManagement;

public class ResumeButton : MonoBehaviour
{
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void UnLoadScreen(int buildIndex)
    {
        SceneManager.UnloadSceneAsync(buildIndex);
    }
}