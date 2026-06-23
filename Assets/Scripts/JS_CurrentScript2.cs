using UnityEngine;

public class CurrentTunnel2 : MonoBehaviour
{
    public Transform directionPoint;
    public float currentStrength = 5f;

    [Header("Audio Settings")]
    [SerializeField] private float soundCooldown = 2f;
    private float lastSoundTime;

    private void OnTriggerStay(Collider other)
    {
        PlayerMovement3 ship = other.GetComponent<PlayerMovement3>();

        if (ship != null)
        {
            Vector3 direction =
                (directionPoint.position - transform.position).normalized;

            ship.ApplyCurrent(
                new Vector2(direction.x, direction.y),
                currentStrength
            );

            if (Time.time - lastSoundTime >= soundCooldown)
            {
                AudioManager.Instance.PlayBubblesAtPosition(transform.position);
                lastSoundTime = Time.time;
            }
        }
    }
}