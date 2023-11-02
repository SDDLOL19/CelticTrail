using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static int cantidadEnemigosEnEscena, cantidadEnemigosMax, cantidadEnemigosEliminados;
    public static int rondaActual;
    public int[] cantidadEnemigosRonda;
    public static float timeToSpawn;

    private void Start()
    {
        ActualizarRonda();
    }

    private void Update()
    {
        if (cantidadEnemigosEliminados == cantidadEnemigosMax)
        {
            cantidadEnemigosEliminados = 0;
            Invoke("ActualizarRonda", 4f);
        }
    }

    void ActualizarRonda()
    {
        rondaActual++;
        cantidadEnemigosMax = cantidadEnemigosRonda[rondaActual];
    }
}
