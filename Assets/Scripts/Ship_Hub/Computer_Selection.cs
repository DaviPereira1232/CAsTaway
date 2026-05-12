using UnityEngine;

public class Computer_Selection : MonoBehaviour
{
    public ScreenSwitching screenScript;
    public CatShipMovement catScript;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Voltar()
    {
        screenScript.currentPos = 0;
        catScript.CanPlayerWalk = true;
    }
}
