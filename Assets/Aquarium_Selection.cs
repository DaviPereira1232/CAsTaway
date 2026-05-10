using TMPro;
using UnityEngine;

public class Aquarium_Selection : MonoBehaviour
{
    public GameObject[] Peixes;
    public Transform Transform_Peixe;
    public int Peixe_Atual;
    public TextMeshProUGUI Nome_Text;
    public TextMeshProUGUI Descri_Text;

    private GameObject peixeinstanciado;
    public ScreenSwitching screenScript;
    public CatShipMovement catScript;
    private FishData info_Peixe;

    public void Start()
    {

        Peixe_Atual = 0;

        peixeinstanciado = Instantiate(Peixes[Peixe_Atual], Transform_Peixe);
    }
    private void Update()
    {
        info_Peixe = peixeinstanciado.GetComponent<FishData>();

        if (info_Peixe != null)
        {
            Nome_Text.text = info_Peixe.Nome_do_Peixe;
            Descri_Text.text = info_Peixe.Descrição_do_Peixe;
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
    public void Voltar()
    {
        screenScript.currentPos = 0;
        catScript.CanPlayerWalk = true;
    }
}
