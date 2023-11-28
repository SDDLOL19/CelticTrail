using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static int cantidadEnemigosEnEscena, cantidadEnemigosMax;
    public static int rondaActual;
    [SerializeField] int[] cantidadEnemigosRonda;
    public static float timeToSpawn;
    [SerializeField] float spawnTime;

    bool puedeCambiarRonda = false;

    private void Start()
    {
        ActualizarTimeToSpawn();
        ActualizarRonda();
    }

    private void Update()
    {
        if (!GameManager.partidaAcabada)
        {
            if (cantidadEnemigosMax == 0 && puedeCambiarRonda)
            {
                Invoke("ActualizarRonda", 4f);
                puedeCambiarRonda = false; //Para que solo lo haga una vez
            }
        }
    }

    void ActualizarRonda()
    {
        rondaActual++;
        cantidadEnemigosMax = cantidadEnemigosRonda[rondaActual - 1];
        puedeCambiarRonda = true;
        
    }

    void ActualizarTimeToSpawn()
    {
        timeToSpawn = spawnTime;
    }
}
