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

        //if (tiroDeDado <= 20)  //Asesino
        //{
        //    numeroEnemigo = 3;
        //}

        //else if (tiroDeDado > 20 && tiroDeDado <= 40)                //Arquero
        //{
        //    numeroEnemigo = 3;
        //}

        //else
        //{
        //    numeroEnemigo = 3;
        //}

        numeroEnemigo = 3;

        Instantiate(prefabEnemy[numeroEnemigo], transform.position, Quaternion.identity); //spawnea el enemigo
        Destroy(this.gameObject);
    }
}
