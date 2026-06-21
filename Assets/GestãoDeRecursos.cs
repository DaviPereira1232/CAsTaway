using UnityEngine;
using UnityEngine.UI;

public class GestãoDeRecursos : MonoBehaviour
{
    public static float Fome = 2;
    public static float Sani = 2;
    public static float Fuel = 10;

    public Slider fome_Slider;
    public Slider sani_Slider;
    public Slider fuel_Slider;

    void Start()
    {
        fome_Slider.value = Fome;
        sani_Slider.value = Sani;
        fuel_Slider.value = Fuel;
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
}