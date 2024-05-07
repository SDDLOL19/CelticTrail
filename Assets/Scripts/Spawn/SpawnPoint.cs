using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject[] prefabEnemy;
    int numeroEnemigo, tiroDeDado; 

    public void SpawnEnemy()
    {
        tiroDeDado = Random.Range(1, 100);

        if (tiroDeDado <= 20)  //Asesino
        {
            numeroEnemigo = 1;
        }

        /* //mi intento de poner los enemigos firmado: Darío
        else if (tiroDeDado > 20 && tiroDeDado < 40) //phooka
        {
            numeroEnemigo = 2;
        }

        else if (tiroDeDado > 40 && tiroDeDado < 60) //Hada
        {
            numeroEnemigo = 3;
        }

        else if (tiroDeDado > 60 && tiroDeDado <= 80) //Fachen
        {
            numeroEnemigo = 4;
        }
        */

        else                   //Arquero
        {
            numeroEnemigo = 0;
        }

        Instantiate(prefabEnemy[numeroEnemigo], transform.position, Quaternion.identity); //spawnea el enemigo
        Destroy(this.gameObject);
    }
}
