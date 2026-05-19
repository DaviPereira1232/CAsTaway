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
        foreach (GameObject peixe in Fish_caught)
        {
            if (peixe == peixepego)
            {
                return;
            }
        }

        // primeiro slot vazio
        for (int i = 0; i < Fish_caught.Length; i++)
        {
            if (Fish_caught[i] == null)
            {
                Fish_caught[i] = peixepego;
                Número_de_peixes++;

                Debug.Log("Peixe guardado no slot " + i);

                // Efeitos visuais
                peixepego.transform.localScale = Vector3.Lerp(
                    peixepego.transform.localScale,
                    peixepego.transform.localScale / 2,
                    Time.deltaTime * 2f
                );

                peixepego.transform.position = Vector3.Lerp(
                    peixepego.transform.position,
                    transform.position,
                    Time.deltaTime * 4f
                );

                // Desativa colisão para impedir capturas repetidas
                colisao.enabled = false;

                // Esconde peixe depois
                Destroy(mesh, 1f);

                return;
            }
        }

        Debug.Log("Inventário cheio!");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Fish" && Input.GetButtonDown("Fire1"))
        {
            Pegar(other.gameObject, other.GetComponent<Collider>(), other.GetComponent<MeshRenderer>());
        }
    }

}
