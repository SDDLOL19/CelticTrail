using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject[] prefabEnemy;
    int numeroEnemigo, tiroDeDado; 

    public void SpawnEnemy()
        //ENEMIGOS: 0 ARQUERO, 1 ASESINO. 2 PHOOKA, 3 HADA, 4 FACHEN
    {
        tiroDeDado = Random.Range(1, 100);

        if (tiroDeDado <= 20)  //Arquero
        {
            numeroEnemigo = 0;
        }

        else if (tiroDeDado > 20 && tiroDeDado <= 50)                //Asesino
        {
            numeroEnemigo = 1;
        }

        else if (tiroDeDado > 50 && tiroDeDado <= 70)                //Phooka
        {
            numeroEnemigo = 2;
        }

        else if (tiroDeDado > 70 && tiroDeDado <= 90)                //Fachen
        {
            numeroEnemigo = 4;
        }

        else                                                        //Hada
        {
            numeroEnemigo = 3;
        }

        Instantiate(prefabEnemy[numeroEnemigo], transform.position, Quaternion.identity); //spawnea el enemigo
        Destroy(this.gameObject);
    }
}
