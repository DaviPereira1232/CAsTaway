using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenSwitching : MonoBehaviour
{
    public Transform[] camPoses;
    public TextMeshProUGUI DisplayText;
    public int currentPos = 0;
    public float camFOV = 40;
    //CAM 0 Player
    //CAM 1 Aqua
    //CAM 2 PC
    //CAM 3 Exit
    public GameObject Aqua_UI;
    public GameObject PC_UI;
    public Camera cam;

    void Start()
    {
       
    }

    void Update()
    {
        //Mudar posição da câmera
        if (currentPos == 1)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position,camPoses[1].transform.position, Time.deltaTime * 5);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, camPoses[1].transform.rotation, Time.deltaTime * 5);
            camFOV = 80;

        }
        else if (currentPos == 2)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, camPoses[2].transform.position, Time.deltaTime * 5);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, camPoses[2].transform.rotation, Time.deltaTime * 5);
            camFOV = 80;
        }
        else if (currentPos == 3)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, camPoses[3].transform.position, Time.deltaTime * 5);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, camPoses[3].transform.rotation, Time.deltaTime * 5);
            SceneManager.LoadScene("Asteroid_LEVEL1");
            camFOV = 80;
        }
        else if (currentPos == 0)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, camPoses[0].transform.position, Time.deltaTime * 4);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, camPoses[0].transform.rotation, Time.deltaTime * 4);
            camFOV = 40;
        }

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, camFOV, Time.deltaTime * 2);

        //Alternar de UIs
        if (Vector3.Distance(cam.transform.position, camPoses[1].position) < 0.5f)
        {
            Aqua_UI.SetActive(true);
            PC_UI.SetActive(false);
        }
        else if (Vector3.Distance(cam.transform.position, camPoses[2].position) < 0.5f)
        {
            PC_UI.SetActive(true);
            Aqua_UI.SetActive(false);
        }
        else
        {
            Aqua_UI.SetActive(false);
            PC_UI.SetActive(false);
        }

        Debug.Log(currentPos);
    }
}
