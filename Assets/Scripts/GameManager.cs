using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int framerate = 60;
    public static GameManager Instance;
    public static bool partidaAcabada = false;

    //MOVIMIENTO JUGADOR
    public static Head player; //Para que al cargar pantalla funcione el Navmesh se tiene que guardar directamente desde player en su start

    public static KeyCode movimientoIzquierda = KeyCode.A, movimientoDerecha = KeyCode.D, movimientoArriba = KeyCode.W, movimientoAbajo = KeyCode.S;

    public static Transform objetivoPrincipalEnemigos;

    //ELEMENTOS JUGADOR
    public static int puntosJugador = 0;

    //SPAWN ENEMIGOS
    [SerializeField] GameObject prefabEnemy;
    [SerializeField] float timeSpawn, distancePlayer, enemigosSpawneados;
    float timeAux;

    void Singleton()
    {
        if (Instance == null) // If there is no instance already
        {
            DontDestroyOnLoad(this.gameObject); // Keep the GameObject, this component is attached to, across different scenes
            Instance = this;
        }

        else if (Instance != this) // If there is already an instance and it's not `this` instance
        {
            Destroy(this.gameObject); // Destroy the GameObject, this component is attached to
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Awake()
    {
        Application.targetFrameRate = framerate;
        Singleton();     
    }

    private void Start()
    {
        objetivoPrincipalEnemigos = player.transform;
        timeAux = timeSpawn;
    }

    void Update()
    {
        //MostrarHud();
        //cronometro -= Time.deltaTime;
        //cronometro = Mathf.Clamp(cronometro, 0, 180);

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
            Vector2 playerPosition = player.transform.position; //detecta la posicion del jugador
            Vector2 randomPosicion = UnityEngine.Random.onUnitSphere * distancePlayer; // Random de distancia: genera una posicion random a una unidad de distancia (en todas direcciones x,y,z), se multiplica para que spawnee a la distancia que se quiera
            Vector2 enemyPosition = playerPosition + randomPosicion; //suma la posicion actual del personaje mas el random de distancia
            Instantiate(prefabEnemy, enemyPosition, Quaternion.identity); //spawnea el enemigo
        }
    }
    void ActualizarObjetivo()
    {

    }

    public static void ReanudarTiempo()
    {
        Time.timeScale = 1;
    }

    public static void PararTiempo()
    {
        Time.timeScale = 0;
    }

    public void CerrarJuego()
    {
        Debug.Log("cerrando... "); //Sirve para comprobar si funciona en el editor, donde no se puede cerrar
        Application.Quit();
    }

    public static void CargarMenuPrincipal()
    {
        ReanudarTiempo();
        //SceneManager.LoadScene(MenuPrincipal.name);
        SceneManager.LoadScene(1);
    }
}