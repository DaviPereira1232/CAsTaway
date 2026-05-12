using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5;
    public float Fuel = 100;
    public GameObject[] Fish_caught;

    private Rigidbody rb;
    public Image barrinha;
    private float Hori;
    private float Vert;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Hori = Input.GetAxis("Horizontal");
        Vert = Input.GetAxis("Vertical");

        Fuel = Fuel - 1 * Time.deltaTime;

        if (Hori != 0 || Vert != 0)
        {
            Fuel = Fuel - 1 * Time.deltaTime;
        }

        barrinha.fillAmount = Fuel / 100;
    }

    private void FixedUpdate()
    {
        rb.AddForce(Hori * Speed, Vert * Speed, 0);
    }

    private void Pegar(GameObject peixepego)
    {
        //Efeitos do Peixe diminuindo
        peixepego.transform.localScale = Vector3.Lerp(peixepego.transform.localScale, peixepego.transform.localScale / 2, Time.deltaTime * 2f);
        peixepego.transform.position = Vector3.Lerp(peixepego.transform.position, transform.position, Time.deltaTime * 4f);
        //Deletando o peixe
        Destroy(peixepego.gameObject, 0.5f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Fish" && Input.GetButton("Fire1"))
        {
            Pegar(other.gameObject);
        }
    }
}
