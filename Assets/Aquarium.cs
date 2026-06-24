using TMPro;
using UnityEngine;

public class Aquarium : MonoBehaviour
{
    private GameObject[] Peixes_Aquario;
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

        if (usado == null || usado.Length != Transform_Peixes.Length)
            usado = new bool[Transform_Peixes.Length];

        if (textos == null || textos.Length != Transform_Peixes.Length)
            textos = new TextMeshPro[Transform_Peixes.Length];

        for (int i = 0; i < balao.Length; i++)
        {
            if (balao[i] == null && Transform_Peixes[i] != null && Transform_Peixes[i].childCount > 0)
            {
                balao[i] = Transform_Peixes[i].GetChild(0).gameObject;
            }
            usado[i] = false;
        }

        VerificarSeEstaCheio(); // Checa uma vez no início
    }

    // Removido o Update pesado. Criamos um método focado.
    public void VerificarSeEstaCheio()
    {
        if (Peixes_Aquario == null) return;

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
            if (Peixes_Aquario[i] != null && Peixes_Aquario[i].GetComponent<FishData>().vida > 0)
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
        VerificarSeEstaCheio();
    }

    public void FalarComPeixe(int qualpeixe)
    {
        if (qualpeixe < 0 || qualpeixe >= Peixes_Aquario.Length)
        {
            FecharTodosOsBaloes();
            return;
        }

        if (Peixes_Aquario[qualpeixe] == null) return;

        if (!usado[qualpeixe])
        {
            FishData data = Peixes_Aquario[qualpeixe].GetComponent<FishData>();
            if (data != null && gestao != null)
            {
                gestao.AlterarSani(data.Sani);
            }
            usado[qualpeixe] = true;
        }

        // Ativação do Balão e Animação
        if (balao[qualpeixe] != null)
        {
            balao[qualpeixe].SetActive(true);

            if (balao[qualpeixe].TryGetComponent<Animation>(out Animation anim))
            {
                anim.Play();
            }

            // Atualiza o texto do balão dinamicamente baseado no FishData do peixe
            Transform child = balao[qualpeixe].transform.GetChild(0);
            if (child != null && child.TryGetComponent<TextMeshPro>(out TextMeshPro tmp))
            {
                textos[qualpeixe] = tmp;
                FishData data = Peixes_Aquario[qualpeixe].GetComponent<FishData>();

                if (data != null && data.Dialogos != null && data.Dialogos.Length > qualpeixe)
                {
                    // Atribui o texto correto (Nota: use o índice correto do diálogo se necessário)
                    tmp.text = data.Dialogos[qualpeixe];
                }
            }
        }
    }
    public void ReduzirVida()
    {
        if (Player_Save.Instance == null || Player_Save.Instance.Peixes_Aquario == null) return;

        for (int i = 0; i < Player_Save.Instance.Peixes_Aquario.Length; i++)
        {
            if (Player_Save.Instance.Peixes_Aquario[i] != null)
            {
                FishData data = Player_Save.Instance.Peixes_Aquario[i].GetComponent<FishData>();
                if (data != null)
                {
                    data.vida -= 1;

                    if (data.vida <= 0 && inst_peixes[i] != null)
                    {
                        Destroy(inst_peixes[i]);
                        VerificarSeEstaCheio();
                    }
                }
            }
        }
    }
    private void FecharTodosOsBaloes()
    {
        for (int i = 0; i < balao.Length; i++)
        {
            if (balao[i] != null)
            {
                balao[i].SetActive(false);
            }
        }
    }
}