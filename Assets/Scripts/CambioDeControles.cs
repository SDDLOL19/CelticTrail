using System.Collections;
using System.Collections.Generic;
using UnityEngine;

unsafe public class CambioDeControles : MonoBehaviour
{
    public bool esperandoTecla = false;

    KeyCode* teclaQueCambia;

    void OnGUI() //Se actualiza cada vez que hay un evento
    {
        CambiarTecla(); 
    }

    void CambiarTecla() //Obligatoriamente tiene que llamarse en OnGUI || Hay que añadir la tecla que hay que cambiar por puntero
    {
        if (esperandoTecla && Event.current.isKey && Event.current.type == EventType.KeyDown)
        {
            if (Event.current.keyCode != KeyCode.None)
            {
                Debug.Log(Event.current.keyCode);
                *teclaQueCambia = Event.current.keyCode;
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

        fixed (KeyCode* tecla = &GameManager.movimientoDerecha) 
        {
            teclaQueCambia = tecla;
        }
    }

    public void CambiarIzquierda()
    {
        PermitirCambioDeTecla();

        fixed (KeyCode* tecla = &GameManager.movimientoIzquierda)
        {
            teclaQueCambia = tecla;
        }
    }

    public void CambiarArriba()
    {
        PermitirCambioDeTecla();

        fixed (KeyCode* tecla = &GameManager.movimientoArriba)
        {
            teclaQueCambia = tecla;
        }
    }

    public void CambiarAbajo()
    {
        PermitirCambioDeTecla();

        fixed (KeyCode* tecla = &GameManager.movimientoAbajo)
        {
            teclaQueCambia = tecla;
        }
    }

    public void Cambiar()
    {

    }

    public void SalirDelMenu()
    {
        this.gameObject.SetActive(false);
    }

}
