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

    public static KeyCode botonMovimientoIzquierda = KeyCode.A, botonMovimientoDerecha = KeyCode.D, botonMovimientoArriba = KeyCode.W, botonMovimientoAbajo = KeyCode.S, botonTorretaSuelta = KeyCode.E, botonGastarEscudo = KeyCode.Q, botonUsarTurbo = KeyCode.Space;

    public static Transform objetivoPrincipalEnemigos;

    //ELEMENTOS JUGADOR
    public static int puntosJugador = 0;

    public static HUD_Manager hudMngr;

    //AUDIO
    public GameObject prefabAudioSource;
    public static float volumenGeneral = 1;
    public static float volumenMusica = 1;

    [SerializeField] protected static AudioClip sonidoPulsarBoton;




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
        partidaAcabada = false;
    }

    private void Start()
    {
        Application.targetFrameRate = framerate;
        partidaAcabada = false;
    }

    void Update()
    {
        //cronometro -= Time.deltaTime;
        //cronometro = Mathf.Clamp(cronometro, 0, 180);
    }

    public static void ReanudarTiempo()
    {
        Time.timeScale = 1;
    }

    public static void PararTiempo()
    {
        Time.timeScale = 0;
    }

    public static void CerrarJuego()
    {
        //Instantiate(GameManager.Instance.prefabAudioSource).GetComponent<PrefabAudioSource>().EjecutaAudio(sonidoPulsarBoton);

        Debug.Log("cerrando... "); //Sirve para comprobar si funciona en el editor, donde no se puede cerrar
        Application.Quit();
    }

    public static void CargarMenuPrincipal()
    {

        //Instantiate(GameManager.Instance.prefabAudioSource).GetComponent<PrefabAudioSource>().EjecutaAudio(sonidoPulsarBoton);

        ReanudarTiempo();
        partidaAcabada = false;
        SceneManager.LoadScene("MenuPrincipal");
    }

}