using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] int vidaEscudo, escudoMaximo = 5, cantidadRecuperada = 1;
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

            vidaEscudo += cantidadRecuperada;
        }

        vidaEscudo = Mathf.Clamp(vidaEscudo, 0, escudoMaximo);
    }

    void RecargarEscudo()
    {
        vidaEscudo = escudoMaximo;
    }

    public float LeerEscudo()
    {
        return vidaEscudo;
    }

    public float LeerEscudoMaximo()
    {
        return escudoMaximo;
    }
}
