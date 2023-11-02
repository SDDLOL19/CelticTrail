using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject prefabEnemy;
    [SerializeField] float radioDeSpawn, enemigosQueSpawnean;
    float contadorTiempoSpawn; //Contador individual de cada spawner

    void Start()
    {
        contadorTiempoSpawn = SpawnManager.timeToSpawn;
    }

    void Update()
    {
        if (SpawnManager.cantidadEnemigosEnEscena < SpawnManager.cantidadEnemigosMax)
        {
            TimerSpawnEnemy();
            if (contadorTiempoSpawn <= 0)
            {
                SpawnEnemy();
                contadorTiempoSpawn = SpawnManager.timeToSpawn;
            }
        }
    }

    void TimerSpawnEnemy()
    {
        contadorTiempoSpawn -= Time.deltaTime;
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < enemigosQueSpawnean; i++)
        {
            Vector2 randomPosicion = UnityEngine.Random.onUnitSphere * radioDeSpawn; // Random de distancia: genera una posicion random a una unidad de distancia (en todas direcciones x,y,z), se multiplica para que spawnee a la distancia que se quiera
            Vector2 enemyPosition = new Vector2(transform.position.x, transform.position.y) + randomPosicion; //suma la posicion actual del player mas el random de distancia
            Instantiate(prefabEnemy, enemyPosition, Quaternion.identity); //spawnea el enemigo
        }
    }
}
