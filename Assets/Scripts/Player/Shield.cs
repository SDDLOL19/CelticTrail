using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] int vidaEscudo;
    [SerializeField] float tiempoMaxRecarga = 3, temporizadorRecarga;

    private void Start()
    {
        RecargarEscudo();
    }

    private void Update()
    {
        CuentaAtras();
    }

    public void QuitarEscudo()
    {
        vidaEscudo--;
    }

    void CuentaAtras()
    {
        temporizadorRecarga -= Time.deltaTime;

        if (temporizadorRecarga <= 0)
        {
            temporizadorRecarga = tiempoMaxRecarga;

            vidaEscudo += StatManager.cantidadEscudoRecuperada;
        }

        vidaEscudo = Mathf.Clamp(vidaEscudo, 0, StatManager.vidaEscudoMaxima);
    }

    void RecargarEscudo()
    {
        vidaEscudo = StatManager.vidaEscudoMaxima;
    }

    public float LeerEscudo()
    {
        return vidaEscudo;
    }

    public float LeerEscudoMaximo()
    {
        return StatManager.vidaEscudoMaxima;
    }
}
