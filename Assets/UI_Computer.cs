using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Computer : MonoBehaviour
{
    [SerializeField] public int qual_menu = 0;

    [Header("-Menu Principal")]
    public GameObject objectos_menu;
    public TextMeshProUGUI[] Main_Alt_Text;

    [Header("-Menu Asteroide")]
    public GameObject objectos_aste;
    public GameObject[] Asteroides;

    [Header("-Menu Peixedex")]
    private List<FishInfo> Peixes = new List<FishInfo>();
    public TextMeshProUGUI Nome_Text;
    public TextMeshProUGUI Descri_Text;
    public TextMeshProUGUI Counter;
    private GameObject peixeinstanciado;
    public Transform peixe_transform;
    private int peixe_atual;
    public GameObject objectos_peixe;
    public GameObject Full;
    public Aquarium aqua;

    void Start()
    {
        peixe_atual = 0;

        if (Player_Save.Instance != null)
        {
            Peixes = Player_Save.Instance.FishCaught;
        }

        if (Peixes == null)
            Peixes = new List<FishInfo>();

        if (Peixes.Count > 0)
        {
            SpawnPeixe();
        }
    }

    void Update()
    {
        // Gerenciamento de Menus
        objectos_menu.SetActive(qual_menu == 0);
        objectos_aste.SetActive(qual_menu == 1);
        objectos_peixe.SetActive(qual_menu == 2);

        // Contador de Peixes
        if (Peixes.Count == 0)
            Counter.text = peixe_atual + "/" + Peixes.Count;
        else
            Counter.text = (peixe_atual + 1) + "/" + Peixes.Count;

        // Feedback de aquário cheio
        if (aqua != null && Full != null)
        {
            Full.SetActive(aqua.cheio);
        }
    }

    public void ShowAltText(int texto)
    {
        Main_Alt_Text[0].enabled = (texto == 1);
        Main_Alt_Text[1].enabled = (texto == 2);
    }

    public void MudarMenu(int próximo_menu)
    {
        qual_menu = próximo_menu;
    }

    public void EscolherPlaneta(int selecionado)
    {
        if (selecionado >= 0 && selecionado < Asteroides.Length)
        {
            Asteroides[selecionado].transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            Asteroides[selecionado].transform.Find("Sign").gameObject.SetActive(true);
            Asteroides[selecionado].transform.Find("Highlight").gameObject.SetActive(true);
        }
        else if (selecionado == -1)
        {
            foreach (var asteroide in Asteroides)
            {
                if (asteroide == null) continue;
                asteroide.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                asteroide.transform.Find("Sign").gameObject.SetActive(false);
                asteroide.transform.Find("Highlight").gameObject.SetActive(false);
            }
        }
    }

    public void MudarDePeixe(int input)
    {
        if (Peixes.Count == 0) return;

        if (input == 1)
        {
            peixe_atual++;
            if (peixe_atual >= Peixes.Count) peixe_atual = 0;
            SpawnPeixe();
        }
        else if (input == -1)
        {
            peixe_atual--;
            if (peixe_atual < 0) peixe_atual = Peixes.Count - 1;
            SpawnPeixe();
        }
    }

    private void SpawnPeixe()
    {
        // Se não houver peixes no inventário, limpa a UI e sai da função
        if (Peixes.Count == 0)
        {
            if (peixeinstanciado != null) Destroy(peixeinstanciado);
            Nome_Text.text = "No fish encountered";
            Descri_Text.text = "Find fish deep in the asteroids to use as Food, Pets or Fuel";
            return;
        }

        if (peixeinstanciado != null)
            Destroy(peixeinstanciado);

        peixeinstanciado = Instantiate(Peixes[peixe_atual].PrefabVisual, peixe_transform);
        peixeinstanciado.transform.localScale = Vector3.one * 0.80f;

        FishInfo fish = Peixes[peixe_atual];
        Nome_Text.text = fish.Nome_do_Peixe;
        Descri_Text.text = fish.Descrição_do_Peixe;
    }

    public void UsarPeixe(int utilidade = 0)
    {
        if (utilidade == 2 && peixeinstanciado != null && Peixes.Count > 0)
        {
            // Substitui os 5 "if/else" repetitivos por um loop for limpo
            for (int i = 0; i < aqua.Peixes_Aquario.Length; i++)
            {
                if (aqua.Peixes_Aquario[i] == null)
                {
                    // Transfere o Prefab para o aquário
                    aqua.Peixes_Aquario[i] = Peixes[peixe_atual].PrefabVisual;

                    // Remove do inventário
                    Peixes.RemoveAt(peixe_atual);

                    // CORREÇÃO: Ajusta o índice de seleção ANTES de tentar renderizar a UI de novo
                    if (peixe_atual >= Peixes.Count)
                    {
                        peixe_atual = Peixes.Count - 1;
                    }
                    if (peixe_atual < 0) peixe_atual = 0;

                    // Atualiza o mundo físico e a UI do computador
                    aqua.SpawnPeixeAqua();
                    SpawnPeixe();

                    break; // Sai do loop já que encontrou uma vaga vazia
                }
            }
        }
    }
}