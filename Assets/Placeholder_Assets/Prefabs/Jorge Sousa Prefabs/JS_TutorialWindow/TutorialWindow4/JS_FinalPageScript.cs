using UnityEngine;

public class TutorialPageEvents : MonoBehaviour
{
    [SerializeField] private TutorialManager tutorialManager;

    public void FinishTutorial()
    {
        tutorialManager.FinishTutorial();
    }
}