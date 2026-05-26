using System.Collections.Generic;
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

    private List<FishInfo> Peixes = new List<FishInfo>();

    public Transform Transform_Peixe;
    public int Peixe_Atual;

    public TextMeshProUGUI Nome_Text;
    public TextMeshProUGUI Descri_Text;

    public RawImage Gaso;
    public RawImage Sani;
    public RawImage Fome;

    private GameObject peixeinstanciado;

    void Start()
    {
        EscoAsteroide_Menu.SetActive(false);
        EscoPeixe_Menu.SetActive(false);

        if (Player_Save.Instance != null)
        {
            Peixes = Player_Save.Instance.FishCaught;
        }

        if (Peixes == null)
            Peixes = new List<FishInfo>();

        Peixe_Atual = 0;

        if (Peixes.Count > 0)
        {
            SpawnFish();
            AtualizarUI();
        }
        else
        {
            ClearUI();
        }
    }

    void Update()
    {

    }


    void AtualizarUI()
    {
        if (Peixes.Count == 0) return;

        FishInfo fish = Peixes[Peixe_Atual];

        Nome_Text.text = fish.Nome_do_Peixe;
        Descri_Text.text = fish.Descrição_do_Peixe;

        Gaso.color = fish.Recurso_Gaso ? Color.white : Color.black;
        Sani.color = fish.Recurso_Sani ? Color.white : Color.black;
        Fome.color = fish.Recurso_Fome ? Color.white : Color.black;
    }

    void ClearUI()
    {
        Nome_Text.text = "Sem peixes";
        Descri_Text.text = "";
    }

    void SpawnFish()
    {
        if (Peixes.Count == 0) return;

        if (peixeinstanciado != null)
            Destroy(peixeinstanciado);

        peixeinstanciado = Instantiate(
            Peixes[Peixe_Atual].PrefabVisual,
            Transform_Peixe
        );

        peixeinstanciado.transform.localScale = Vector3.one * 0.42f;
    }

    public void MudarMenu(int num)
    {
        Main_Menu.SetActive(num == 0);
        EscoAsteroide_Menu.SetActive(num == 1);
        EscoPeixe_Menu.SetActive(num == 2);

        if (num == 3)
        {
            if (screenScript != null)
                screenScript.currentPos = 0;

            if (catScript != null)
                catScript.CanPlayerWalk = true;

            Debug.Log("Sair");
        }
    }

    public void Subir()
    {
        if (Peixes.Count == 0) return;

        Peixe_Atual++;
        if (Peixe_Atual >= Peixes.Count)
            Peixe_Atual = 0;

        SpawnFish();
        AtualizarUI();
    }

    public void Descer()
    {
        if (Peixes.Count == 0) return;

        Peixe_Atual--;
        if (Peixe_Atual < 0)
            Peixe_Atual = Peixes.Count - 1;

        SpawnFish();
        AtualizarUI();
    }
}