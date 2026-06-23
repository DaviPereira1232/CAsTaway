using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerMovement3 : MonoBehaviour
{
    public float Speed = 12;
    public float FuelGasol;
    public List<FishInfo> FishCaught = new List<FishInfo>();

    private Rigidbody rb;
    private float Hori;
    private float Vert;
    public Slider slider;

    //Jorge Mods:
    private Vector2 currentForce;
    private bool canTakeDamage = true;
    public GameObject shipMesh;
    private Vector3 smoothedCurrentVelocity;
    private GameObject nearbyFish;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = 0f;
        FuelGasol = GestãoDeRecursos.Gasolina;

        if (slider != null)
        {
            slider.maxValue = 100;
        }
    }

    void Update()
    {
        Hori = Input.GetAxis("Horizontal");
        Vert = Input.GetAxis("Vertical");

        float drain = 1f;

        if (Hori != 0 || Vert != 0)
            drain += 1f;

        FuelGasol -= drain * Time.deltaTime;

        if (slider != null)
        {
            slider.value = FuelGasol;

        }

        if (FuelGasol <= 0)
        {
            Voltar();
        }

        //Fish Gathering 
        if (nearbyFish != null && Input.GetButtonDown("Fire1"))
        {
            if (nearbyFish.GetComponent<Collider>() != null)
            {
                StartCoroutine(Pegar(
                    nearbyFish,
                    nearbyFish.GetComponent<Collider>(),
                    nearbyFish.GetComponent<MeshRenderer>()
                ));
            }

            Destroy( nearbyFish.GetComponent<SphereCollider>() );
        }

        GestãoDeRecursos.Gasolina = FuelGasol;
    }

    private void FixedUpdate()
    {
        Vector3 input =
            new Vector3(Hori, Vert, 0f).normalized;

        // Target current velocity
        Vector3 targetCurrentVelocity =
            new Vector3(currentForce.x, currentForce.y, 0f);

        // Smoothly blend currents
        smoothedCurrentVelocity = Vector3.Lerp(
            smoothedCurrentVelocity,
            targetCurrentVelocity,
            2f * Time.fixedDeltaTime
        );

        // Velocity relative to water
        Vector3 relativeVelocity =
            rb.linearVelocity - smoothedCurrentVelocity;

        // Player acceleration
        relativeVelocity +=
            input * Speed * Time.fixedDeltaTime;

        // Water drag
        relativeVelocity *= 0.995f;

        // Clamp only player speed
        float maxPlayerSpeed = 20f;

        relativeVelocity =
            Vector3.ClampMagnitude(
                relativeVelocity,
                maxPlayerSpeed
            );

        // Final velocity
        rb.linearVelocity =
            relativeVelocity + smoothedCurrentVelocity;

        // Reset for next frame
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

            peixepego.transform.localScale =
                Vector3.Lerp(
                    startScale,
                    startScale * 0.5f,
                    n
                );

            peixepego.transform.position =
                Vector3.Lerp(
                    startPos,
                    transform.position,
                    n
                );

            yield return null;
        }

        FishData peixeData = peixepego.GetComponent<FishData>();

        if (peixeData == null)
            yield break;

        FishInfo copy = new FishInfo(peixeData);

        Player_Save.Instance.AddFish(copy);

        Destroy(peixepego); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fish"))
        {
            nearbyFish = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == nearbyFish)
        {
            nearbyFish = null;
        }
    }

    //Adicionei uma função para pegar na corrente
    public void ApplyCurrent(Vector2 direction, float strength)
    {
        currentForce += direction.normalized * strength;
    }

    //Função para detetar tag de obstáculo
    private void OnCollisionEnter(Collision collision)
    {
        if (!canTakeDamage) return;

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Hit: " + collision.collider.name);
            //Debug.Log("Ouriço atropelado!");

            FuelGasol -= 10;

            canTakeDamage = false;
            StartCoroutine(HitCooldown());
            StartCoroutine(BlinkShip());
        }
    }

    private IEnumerator HitCooldown()
    {
        canTakeDamage = false;

        Debug.Log("Ouriço atropelado!");

        yield return new WaitForSeconds(0.3f);

        canTakeDamage = true;
    }
    private IEnumerator BlinkShip()
    {
        while (!canTakeDamage)
        {
            shipMesh.SetActive(false);
            yield return new WaitForSeconds(0.1f);

            shipMesh.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }

        shipMesh.SetActive(true);
    }

    public void Voltar()
    {
        SceneManager.LoadScene("Teste");
    }

}
