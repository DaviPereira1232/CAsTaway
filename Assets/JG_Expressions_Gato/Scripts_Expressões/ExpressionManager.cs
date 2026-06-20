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

    private void Start()
    {
        StartCoroutine(BlinkLoop());
    }

    private void Update()
    {
        ApplyState();
    }

    private void ApplyState()
    {
        if (blink != null)
            blink.SetActive(currentExpression == ExpressionType.Blink);

        if (scare != null)
            scare.SetActive(currentExpression == ExpressionType.Scare);
            
        if (happy != null)
            happy.SetActive(currentExpression == ExpressionType.Happy);

        if (xx != null)
            xx.SetActive(currentExpression == ExpressionType.XX);
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