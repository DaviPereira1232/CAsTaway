using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GestãoDeRecursos : MonoBehaviour
{
    public static float Fome = 2;
    public static float Sani = 2;
    public static float Fuel = 10;

    public static int dia_num = 1;

    public TextMeshProUGUI dia_text;

    public Slider fome_Slider;
    public Slider sani_Slider;
    public Slider fuel_Slider;

    public TextMeshProUGUI resultados_text;

    void Start()
    {
        fome_Slider.value = Fome;
        sani_Slider.value = Sani;
        fuel_Slider.value = Fuel;

        dia_text.text = ("Day " + dia_num);
    }

    public void AlterarFome(float valor)
    {
        Fome += valor;
        fome_Slider.value = Fome;
    }

    public void AlterarSani(float valor)
    {
        Sani += valor;
        sani_Slider.value = Sani;
    }

    public void AlterarFuel(float valor)
    {
        Fuel += valor;
        fuel_Slider.value = Fuel; // fixed bug
    }

    public void Resultados()
    {
        resultados_text.text = "Hunger:\r\n" + Fome + "/15\r\nSanity:\r\n" + Sani + "/15\r\nFuel:\r\n" + Fuel + "/100";
    }
}