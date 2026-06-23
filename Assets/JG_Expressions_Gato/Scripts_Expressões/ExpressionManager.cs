using System.Collections;
using UnityEngine;

public class ExpressionController : MonoBehaviour
{
    public enum ExpressionType
    {
        Blink,
        Scare,
        Happy,
        XX
    }

    [Header("Ativo: ")]
    [SerializeField] private ExpressionType currentExpression;

    [Header("Expressões: ")]
    [SerializeField] private GameObject blink;
    [SerializeField] private GameObject scare;
    [SerializeField] private GameObject xx;
    [SerializeField] private GameObject happy;

    [Header("Tempo entre cada Loop:")]
    [SerializeField] private float blinkCooldown = 2f;

    private ExpressionType previousExpression; // Track previous state

    private void Start()
    {
        previousExpression = currentExpression; // Initialize
        StartCoroutine(BlinkLoop());
    }

    private void Update()
    {
        // Check if expression changed
        if (currentExpression != previousExpression)
        {
            OnExpressionChanged(currentExpression);
            previousExpression = currentExpression;
        }
        
        ApplyState();
    }

    // New method - plays sound only when expression changes
    private void OnExpressionChanged(ExpressionType newExpression)
    {
        switch (newExpression)
        {
            case ExpressionType.Scare:
                AudioManager.Instance.PlayScaredCat();
                break;
            case ExpressionType.Happy:
                AudioManager.Instance.PlayHappyCat();
                break;
            case ExpressionType.XX:
                AudioManager.Instance.PlayUnwellCat();
                break;
            // Blink doesn't need a sound
        }
    }

    private void ApplyState()
    {
        
        if (blink != null)
        {
            blink.SetActive(currentExpression == ExpressionType.Blink);
        }

        if (scare != null)
        {
            scare.SetActive(currentExpression == ExpressionType.Scare);
        }

        if (happy != null)
        {
            happy.SetActive(currentExpression == ExpressionType.Happy);
        }

        if (xx != null)
        {
            xx.SetActive(currentExpression == ExpressionType.XX);
        }
    }

    private IEnumerator BlinkLoop()
    {
        while (true)
        {
            if (currentExpression == ExpressionType.Blink)
            {
                blink.SetActive(true);
                yield return new WaitForSeconds(0.1f);

                blink.SetActive(false);
                yield return new WaitForSeconds(blinkCooldown);
            }
            else
            {
                yield return null;
            }
        }
    }

    public void SetExpression(ExpressionType expression)
    {
        currentExpression = expression;
    }
}