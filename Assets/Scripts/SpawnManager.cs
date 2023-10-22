using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //SPAWN ENEMIGOS
    [SerializeField] GameObject prefabEnemy;
    [SerializeField] float timeSpawn, distancePlayer, enemigosSpawneados;
    float timeAux;

    void Start()
    {
        timeAux = timeSpawn;
    }

    void Update()
    {
        TimerSpawnEnemy();
        if (timeSpawn <= 0)
        {
            SpawnEnemy();
            timeSpawn = timeAux;
        }
    }

    //SPAWN ENEMIGOS
    private void TimerSpawnEnemy()
    {
        timeSpawn -= Time.deltaTime;
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < enemigosSpawneados; i++)
        {
            Vector2 playerPosition = GameManager.player.transform.position; //detecta la posicion del jugador
            Vector2 randomPosicion = UnityEngine.Random.onUnitSphere * distancePlayer; // Random de distancia: genera una posicion random a una unidad de distancia (en todas direcciones x,y,z), se multiplica para que spawnee a la distancia que se quiera
            Vector2 enemyPosition = playerPosition + randomPosicion; //suma la posicion actual del player mas el random de distancia
            Instantiate(prefabEnemy, enemyPosition, Quaternion.identity); //spawnea el enemigo
        }
    }
}
