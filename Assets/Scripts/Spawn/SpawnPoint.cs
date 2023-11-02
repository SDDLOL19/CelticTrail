using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject prefabEnemy;
    [SerializeField] float timeToSpawn, radioDeSpawn, enemigosQueSpawnean;
    float timeAux;

    void Start()
    {
        timeAux = timeToSpawn;
    }

    void Update()
    {
        if (SpawnManager.cantidadEnemigosEnEscena < SpawnManager.cantidadEnemigosRonda[SpawnManager.rondaActual])
        {
            TimerSpawnEnemy();
            if (timeAux <= 0)
            {
                SpawnEnemy();
                timeAux = timeToSpawn;
            }
        }       
    }

    //SPAWN ENEMIGOS
    void TimerSpawnEnemy()
    {
        timeAux -= Time.deltaTime;
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < enemigosQueSpawnean; i++)
        {
            Vector2 playerPosition = GameManager.player.transform.position; //detecta la posicion del jugador
            Vector2 randomPosicion = UnityEngine.Random.onUnitSphere * radioDeSpawn; // Random de distancia: genera una posicion random a una unidad de distancia (en todas direcciones x,y,z), se multiplica para que spawnee a la distancia que se quiera
            Vector2 enemyPosition = playerPosition + randomPosicion; //suma la posicion actual del player mas el random de distancia
            Instantiate(prefabEnemy, enemyPosition, Quaternion.identity); //spawnea el enemigo
        }
    }
}
