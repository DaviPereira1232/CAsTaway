using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class PlayerMovement2 : MonoBehaviour
{
    public float Speed = 12;
    public float Fuel = 100;
    public UnityEngine.UI.Image barrinha;
    public List<GameObject> Fish_caught = new List<GameObject>();

    private Rigidbody rb;
    private float Hori;
    private float Vert;

    //Jorge Mods:
    private Vector2 currentForce;
    private bool canTakeDamage = true;
    public MeshRenderer shipMesh;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = 0f;
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
        Vector3 movement = new Vector3(Hori, Vert, 0f).normalized;

        // Main movement (same as original feel)
        rb.AddForce(movement * Speed, ForceMode.Acceleration);

        // Current force
        Vector3 currentVelocity = new Vector3(currentForce.x, currentForce.y, 0f);

        // Convert current into velocity influence instead of constant force
        rb.linearVelocity += currentVelocity * Time.fixedDeltaTime * 2f;


        // MUCH lighter water drag (key change)
        rb.linearVelocity *= 0.995f;

        // Higher max speed so it can actually ramp up
        float maxSpeed = 20f;

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity =
                rb.linearVelocity.normalized * maxSpeed;
        }

        currentForce = Vector2.zero;
    }

    private IEnumerator Pegar(GameObject peixepego, Collider colisao, MeshRenderer mesh)
    {
        colisao.enabled = false;

        Vector3 startScale = peixepego.transform.localScale;
        Vector3 startPos = peixepego.transform.position;

        float duration = 0.4f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;

            float n = Mathf.SmoothStep(0f, 1f, t / duration);

            // smooth shrink
            peixepego.transform.localScale = Vector3.Lerp(
                startScale,
                startScale * 0.5f,
                n
            );

            // smooth pull to player
            peixepego.transform.position = Vector3.Lerp(
                startPos,
                transform.position,
                n
            );

            yield return null;
        }

        Fish_caught.Add(peixepego);

        Destroy(mesh);
        Destroy(peixepego);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Fish") && Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Pegar(
                other.gameObject,
                other.GetComponent<Collider>(),
                other.GetComponent<MeshRenderer>()
            ));
        }
    }

    //Adicionei uma função para pegar na corrente
    public void ApplyCurrent(Vector2 direction, float strength)
    {
        currentForce += direction * strength;
    }

    //Função para detetar tag de obstáculo
    private void OnCollisionEnter(Collision collision)
    {
        if (!canTakeDamage) return;

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Hit: " + collision.collider.name);
            //Debug.Log("Ouriço atropelado!");

            Fuel -= 10;

            canTakeDamage = false;
            StartCoroutine(HitCooldown());
            StartCoroutine(BlinkShip());
        }
    }

    private IEnumerator HitCooldown()
    {
        canTakeDamage = false;

        Debug.Log("Ouriço atropelado!");
        Fuel -= 10;

        yield return new WaitForSeconds(0.3f);

        canTakeDamage = true;
    }
    private IEnumerator BlinkShip()
    {
        while (!canTakeDamage)
        {
            shipMesh.enabled = false;
            yield return new WaitForSeconds(0.1f);

            shipMesh.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        shipMesh.enabled = true;
    }

}

