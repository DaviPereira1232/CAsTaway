using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5;
    public float Fuel = 100;
    public UnityEngine.UI.Image barrinha;
    public int Número_de_peixes = 0;
    public GameObject[] Fish_caught;

    private Rigidbody rb;
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

    private void Pegar(GameObject peixepego, Collider colisao, MeshRenderer mesh)
    {
        //Efeitos do Peixe diminuindo
        peixepego.transform.localScale = Vector3.Lerp(peixepego.transform.localScale, peixepego.transform.localScale / 2, Time.deltaTime * 2f);
        peixepego.transform.position = Vector3.Lerp(peixepego.transform.position, transform.position, Time.deltaTime * 4f);
        //Deletando o peixe
        Destroy(colisao);
        Fish_caught[Número_de_peixes] = peixepego;
        Destroy(mesh, 1f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Fish" && Input.GetButton("Fire1"))
        {
            Pegar(other.gameObject,other.GetComponent<Collider>(), other.GetComponent<MeshRenderer>());
        }
    }
}
