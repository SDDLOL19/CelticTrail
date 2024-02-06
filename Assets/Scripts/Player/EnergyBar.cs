using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBar : MonoBehaviour
{
    [SerializeField] float duracionEnergia, multiplicadorReduccionEnergia = 1, multiplicadorRegeneracionEnergia = 1;
    float temporizadorRecarga, temporizadorSinEnergia;
    float energiaActual;

    public float multiplicadorVelocidad = 2;
    bool puedoCorrer, estoyCorriendo;

    [SerializeField] Head player;

    private void Start()
    {
        energiaActual = duracionEnergia;
        puedoCorrer = true;
    }

    private void Update()
    {
        if (energiaActual <= 0)
        {
            puedoCorrer = false;
            estoyCorriendo = false;
            RecargarEnergia();
            Debug.Log("Energia:" + energiaActual);
        }
    }

    public void UsarTurbo()
    {
        if (puedoCorrer)
        {
            energiaActual -= multiplicadorReduccionEnergia * Time.deltaTime;
            player.playerSpeed += multiplicadorVelocidad * Time.deltaTime;
            estoyCorriendo = true;
        }
    }

    void RecargarEnergia()
    {
        if (energiaActual < duracionEnergia && estoyCorriendo)
        {
            energiaActual += multiplicadorRegeneracionEnergia * Time.deltaTime;           
        }
    }

}
