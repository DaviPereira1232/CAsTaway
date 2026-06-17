using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenSwitching : MonoBehaviour
{
    public Transform[] camPoses;
    public GameObject Player;
    public TextMeshProUGUI DisplayText;
    public int currentPos = 0;
    private float camFOV = 40;
    public string currentTrigger = "";
    private bool mudarDeCena = false;
    //CAM 0 Player
    //CAM 1 Aqua
    //CAM 2 PC
    //CAM 3 Exit
    public GameObject Aqua_UI;
    public GameObject PC_UI;
    public Camera cam;
    public Camera cam_cutscene;
    public GameObject monitormesh;
    public GameObject aperturemesh;

    void Start()
    {
       
    }

    void Update()
    {

        if (currentTrigger != "" && Input.GetButtonDown("Fire1"))
        {
            Interact();
        }

        //Mudar posição da câmera
        if (currentPos == 1)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position,camPoses[1].transform.position, Time.deltaTime * 5);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, camPoses[1].transform.rotation, Time.deltaTime * 5);
            camFOV = 35;

        }
        else if (currentPos == 2)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, camPoses[2].transform.position, Time.deltaTime * 5);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, camPoses[2].transform.rotation, Time.deltaTime * 5);
            camFOV = 45;
        }
        else if (currentPos == 3)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, camPoses[3].transform.position, Time.deltaTime * 5);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, camPoses[3].transform.rotation, Time.deltaTime * 5);
            camFOV = 45;
        }
        else if (currentPos == 0)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, camPoses[0].transform.position, Time.deltaTime * 4);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, camPoses[0].transform.rotation, Time.deltaTime * 4);
            camFOV = 23;
        }

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, camFOV, Time.deltaTime * 2);

        //Alternar de UIs
        if (Vector3.Distance(cam.transform.position, camPoses[2].position) < 0.5f)
        {
            Aqua_UI.SetActive(true);
            PC_UI.SetActive(false);
        }
        else if (Vector3.Distance(cam.transform.position, camPoses[1].position) < 0.01f)
        {
            PC_UI.SetActive(true);
            Aqua_UI.SetActive(false);
        }
        else if (Vector3.Distance(cam.transform.position, camPoses[3].position) < 0.0001f)
        {
            if (cam.gameObject.activeSelf)
            {
                Sair();
            }
        }
        else
        {
            Aqua_UI.SetActive(false);
            PC_UI.SetActive(false);
        }

        Debug.Log(currentPos);

        if (mudarDeCena == true && !cam_cutscene.GetComponent<Animation>().isPlaying)
        {
            SceneManager.LoadScene("Asteroid_LEVEL1");
        }
    }

    void Interact()
    {
        if (Player.activeSelf)
        {
            switch (currentTrigger)
            {
                case "Trigger_Aqua":
                    currentPos = 2;
                    break;

                case "Trigger_PC":
                    currentPos = 1;
                    monitormesh.GetComponent<Animation>().Play("Non_Static");
                    break;

                case "Trigger_Door":
                    currentPos = 3;
                    break;
            }

            Player.SetActive(false);
            DisplayText.gameObject.SetActive(false);
        }

    }

    public void Voltar()
    {
        currentPos = 0;
        Player.SetActive(true);
        monitormesh.GetComponent<Animation>().Play("Static");
    }

    public void Sair()
    {
        cam.gameObject.SetActive(false);
        cam_cutscene.gameObject.SetActive(true);
        cam_cutscene.GetComponent<Animation>().Play();
        aperturemesh.GetComponent<Animation>().Play();
        mudarDeCena = true;
    }

}
