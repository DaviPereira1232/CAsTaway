using UnityEngine;

public class CurrentTunnel : MonoBehaviour
{
    public float currentStrength = 5f;

    private void OnTriggerStay(Collider other)
    {
        PlayerMovement ship =
            other.GetComponent<PlayerMovement>();

        if (ship != null)
        {
            ship.ApplyCurrent(
                transform.right,
                currentStrength
            );
        }
    }
}
