using UnityEngine;

public class CurrentTunnel2 : MonoBehaviour
{
    public Transform directionPoint;
    public float currentStrength = 5f;

    private void OnTriggerStay(Collider other)
    {
        PlayerMovement2 ship = other.GetComponent<PlayerMovement2>();

        if (ship != null)
        {
            Vector3 direction =
                (directionPoint.position - transform.position).normalized;

            ship.ApplyCurrent(
                new Vector2(direction.x, direction.y),
                currentStrength
            );
        }
    }
}