using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static int cantidadEnemigosEnEscena, cantidadEnemigosMax, rondaActual;
    [SerializeField] int[] cantidadEnemigosRonda;
    [SerializeField] float tiempoEscogido, radioEscogido;
    public static float timeToSpawn, radioDeSpawn;
    [SerializeField] SpawnArea[] areasDeSpawn;

    bool puedeCambiarRonda = false;
    float temporizadorEspecial;

    private void Start()
    {
        timeToSpawn = tiempoEscogido;
        radioDeSpawn = radioEscogido;
        temporizadorEspecial = tiempoEscogido + 0.2f;
        rondaActual = 0;
        ActualizarRonda();
    }

    private void Update()
    {
        temporizadorEspecial -= Time.deltaTime;

        if (!GameManager.partidaAcabada)
        {
            if (cantidadEnemigosMax == 0 && puedeCambiarRonda)
            {
                Invoke("ActualizarRonda", 4f);
                puedeCambiarRonda = false; //Para que solo lo haga una vez
            }

            if (temporizadorEspecial <= 0)
            {
                ControlarSpawnAreas();
                temporizadorEspecial = tiempoEscogido + 0.2f;
            }
        }
    }

    void ControlarSpawnAreas()
    {
        for (int i = 0; i < areasDeSpawn.Length; i++)
        {
            if (cantidadEnemigosEnEscena < cantidadEnemigosMax)
            {
                areasDeSpawn[i].CrearSpawnPoint();
                cantidadEnemigosEnEscena++;
            }
        }
    }

    void ActualizarRonda()
    {
        rondaActual++;
        cantidadEnemigosMax = cantidadEnemigosRonda[rondaActual - 1];
        puedeCambiarRonda = true;
        
    }
}
