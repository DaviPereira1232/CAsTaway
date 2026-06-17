using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPanelManager : MonoBehaviour
{
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
        SceneManager.LoadScene("Teste");
    }
}
