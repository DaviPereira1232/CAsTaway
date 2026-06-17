using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Subtitles : MonoBehaviour
{
    public Button leftArrow;
    public Button rightArrow;
    public TMP_Text subtitleText;
    
    private string[] languages = {"English", "Português", "Español", "Français"};
    private int currentIndex = 0;
    
    void Start()
    {
        subtitleText.text = languages[currentIndex];
        
        leftArrow.onClick.AddListener(() => ChangeLanguage(-1));
        rightArrow.onClick.AddListener(() => ChangeLanguage(1));
    }
    
    void ChangeLanguage(int direction)
    {
        // direction: -1 for previous, 1 for next
        currentIndex = (currentIndex + direction + languages.Length) % languages.Length;
        subtitleText.text = languages[currentIndex];
    }
}