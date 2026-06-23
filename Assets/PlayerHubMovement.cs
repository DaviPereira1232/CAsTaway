using UnityEngine;

public class PlayerHubMovement : MonoBehaviour
{
    public float speed = 5f;
    public float turnSpeed = 10f;

    public ScreenSwitching screen_script;
    private Rigidbody rb;
    private Animator animator;
    private float horizontalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    void FixedUpdate()
    {
        Vector3 desiredDirection = new Vector3(-horizontalInput, 0, 0);

        if (desiredDirection != Vector3.zero)
        {
            transform.forward = Vector3.Lerp(
                transform.forward,
                desiredDirection,
                turnSpeed
            );

            rb.linearVelocity = transform.forward * speed;
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
            transform.rotation = Quaternion.Euler(0f, 0, 0f);
        }
    }

    void OnTriggerStay(Collider other)
    {
        screen_script.currentTrigger = other.gameObject.name;

        screen_script.DisplayText.gameObject.SetActive(true);

        switch (screen_script.currentTrigger)
        {
            case "Trigger_Aqua":
                screen_script.DisplayText.text = "Aquarium";
                break;

            case "Trigger_PC":
                screen_script.DisplayText.text = "Computer";
                break;

            case "Trigger_Door":
                screen_script.DisplayText.text = "Exit";
                break;
            case "Trigger_Bed":
                screen_script.DisplayText.text = "Bed";
                break;
        }
    }

    void OnTriggerExit(Collider other)
    {
        screen_script.currentTrigger = "";

        screen_script.DisplayText.gameObject.SetActive(false);
    }
}