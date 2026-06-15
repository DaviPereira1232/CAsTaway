using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Aquarium : MonoBehaviour
{
    public GameObject[] Peixes_Aquario;
    public Transform[] Transform_Peixes;
    public GameObject[] balão;
    private GameObject[] inst_peixes;
    public bool cheio;

    void Start()
    {
        inst_peixes = new GameObject[Peixes_Aquario.Length];
        SpawnPeixeAqua();

        for (int i = 0; i < balão.Length; i++)
        {
            if (balão[i] == null)
            {
                balão[i] = Transform_Peixes[i].transform.GetChild(0).gameObject;
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
        
        if (qualpeixe >= 0 && qualpeixe <= 4 && Peixes_Aquario[qualpeixe] != null)
        {
            balão[qualpeixe].SetActive(true);
            balão[qualpeixe].GetComponent<Animation>().Play();
            string textoAtual = "";
            string texto = balão[qualpeixe].transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text;
            for (int i = 0; i < texto.Length; i++)
            {
                textoAtual = texto.Substring(0, i);
            }
        }
        else if (qualpeixe == 6)
        {
            balão[0].SetActive(false);
            balão[1].SetActive(false);
            balão[2].SetActive(false);
            balão[3].SetActive(false);
            balão[4].SetActive(false);
        }
    }
}