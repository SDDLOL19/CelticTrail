using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject prefabEnemy, player;
    [SerializeField] float timeSpawn, distancePlayer, enemigosSpawneados;
    float timeAux;

    private void Start()
    {
        timeAux = timeSpawn;
    }
    void Update()
    {
        Error();
        Timer();
        if (timeSpawn <= 0)
        {
            SpawnEnemy();
            timeSpawn = timeAux;
        }
    }
    private void Timer()
    {
        timeSpawn -= Time.deltaTime;
    }
    void SpawnEnemy()
    {
        for (int i = 0; i < enemigosSpawneados; i++)
        {
            Vector2 playerPosition = player.transform.position; //detecta la posicion del jugador
            Vector2 randomPosicion = Random.onUnitSphere * distancePlayer; // Random de distancia: genera una posicion random a una unidad de distancia (en todas direcciones x,y,z), se multiplica para que spawnee a la distancia que se quiera
            Vector2 enemyPosition = playerPosition + randomPosicion; //suma la posicion actual del personaje mas el random de distancia
            Instantiate(prefabEnemy, enemyPosition, Quaternion.identity); //spawnea el enemigo
        }
    }
    void Error()
    {
        if (player == null)
        {
            Debug.LogError("Player no asignado! //cambiar en GameManager");
            return;
        }
        if (prefabEnemy == null)
        {
            Debug.LogError("Enemy no asignado! //cambiar en GameManager");
            return;
        }
        if (enemigosSpawneados == 0)
        {
            Debug.LogError("Enemigos spawneados: 0! //cambiar en GameManager");
            return;
        }
        if (distancePlayer == 0)
        {
            Debug.LogError("Distancia enemigos: 0! //cambiar en GameManager");
            return;
        }
        if (timeSpawn == 0)
        {
            Debug.LogError("Tiempo espera: 0! //cambiar en GameManager");
            return;
        }
    }

    //perdida de tiempo
    //poco funcional(muy largo) ademas no spawnea bien (no se porque, si elimino la distancia al jugador funciona si no, no)

    //[SerializeField] float distanciaXderecha, distanciaXizquierda, distanciaYarriba, distanciaYabajo;
    //[SerializeField] float derechaX, izquierdaX, arribaY, abajoY;
    //float playerPositionY, playerPositionX;
    //float arribaOAbajo, izquierdaODerecha;
    //Transform detectPlayerPosition;

    //void SpawnArriba()
    //{
    //    izquierdaODerecha = Random.Range(-1, 1);
    //    if (izquierdaODerecha > 0) //spawnea a la derecha del personaje
    //    {
    //        float posicionArriba = Random.Range(playerPositionX + distanciaXderecha, Mathf.Infinity);
    //        Instantiate(prefabEnemy, new Vector2(posicionArriba, playerPositionY + arribaY), Quaternion.identity);
    //    }
    //    else if (izquierdaODerecha <= 0) //spawnea a la izquierda del personaje
    //    {
    //        float posicionArriba = Random.Range(-Mathf.Infinity, playerPositionX + distanciaXderecha);
    //        Instantiate(prefabEnemy, new Vector2(posicionArriba, playerPositionY + arribaY), Quaternion.identity);
    //    }
    //}
    //void SpawnAbajo()
    //{
    //    izquierdaODerecha = Random.Range(-1, 1);
    //    if (izquierdaODerecha > 0) //spawnea a la derecha del personaje
    //    {
    //        float posicionAbajo = Random.Range(playerPositionX + distanciaXizquierda, Mathf.Infinity);
    //        Instantiate(prefabEnemy, new Vector2(posicionAbajo, playerPositionY + abajoY), Quaternion.identity);
    //    }
    //    else if (izquierdaODerecha <= 0) //spawnea a la izquierda del personaje
    //    {
    //        float posicionAbajo = Random.Range(-Mathf.Infinity, playerPositionX + distanciaXizquierda);
    //        Instantiate(prefabEnemy, new Vector2(posicionAbajo, playerPositionY + abajoY), Quaternion.identity);
    //    }
    //}
    //void SpawnDerecha()
    //{
    //    arribaOAbajo = Random.Range(-1, 1);
    //    if (arribaOAbajo > 0) //spawnea por encima del personaje
    //    {
    //        float posicionDerecha = Random.Range(playerPositionY + distanciaYarriba, Mathf.Infinity);
    //        Instantiate(prefabEnemy, new Vector2(playerPositionX + derechaX, posicionDerecha), Quaternion.identity);
    //    }
    //    else if (arribaOAbajo <= 0) //spawnea por debajo del personaje 
    //    {
    //        float posicionDerecha = Random.Range(-Mathf.Infinity, playerPositionY + distanciaYabajo);
    //        Instantiate(prefabEnemy, new Vector2(playerPositionX + derechaX, posicionDerecha), Quaternion.identity);
    //    }
    //}
    //void SpawnIzquierda()
    //{
    //    arribaOAbajo = Random.Range(-1, 1);
    //    if (arribaOAbajo > 0) //spawnea por encima del personaje
    //    {
    //        float posicionIzquierda = Random.Range(playerPositionY + distanciaYarriba, Mathf.Infinity);
    //        Instantiate(prefabEnemy, new Vector2(playerPositionX + izquierdaX, posicionIzquierda), Quaternion.identity);
    //    }
    //    else if (arribaOAbajo <= 0) //spawnea por debajo del personaje 
    //    {
    //        float posicionIzquierda = Random.Range(-Mathf.Infinity, playerPositionY + distanciaYabajo);
    //        Instantiate(prefabEnemy, new Vector2(playerPositionX + izquierdaX, posicionIzquierda), Quaternion.identity);
    //    }
    //}
    //void SpawnCompleto()
    //{
    //    float eleccionSpawn = Random.Range(1, 5);
    //    if (eleccionSpawn > 1 && eleccionSpawn < 2)
    //        SpawnArriba();
    //    else if (eleccionSpawn > 2 && eleccionSpawn < 3)
    //        SpawnAbajo();
    //    else if (eleccionSpawn > 3 && eleccionSpawn < 4)
    //        SpawnDerecha();
    //    else if (eleccionSpawn > 4 && eleccionSpawn <= 5)
    //        SpawnIzquierda();
    //}
    //void PlayerPosition()
    //{
    //    detectPlayerPosition = GameObject.Find("Player").transform;
    //    Vector2 playerPosition = detectPlayerPosition.position;
    //    playerPositionY = playerPosition.y;
    //    playerPositionX = playerPosition.x;
    //    Debug.Log("Posición del objeto: " + playerPosition);
    //    Debug.Log("Posición y: " + playerPositionY);
    //    Debug.Log("Posición x: " + playerPositionX);
    //}
}
