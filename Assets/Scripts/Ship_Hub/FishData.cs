using UnityEngine;

public class FishData : MonoBehaviour
{
    public string Nome_do_Peixe;
    public string Descrição_do_Peixe;

    public bool Recurso_Gaso;
    public bool Recurso_Sani;
    public bool Recurso_Fome;

    public GameObject PrefabVisual;

}

[System.Serializable]
public class FishInfo
{
    public string Nome_do_Peixe;
    public string Descrição_do_Peixe;

    public bool Recurso_Gaso;
    public bool Recurso_Sani;
    public bool Recurso_Fome;

    public GameObject PrefabVisual;

    public FishInfo(FishData peixe)
    {
        Nome_do_Peixe = peixe.Nome_do_Peixe;
        Descrição_do_Peixe = peixe.Descrição_do_Peixe;

        Recurso_Gaso = peixe.Recurso_Gaso;
        Recurso_Sani = peixe.Recurso_Sani;
        Recurso_Fome = peixe.Recurso_Fome;

        PrefabVisual = peixe.PrefabVisual;
    }
}
