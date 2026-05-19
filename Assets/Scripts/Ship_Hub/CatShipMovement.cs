using TMPro;
using UnityEngine;

public class CatShipMovement : MonoBehaviour
{
    public float Speed = 5;
    private float HoriInput;
    public bool CanPlayerWalk;
    public GameObject CatMesh;

    private Rigidbody RB;
    public GameObject camManagement;
    private ScreenSwitching camScript;
    public TextMeshProUGUI DisplayText;
    private string currentTrigger = "";

    void Start()
    {
        RB = GetComponent<Rigidbody>(); 
        camScript = camManagement.GetComponent<ScreenSwitching>();

        DisplayText.gameObject.SetActive(false);
    }
    void Update()
    {
        HoriInput = Input.GetAxis("Horizontal");

        if (currentTrigger != "" && Input.GetButtonDown("Fire1"))
        {
            Interact();
        }

    }
    private void FixedUpdate()
    {
        if (CanPlayerWalk)
        {
            Vector3 movement = new Vector3(HoriInput * Speed, RB.linearVelocity.y, 0);
            RB.linearVelocity = movement;
            CatMesh.SetActive(true);
        }
        else
        {
            RB.linearVelocity = Vector3.zero;
            RB.angularVelocity = Vector3.zero;
            CatMesh.SetActive(false);
        }

    }

    void Interact()
    {
        if (CanPlayerWalk)
        {
            switch (currentTrigger)
            {
                case "Trigger_Aqua":
                    camScript.currentPos = 1;
                    break;

                case "Trigger_PC":
                    camScript.currentPos = 2;
                    break;

                case "Trigger_Door":
                    camScript.currentPos = 3;
                    break;
            }

            CanPlayerWalk = false;
            DisplayText.gameObject.SetActive(false);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        currentTrigger = other.gameObject.name;

        DisplayText.gameObject.SetActive(true);

        switch (currentTrigger)
        {
            case "Trigger_Aqua":
                DisplayText.text = "Aquarium";
                break;

            case "Trigger_PC":
                DisplayText.text = "Computer";
                break;

            case "Trigger_Door":
                DisplayText.text = "Exit";
                break;
        }
    }

    void OnTriggerExit(Collider other)
    {
        currentTrigger = "";

        DisplayText.gameObject.SetActive(false);
    }
}
