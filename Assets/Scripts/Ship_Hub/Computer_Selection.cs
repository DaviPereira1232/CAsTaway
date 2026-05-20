using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Computer_Selection : MonoBehaviour
{
    public ScreenSwitching screenScript;
    public CatShipMovement catScript;

    public GameObject Main_Menu;
    public GameObject EscoAsteroide_Menu;
    public GameObject EscoPeixe_Menu;

    public GameObject[] Peixes;
    public Transform Transform_Peixe;
    public int Peixe_Atual;
    public TextMeshProUGUI Nome_Text;
    public TextMeshProUGUI Descri_Text;
    private FishData info_Peixe;
    private GameObject peixeinstanciado;

    public RawImage Gaso;
    public RawImage Sani;
    public RawImage Fome;

    void Start()
    {
        EscoAsteroide_Menu.SetActive(false);
        EscoPeixe_Menu.SetActive(false);

        Peixe_Atual = 0;

        peixeinstanciado = Instantiate(Peixes[Peixe_Atual], Transform_Peixe);
    }

    void Update()
    {
        info_Peixe = peixeinstanciado.GetComponent<FishData>();

        peixeinstanciado.transform.localScale = new Vector3(0.42f, 0.42f, 0.42f);

        if (info_Peixe != null)
        {
            Nome_Text.text = info_Peixe.Nome_do_Peixe;
            Descri_Text.text = info_Peixe.Descrição_do_Peixe;

            if (info_Peixe.Recurso_Gaso)
            {
                Gaso.color = Color.white;
            }
            else if (!info_Peixe.Recurso_Gaso)
            {
                Gaso.color = Color.black;
            }

            if (info_Peixe.Recurso_Sani)
            {
                Sani.color = Color.white;
            }
            else if (!info_Peixe.Recurso_Sani)
            {
                Sani.color = Color.black;
            }

            if (info_Peixe.Recurso_Fome)
            {
                Fome.color = Color.white;
            }
            else if (!info_Peixe.Recurso_Fome)
            {
                Fome.color = Color.black;
            }
        }
    }

    public void MudarMenu(int num)
    {
        if (num == 0)
        {
            Main_Menu.SetActive(true);
            EscoAsteroide_Menu.SetActive(false);
            EscoPeixe_Menu.SetActive(false);
            Debug.Log("Menu");
        }
        else if (num == 1)
        {
            Main_Menu.SetActive(false);
            EscoAsteroide_Menu.SetActive(true);
            EscoPeixe_Menu.SetActive(false);
            Debug.Log("Asteroide");
        }
        else if (num == 2)
        {
            Main_Menu.SetActive(false);
            EscoAsteroide_Menu.SetActive(false);
            EscoPeixe_Menu.SetActive(true);
            Debug.Log("Peixe");
        }
        else if (num == 3)
        {
            screenScript.currentPos = 0;
            catScript.CanPlayerWalk = true;
            Debug.Log("Sair");
        }
    }

    public void Subir()
    {
        Peixe_Atual++;

        if (Peixe_Atual >= Peixes.Length)
        {
            Peixe_Atual = 0;
        }

        Destroy(peixeinstanciado.gameObject);

        peixeinstanciado = Instantiate(
            Peixes[Peixe_Atual],
            Transform_Peixe
        );
    }

    public void Descer()
    {
        Peixe_Atual--;

        if (Peixe_Atual < 0)
        {
            Peixe_Atual = Peixes.Length - 1;
        }

        Destroy(peixeinstanciado.gameObject);

        peixeinstanciado = Instantiate(
            Peixes[Peixe_Atual],
            Transform_Peixe
        );
    }
}
