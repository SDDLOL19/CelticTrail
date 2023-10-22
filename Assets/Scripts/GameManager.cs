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

    void Update()
    {
        //cronometro -= Time.deltaTime;
        //cronometro = Mathf.Clamp(cronometro, 0, 180);
    }

    void ActualizarObjetivo(Transform objetivo)
    {
        objetivoPrincipalEnemigos = objetivo;
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
        SceneManager.LoadScene("MenuPrincipal");
    }
}