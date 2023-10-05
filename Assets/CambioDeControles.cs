using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioDeControles : MonoBehaviour
{
    public static KeyCode izquierda = KeyCode.A, derecha = KeyCode.D, arriba = KeyCode.W, abajo = KeyCode.S;

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

    void PermitirCambioDeTecla(KeyCode tecla) //Permites que detecte la siguiente tecla || Hay que añadir la tecla que hay que cambiar
    {
        teclaACambiar = tecla; //Se añade la tecla que quieres cambiar
        esperandoTecla = true;
    }

    public void CambiarDerecha()
    {       
        PermitirCambioDeTecla(derecha);
    }

    public void CambiarIzquierda()
    {
        PermitirCambioDeTecla(izquierda);
    }

    public void CambiarArriba()
    {
        PermitirCambioDeTecla(arriba);
    }

    public void CambiarAbajo()
    {
        PermitirCambioDeTecla(abajo);
    }

    public void Cambiar()
    {

    }

}
