using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] tutorialPages;

    private int currentPage = 0;
    private static bool tutorialShown = false;
    private bool waitingForFinishAnimation = false;

    private void Awake()
    {
        if (GestaoDeRecursos.dia_num > 1)
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        Time.timeScale = 0f;

        if (tutorialShown)
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
            return;
        }

        for (int i = 0; i < tutorialPages.Length; i++)
            tutorialPages[i].SetActive(i == 0);

    }

    private void Update()
    {
        if (waitingForFinishAnimation)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            // Last page clicked
            if (currentPage == tutorialPages.Length - 1)
            {
                waitingForFinishAnimation = true;
                return;
            }

            tutorialPages[currentPage].SetActive(false);
            currentPage++;

            if (currentPage >= tutorialPages.Length - 1)
            {
                Time.timeScale = 1f;
            }

                if (currentPage >= tutorialPages.Length)
                {
                    Time.timeScale = 1f;
                    gameObject.SetActive(false);
                    return;
                }

                tutorialPages[currentPage].SetActive(true);
            }
    }

    public void FinishTutorial()
    {
        tutorialShown = true;
        gameObject.SetActive(false);
    }
}