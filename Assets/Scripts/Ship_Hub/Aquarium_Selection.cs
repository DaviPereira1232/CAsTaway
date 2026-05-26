using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Aquarium_Selection : MonoBehaviour
{
    public GameObject[] Peixes;
    public Transform[] Transform_Peixe;
    private GameObject[] peixes;

    public ScreenSwitching screenScript;
    public CatShipMovement catScript;
    public void Start()
    {
        peixes = new GameObject[Peixes.Length];

        {
            for (int i = 0; i < Peixes.Length; i++)
                if (Peixes[i] != null)
                {
                    peixes[i] = Instantiate(
                        Peixes[i],
                        Transform_Peixe[i]
                    );
                }
        }
    }
    public void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 10000);
        Debug.DrawLine(ray.origin, hit.point);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject == Transform_Peixe[0].gameObject && Input.GetButtonDown("Fire1") && Peixes[0])
            {
                Transform_Peixe[0].GetChild(0).gameObject.SetActive(true);
                Transform_Peixe[0].GetChild(0).gameObject.GetComponent<Animator>().Play("Peixe_Cabeçalho");
            }
            else if (hit.collider.gameObject == Transform_Peixe[1].gameObject && Input.GetButtonDown("Fire1") && Peixes[1])
            {
                Transform_Peixe[1].GetChild(0).gameObject.SetActive(true);
                Transform_Peixe[1].GetChild(0).gameObject.GetComponent<Animator>().Play("Peixe_Cabeçalho");
            }
            else if (hit.collider.gameObject == Transform_Peixe[2].gameObject && Input.GetButtonDown("Fire1") && Peixes[2])
            {
                Transform_Peixe[2].GetChild(0).gameObject.SetActive(true);
                Transform_Peixe[2].GetChild(0).gameObject.GetComponent<Animator>().Play("Peixe_Cabeçalho");
            }
            else if (hit.collider.gameObject == Transform_Peixe[3].gameObject && Input.GetButtonDown("Fire1") && Peixes[3])
            {
                Transform_Peixe[3].GetChild(0).gameObject.SetActive(true);
                Transform_Peixe[3].GetChild(0).gameObject.GetComponent<Animator>().Play("Peixe_Cabeçalho");
            }
            else if (hit.collider.gameObject == Transform_Peixe[4].gameObject && Input.GetButtonDown("Fire1") && Peixes[4])
            {
                Transform_Peixe[4].GetChild(0).gameObject.SetActive(true);
                Transform_Peixe[4].GetChild(0).gameObject.GetComponent<Animator>().Play("Peixe_Cabeçalho");
            }
        }
        else if (hit.collider == null)
        {
            Transform_Peixe[0].GetChild(0).gameObject.SetActive(false);
            Transform_Peixe[1].GetChild(0).gameObject.SetActive(false);
            Transform_Peixe[2].GetChild(0).gameObject.SetActive(false);
            Transform_Peixe[3].GetChild(0).gameObject.SetActive(false);
            Transform_Peixe[4].GetChild(0).gameObject.SetActive(false);
        }

    }
    public void Voltar()
    {
        screenScript.currentPos = 0;
        catScript.CanPlayerWalk = true;
    }
}
