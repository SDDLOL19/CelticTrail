using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] int vidaEscudo;
    [SerializeField] float tiempoMaxRecarga = 3, tiempoMaxEscudoRoto = 3;
    float temporizadorRecarga, temporizadorEscudoRoto;

    bool puedoEscudear;

    [SerializeField] Head player;

    private void Start()
    {
        RecargarEscudo();

        temporizadorRecarga = tiempoMaxRecarga;
        temporizadorEscudoRoto = tiempoMaxEscudoRoto;
        puedoEscudear = true;
    }

    private void Update()
    {
        if (vidaEscudo < 0)
        {
            puedoEscudear = false;
        }

        CuentaAtras();
    }

    public void VaciarEscudo()
    {
        vidaEscudo = 0;
        player.AnimacionEscudo();
    }

    public void QuitarEscudo()
    {
        vidaEscudo--;
        player.AnimacionEscudo();
    }

    void CuentaAtras()
    {
        if (puedoEscudear == true)
        {
            temporizadorRecarga -= Time.deltaTime;
        }

        else
        {
            temporizadorEscudoRoto -= Time.deltaTime;
        }

        if (temporizadorEscudoRoto <= 0)
        {
            temporizadorEscudoRoto = tiempoMaxEscudoRoto;
            puedoEscudear = true;
            temporizadorRecarga = tiempoMaxRecarga;
        }
        
        if (temporizadorRecarga <= 0)
        {
            temporizadorRecarga = tiempoMaxRecarga;

            vidaEscudo += StatManager.cantidadEscudoRecuperada;

            player.AnimacionEscudo();
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
