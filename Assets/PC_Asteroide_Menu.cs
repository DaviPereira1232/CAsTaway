using UnityEngine;

public class PC_Asteroide_Menu : MonoBehaviour
{
    public GameObject[] Asteroides;

    void Start()
    {
        
    }

    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 10000);
        Debug.DrawLine(ray.origin, hit.point);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject == Asteroides[0])
            {
                Asteroides[0].transform.GetChild(0).gameObject.SetActive(true);
                Asteroides[0].transform.GetChild(0).gameObject.GetComponent<Animator>().Play("Asteroide_Cabeçalho");
                Asteroides[1].transform.GetChild(0).gameObject.SetActive(false);
                Asteroides[2].transform.GetChild(0).gameObject.SetActive(false);

                if (Input.GetMouseButtonDown(0))
                {

                }
            }
            else if (hit.collider.gameObject == Asteroides[1])
            {
                Asteroides[0].transform.GetChild(0).gameObject.SetActive(false);
                Asteroides[1].transform.GetChild(0).gameObject.GetComponent<Animator>().Play("Asteroide_Cabeçalho");
                Asteroides[1].transform.GetChild(0).gameObject.SetActive(true);
                Asteroides[2].transform.GetChild(0).gameObject.SetActive(false);
            }
            else if (hit.collider.gameObject == Asteroides[2])
            {
                Asteroides[0].transform.GetChild(0).gameObject.SetActive(false);
                Asteroides[2].transform.GetChild(0).gameObject.GetComponent<Animator>().Play("Asteroide_Cabeçalho");
                Asteroides[1].transform.GetChild(0).gameObject.SetActive(false);
                Asteroides[2].transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        else if (hit.collider == null)
        {
            Asteroides[0].transform.GetChild(0).gameObject.SetActive(false);
            Asteroides[1].transform.GetChild(0).gameObject.SetActive(false);
            Asteroides[2].transform.GetChild(0).gameObject.SetActive(false);
        }

    }

}
