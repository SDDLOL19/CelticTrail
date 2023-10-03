using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [Range(1, 10)] public static float playerSpeed;

    public static KeyCode movimientoIzquierda = KeyCode.A, movimientoDerecha = KeyCode.D, movimientoArriba = KeyCode.W, movimientoAbajo = KeyCode.S;

    public static Transform objetivoEnemigos;
    
    public int framerate = 60;
    [SerializeField] TextMeshProUGUI textoPuntosJugador;
    [SerializeField] TextMeshProUGUI textoCronometro;
    public int puntosJugador = 0;
    /*public float cronometro = 180;*/ //Tres minutos en segundos

    bool partidaAcabada = false;

    [SerializeField] Canvas Pausa;
    [SerializeField] Canvas GameOver;
    bool pausado = false;

    public static GameManager Instance;

    [SerializeField] GameObject prefabEnemy;
    [SerializeField] float timeSpawn, distancePlayer, enemigosSpawneados;
    float timeAux;
    private void Awake()
    {
        Application.targetFrameRate = framerate;
        //Pausa.gameObject.SetActive(pausado);

        if (Instance == null) // If there is no instance already
        {
            DontDestroyOnLoad(gameObject); // Keep the GameObject, this component is attached to, across different scenes
            Instance = this;
        }
        else if (Instance != this) // If there is already an instance and it's not `this` instance
        {
            Destroy(gameObject); // Destroy the GameObject, this component is attached to
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        objetivoEnemigos = player.transform;
        timeAux = timeSpawn;
    }

    void Update()
    {
        //MostrarHud();
        //cronometro -= Time.deltaTime;
        //cronometro = Mathf.Clamp(cronometro, 0, 180);

        if (Input.GetKeyDown(KeyCode.Escape) && !partidaAcabada) //Keyboard.current.escapeKey.wasPressedThisFrame
        {
            pausado = !pausado;
            ComprobarPausa();
        }
        TimerSpawnEnemy();
        if (timeSpawn <= 0)
        {
            SpawnEnemy();
            timeSpawn = timeAux;
        }
    }
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

    void MostrarHud()
    {
        textoPuntosJugador.text = puntosJugador.ToString("0"); //Para que no se muestren decimales en el hud
        //textoCronometro.text = cronometro.ToString("0");
    }

    void ComprobarPausa()
    {
        Pausa.gameObject.SetActive(pausado);

        if (pausado)
        {
            PararTiempo();
        }

        else
        {
            ReanudarTiempo();
        }
    }

    static void ReanudarTiempo()
    {
        Time.timeScale = 1;
    }

    static void PararTiempo()
    {
        Time.timeScale = 0;
    }

    public void Pausar()
    {
        pausado = true;
        ComprobarPausa();
    }

    public void Reanudar()
    {
        pausado = false;
        ComprobarPausa();
    }
    public void IniciarPartida()
    {
        Scene EscenaJuego = SceneManager.GetActiveScene();
        SceneManager.LoadScene(EscenaJuego.name);
        Time.timeScale = 1;
    }
    public static void AcabarPartida()
    {
        PararTiempo();
        Scene MenuPrincipal = SceneManager.GetActiveScene();
        SceneManager.LoadScene(MenuPrincipal.name);
        //Invoke("CargarMenuPrincipal", 3);
    }
    public void CerrarJuego()
    {
        Application.Quit();
        print("cerrando... "); 
    }
    public void CargarMenuPrincipal()
    {
        ReanudarTiempo();
        Scene MenuPrincipal = SceneManager.GetActiveScene();
        SceneManager.LoadScene(MenuPrincipal.name);
    }
    public void CargarMenuOpciones()
    {
        //ReanudarTiempo();
        Scene MenuOpciones = SceneManager.GetActiveScene();
        SceneManager.LoadScene(MenuOpciones.name);
    }
}