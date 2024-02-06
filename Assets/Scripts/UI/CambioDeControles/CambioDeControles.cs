using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

unsafe public class CambioDeControles : MonoBehaviour
{
    public bool esperandoTecla = false;

    [SerializeField] TextMeshPro[] textosBotones;

    int textoQueCambia;
    string textoNuevo;

    KeyCode* teclaQueCambia;

    void OnGUI() //Se actualiza cada vez que hay un evento
    {
        CambiarTecla(); 
    }

    private void Update()
    {
        textosBotones[textoQueCambia].text = textoNuevo;
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

        fixed (KeyCode* tecla = &GameManager.botonMovimientoDerecha) 
        {
            teclaQueCambia = tecla;
        }

        CambiarTexto(0, GameManager.botonMovimientoDerecha);
    }

    public void CambiarIzquierda()
    {
        PermitirCambioDeTecla();

        fixed (KeyCode* tecla = &GameManager.botonMovimientoIzquierda)
        {
            teclaQueCambia = tecla;
        }

        CambiarTexto(1, GameManager.botonMovimientoIzquierda);
    }

    public void CambiarArriba()
    {
        PermitirCambioDeTecla();

        fixed (KeyCode* tecla = &GameManager.botonMovimientoArriba)
        {
            teclaQueCambia = tecla;
        }
    }

    public void CambiarAbajo()
    {
        PermitirCambioDeTecla();

        fixed (KeyCode* tecla = &GameManager.botonMovimientoAbajo)
        {
            teclaQueCambia = tecla;
        }
    }

    public void CambiarTurbo()
    {
        PermitirCambioDeTecla();

        fixed (KeyCode* tecla = &GameManager.botonUsarTurbo)
        {
            teclaQueCambia = tecla;
        }
    }

    public void Cambiar()
    {

    }

    void CambiarTexto(int valor, KeyCode tecla)
    {
        textoQueCambia = valor;
        textoNuevo = tecla.ToString();
    }

    public void SalirDelMenu()
    {
        this.gameObject.SetActive(false);
    }

}
