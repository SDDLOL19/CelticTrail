using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioDeControles : MonoBehaviour
{
    public static KeyCode derecha = KeyCode.D, izquierda = KeyCode.A,  arriba = KeyCode.W, abajo = KeyCode.S;
    public  bool esperandoTecla = false;

    void OnGUI() //Se actualiza cada vez que hay un evento
    {
        //CambiarTecla(); 
    }

    void CambiarTecla(ref KeyCode teclaACambiar) //LO DEJÉ AQUÍ || Obligatoriamente tiene que hacerse en OnGUI || Hay que añadir la tecla que hay que cambiar por referencia
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

    void PermitirCambioDeTecla() //Permites que detecte la siguiente tecla
    {
        esperandoTecla = true;        
    }

    public void CambiarDerecha()
    {
        PermitirCambioDeTecla();
        CambiarTecla(ref derecha);
    }

    public void CambiarIzquierda()
    {
        PermitirCambioDeTecla();
        CambiarTecla(ref izquierda);
    }

    public void CambiarArriba()
    {
        PermitirCambioDeTecla();
        CambiarTecla(ref arriba);
    }

    public void CambiarAbajo()
    {
        PermitirCambioDeTecla();
        CambiarTecla(ref abajo);   
    }

    public void Cambiar()
    {

    }

}
