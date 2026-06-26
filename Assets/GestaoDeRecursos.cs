using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GestaoDeRecursos : MonoBehaviour
{
    public static GestaoDeRecursos Instance;

    public Aquarium aqua;

    public static float Fome = 2;
    public static float Sani = 2;
    public static float Fuel = 10;

    public static float Gasolina = 100;

    public static int dia_num = 1;

    private bool sobreviveu = true;

    public TextMeshProUGUI dia_text;

    public Slider fome_Slider;
    public Slider sani_Slider;
    public Slider fuel_Slider;

    public GameObject gatoreact;

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
        if (Fome - 2 <= 0 && Sani - 2 <= 0)
        {
            resultados_text.text = "You lost!\r\n";
            sobreviveu = false;
        }
        else
        {
            resultados_text.text =
                "Hunger:" +
                "\r\n" + Fome + "/15 > " + (Fome - 2) + "/15" +
                "\r\nSanity:" +
                "\r\n" + Sani + "/15 > " + (Sani - 2) + "/15" +
                "\r\nFuel:" +
                "\r\n" + Fuel + "/100";
            sobreviveu = true;
        }

        Gasolina = 100;

        Fome =- 2;
        Sani = -2;

        aqua.ReduzirVida();

        if (gatoreact != null)
        {
            Animator anim = gatoreact.GetComponent<Animator>();

            if (sobreviveu)
            {
                anim.SetBool("Ganhou", true);
            }
            else if (!sobreviveu)
            {
                anim.SetBool("Ganhou", false);
            }
        }
    }

    public void ProximoDia()
    {
        if (sobreviveu == true)
        {
            dia_num += 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (sobreviveu == false)
        {
            dia_num = 1;
            SceneManager.LoadScene("Menu");
        }
    }
}