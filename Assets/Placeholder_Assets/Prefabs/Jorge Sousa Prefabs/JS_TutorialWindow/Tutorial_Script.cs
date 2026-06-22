using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] tutorialPages;

    private int currentPage = 0;

    private void Start()
    {
        Time.timeScale = 0f;

        for (int i = 0; i < tutorialPages.Length; i++)
            tutorialPages[i].SetActive(i == 0);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tutorialPages[currentPage].SetActive(false);
            currentPage++;

            if (currentPage >= tutorialPages.Length)
            {
                Time.timeScale = 1f;
                gameObject.SetActive(false);
                return;
            }

            tutorialPages[currentPage].SetActive(true);
        }
    }
}