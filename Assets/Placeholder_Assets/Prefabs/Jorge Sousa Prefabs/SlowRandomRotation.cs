using UnityEngine;

public class SpaceRotation : MonoBehaviour
{
    [SerializeField] private float minRotationSpeed = 2f;
    [SerializeField] private float maxRotationSpeed = 10f;

    private Vector3 rotationAxis;
    private float rotationSpeed;

    private void Start()
    {
       
        rotationAxis = Random.onUnitSphere;


        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);

      
        if (Random.value > 0.5f)
        {
            rotationSpeed *= -1f;
        }
    }

    private void Update()
    {
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime, Space.World);
    }
}