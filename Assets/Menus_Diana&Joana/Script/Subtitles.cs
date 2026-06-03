using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Subtitles : MonoBehaviour
{
    public Button leftArrow;
    public Button rightArrow;
    private TMP_Text subtitleText;  // Still need TMP_Text
    
    void Start()
    {
        // This works if the script is on the SAME GameObject as the TextMeshPro component
        subtitleText = GetComponent<TMP_Text>();
        
        if (subtitleText == null)
        {
            Debug.LogError("No TMP_Text component found on this GameObject!");
            return;
        }
        
        leftArrow.onClick.AddListener(() => SwitchText("English"));
        rightArrow.onClick.AddListener(() => SwitchText("Português"));
    }
    
    void SwitchText(string text)
    {
        subtitleText.text = text;
    }
}