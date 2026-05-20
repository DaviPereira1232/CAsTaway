using UnityEngine;

public class PingPongObstacle : MonoBehaviour
{
    public Transform leftBound;
    public Transform rightBound;

    public float speed = 3f;

    private Vector3 target;

    void Start()
    {
        target = rightBound.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            target = (target == rightBound.position)
                ? leftBound.position
                : rightBound.position;
        }
    }

    void OnDrawGizmos()
    {
        if (leftBound == null || rightBound == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(leftBound.position, rightBound.position);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(leftBound.position, 0.2f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(rightBound.position, 0.2f);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 0.15f);
    }
}