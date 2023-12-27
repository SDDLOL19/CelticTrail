using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    [SerializeField] GameObject prefabSpawnPoint;
    float contadorTiempoSpawn; //Contador individual de cada spawner
    bool puedoSpawn;

    void Start()
    {
        contadorTiempoSpawn = SpawnManager.timeToSpawn;
        puedoSpawn = false;
    }

    void Update()
    {
        TimerSpawnPoint();

        if (!GameManager.partidaAcabada)
        {
            if (contadorTiempoSpawn <= 0 && puedoSpawn)
            {
                escogerPosicionSpawnpoint();
                contadorTiempoSpawn = SpawnManager.timeToSpawn;
                puedoSpawn = false;
            }
        }
    }

    public void CrearSpawnPoint()
    {
        contadorTiempoSpawn = SpawnManager.timeToSpawn;
        puedoSpawn = true;
    }

    void escogerPosicionSpawnpoint()
    {
        Vector2 randomPosicion = UnityEngine.Random.onUnitSphere * SpawnManager.radioDeSpawn; // Random de distancia: genera una posicion random a una unidad de distancia (en todas direcciones x,y,z), se multiplica para que spawnee a la distancia que se quiera
        Vector2 enemyPosition = new Vector2(transform.position.x, transform.position.y) + randomPosicion; //suma la posicion actual del player mas el random de distancia
        Instantiate(prefabSpawnPoint, enemyPosition, Quaternion.identity); //spawnea el enemigo
    }

    void TimerSpawnPoint()
    {
        contadorTiempoSpawn -= Time.deltaTime;
    }
}
