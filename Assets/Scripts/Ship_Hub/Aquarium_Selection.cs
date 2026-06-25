using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Aquarium_Selection : MonoBehaviour
{
    public GameObject[] PeixesDoAquario;
    public Transform[] Transform_Peixe;
    private GameObject[] peixes;

    public ScreenSwitching screenScript;
    public CatShipMovement catScript;
    public void Start()
    {
        peixes = new GameObject[PeixesDoAquario.Length];

        {
            for (int i = 0; i < PeixesDoAquario.Length; i++)
                if (PeixesDoAquario[i] != null)
                {
                    peixes[i] = Instantiate(
                        PeixesDoAquario[i],
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
            if (hit.collider.gameObject == Transform_Peixe[0].gameObject && Input.GetButtonDown("Fire1") && PeixesDoAquario[0])
            {
                Transform_Peixe[0].GetChild(0).gameObject.SetActive(true);
                Transform_Peixe[0].GetChild(0).gameObject.GetComponent<Animator>().Play("Peixe_Cabe�alho");
                AudioManager.Instance.PlayRandomGlub();
            }
            else if (hit.collider.gameObject == Transform_Peixe[1].gameObject && Input.GetButtonDown("Fire1") && PeixesDoAquario[1])
            {
                Transform_Peixe[1].GetChild(0).gameObject.SetActive(true);
                Transform_Peixe[1].GetChild(0).gameObject.GetComponent<Animator>().Play("Peixe_Cabe�alho");
                AudioManager.Instance.PlayRandomGlub();
            }
            else if (hit.collider.gameObject == Transform_Peixe[2].gameObject && Input.GetButtonDown("Fire1") && PeixesDoAquario[2])
            {
                Transform_Peixe[2].GetChild(0).gameObject.SetActive(true);
                Transform_Peixe[2].GetChild(0).gameObject.GetComponent<Animator>().Play("Peixe_Cabe�alho");
                AudioManager.Instance.PlayRandomGlub();
            }
            else if (hit.collider.gameObject == Transform_Peixe[3].gameObject && Input.GetButtonDown("Fire1") && PeixesDoAquario[3])
            {
                Transform_Peixe[3].GetChild(0).gameObject.SetActive(true);
                Transform_Peixe[3].GetChild(0).gameObject.GetComponent<Animator>().Play("Peixe_Cabe�alho");
                AudioManager.Instance.PlayRandomGlub();
            }
            else if (hit.collider.gameObject == Transform_Peixe[4].gameObject && Input.GetButtonDown("Fire1") && PeixesDoAquario[4])
            {
                Transform_Peixe[4].GetChild(0).gameObject.SetActive(true);
                Transform_Peixe[4].GetChild(0).gameObject.GetComponent<Animator>().Play("Peixe_Cabe�alho");
                AudioManager.Instance.PlayRandomGlub();
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
