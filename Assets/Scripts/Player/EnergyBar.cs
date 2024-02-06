using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBar : MonoBehaviour
{
    [SerializeField] float energiaMax, energiaRestada = 1, energiaSumada = 1, multiplicadorVelocidad = 2;
    float energiaActual;
    [HideInInspector] public float velocidadActual = 1;
    bool puedoCorrer, estoyCorriendo;

    private void Start()
    {
        velocidadActual = 1;
        energiaActual = energiaMax;
        puedoCorrer = true;
        estoyCorriendo = false;
    }

    private void Update()
    {
        if (energiaActual <= 0)
        {
            puedoCorrer = false;
            PararTurbo();
        }

        if (!estoyCorriendo)
        {
            velocidadActual = 1;
            RecargarEnergia();

            if (energiaActual >= energiaMax)
            {
                puedoCorrer = true;
            }
        }

        else
        {
            energiaActual -= energiaRestada * Time.deltaTime;
        }
    }

    public void UsarTurbo()
    {
        if (puedoCorrer)
        {
            velocidadActual = multiplicadorVelocidad;
            estoyCorriendo = true;
        }
    }

    public void PararTurbo()
    {
        estoyCorriendo = false;
    }

    void RecargarEnergia()
    {
        if (energiaActual < energiaMax)
        {
            energiaActual += energiaSumada * Time.deltaTime;
        }
    }

}
