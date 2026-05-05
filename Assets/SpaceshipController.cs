using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public float Speed;
    private float Hori;
    private float Vert;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Hori = Input.GetAxis("Horizontal");
        Vert = Input.GetAxis("Vertical");

        transform.Translate(Hori * Speed * Time.deltaTime, Vert * Speed * Time.deltaTime, transform.position.z);

    }
}
