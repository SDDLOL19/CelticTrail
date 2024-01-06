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

        if (tiroDeDado <= 25)  //Asesino
        {
            numeroEnemigo = 1;
        }

        else                   //Arquero
        {
            numeroEnemigo = 0;
        }

        Instantiate(prefabEnemy[numeroEnemigo], transform.position, Quaternion.identity); //spawnea el enemigo
        Destroy(this.gameObject);
    }
}
