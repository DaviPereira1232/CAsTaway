using UnityEngine;
using UnityEngine.Splines;

public class SplineFollower : MonoBehaviour
{
    public SplineContainer spline;
    public float speed = 0.1f;

    private float t = 0f;

    void Update()
    {
        if (spline == null) return;

        t += speed * Time.deltaTime;

      
        t = Mathf.Repeat(t, 1f);

        transform.position = spline.EvaluatePosition(t);
    }
}