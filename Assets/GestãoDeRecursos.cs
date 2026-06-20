using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class GestãoDeRecursos : MonoBehaviour
{
    public float Fome;
    public float Sani;
    public float Fuel;

    public Slider fome_Slider;
    public Slider sani_Slider;
    public Slider fuel_Slider;

    private void Awake()
    {
        Fome = 2;
        Sani = 2;
        Fuel = 10; 
    }
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
