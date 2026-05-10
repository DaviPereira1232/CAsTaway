using TMPro;
using UnityEngine;

public class ScreenSwitching : MonoBehaviour
{
    public Transform[] camPoses;
    public TextMeshProUGUI DisplayText;
    public int currentPos = 0;
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
            cam.fieldOfView = 80;

        }
        else if (currentPos == 2)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, camPoses[2].transform.position, Time.deltaTime * 5);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, camPoses[2].transform.rotation, Time.deltaTime * 5);
            cam.fieldOfView = 80;
        }
        else if (currentPos == 3)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, camPoses[3].transform.position, Time.deltaTime * 5);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, camPoses[3].transform.rotation, Time.deltaTime * 5);
            cam.fieldOfView = 80;
        }
        else if (currentPos == 0)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, camPoses[0].transform.position, Time.deltaTime * 4);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, camPoses[0].transform.rotation, Time.deltaTime * 4);
            cam.fieldOfView = 40;
        }

        //Alternar de UIs
        if (cam.transform.position == camPoses[1].transform.position)
        {
            Aqua_UI.SetActive(true);
            PC_UI.SetActive(false);
        }
        else if (cam.transform.position == camPoses[2].transform.position)
        {
            PC_UI.SetActive(true);
            Aqua_UI.SetActive(false);
        }
        else
        {
            Aqua_UI.SetActive(false);
            PC_UI.SetActive(false);
        }
    }
}
