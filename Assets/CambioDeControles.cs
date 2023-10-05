using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioDeControles : MonoBehaviour
{
    public static KeyCode derecha = KeyCode.D;
    KeyCode teclaACambiar;
    public  bool esperandoTecla = false;

    void OnGUI() //Se actualiza cada vez que hay un evento
    {
        CambiarTecla();
    }

    void CambiarTecla()
    {
        if (esperandoTecla && Event.current.isKey && Event.current.type == EventType.KeyDown)
        {
            if (Event.current.keyCode != KeyCode.None)
            {
                Debug.Log(Event.current.keyCode);
                teclaACambiar = Event.current.keyCode;
                esperandoTecla = false;
            }
        }
    }

    void PermitirCambioDeTecla()
    {
        esperandoTecla = true;
    }

    public void CambiarDerecha()
    {
        teclaACambiar = derecha; //Se añade la tecla que quieres cambiar
        PermitirCambioDeTecla(); //Permites que detecte la siguiente tecla
    }

}
