using UnityEngine;
using UnityEngine.UI;

public class GestãoDeRecursos : MonoBehaviour
{
    private float Fome;
    private float Sani;
    private float Fuel;

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
         Fome = Fome + valor;
         fome_Slider.value = Fome;
    }
    public void AlterarSani(float valor)
    {
        Sani = Sani + valor;
        sani_Slider.value = Sani;
    }
    public void AlterarFuel(float valor)
    {
        Fuel = Fuel + valor;
        fuel_Slider.value = Fome;
    }
}
