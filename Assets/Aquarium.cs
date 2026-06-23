using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Aquarium : MonoBehaviour
{
    GameObject[] Peixes_Aquario;
    public Transform[] Transform_Peixes;
    public GameObject[] balao;
    public TextMeshPro[] textos;
    private GameObject[] inst_peixes;
    public bool[] usado;
    public bool cheio;

    public GestaoDeRecursos gestao;

    void Start()
    {

        if (Player_Save.Instance != null)
        {
            Peixes_Aquario = Player_Save.Instance.Peixes_Aquario;
            inst_peixes = new GameObject[Peixes_Aquario.Length];
            SpawnPeixeAqua();
        }

        for (int i = 0; i < balao.Length; i++)
        {
            if (balao[i] == null)
            {
                balao[i] = Transform_Peixes[i].transform.GetChild(0).gameObject;
                usado[i] = false;
            }
        }


    }

    public void Update()
    {
        bool espacoVazioEncontrado = false;
        for (int i = 0; i < Peixes_Aquario.Length; i++)
        {
            if (Peixes_Aquario[i] == null)
            {
                espacoVazioEncontrado = true;
                break;
            }
        }
        cheio = !espacoVazioEncontrado;
    }

    public void SpawnPeixeAqua()
    {
        for (int i = 0; i < Peixes_Aquario.Length; i++)
        {
            if (Peixes_Aquario[i] != null)
            {
                if (inst_peixes[i] != null)
                {
                    Destroy(inst_peixes[i]);
                }

                inst_peixes[i] = Instantiate(
                    Peixes_Aquario[i],
                    Transform_Peixes[i].position,
                    Transform_Peixes[i].rotation,
                    Transform_Peixes[i]
                );
            }
        }
    }

    public void FalarComPeixe (int qualpeixe)
    {
        if (usado[qualpeixe] == false)
        {
            gestao.AlterarSani(Peixes_Aquario[qualpeixe].GetComponent<FishData>().Sani);
        }

        usado[qualpeixe] = true;

        if (qualpeixe >= 0 && qualpeixe <= 4 && Peixes_Aquario[qualpeixe] != null)
        {
            balao[qualpeixe].SetActive(true);
            balao[qualpeixe].GetComponent<Animation>().Play();
            string textoAtual = "";
            string texto = balao[qualpeixe].transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text;
            for (int i = 0; i < texto.Length; i++)
            {
                textoAtual = texto.Substring(0, i);
            }
        }
        else if (qualpeixe == 6)
        {
            balao[0].SetActive(false);
            balao[1].SetActive(false);
            balao[2].SetActive(false);
            balao[3].SetActive(false);
            balao[4].SetActive(false);
        }

        for (int i = 0; i < balao.Length; i++)
        {
            textos[i] = balao[i].transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();

            if (inst_peixes[i] != null && textos[i] != null)
            {
                textos[i].text = Peixes_Aquario[i].GetComponent<FishData>().Dialogos[Random.Range(0, Peixes_Aquario[i].GetComponent<FishData>().Dialogos.Length)];
            }
        }   
    }

}