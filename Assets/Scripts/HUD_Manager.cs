using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HUD_Manager : MonoBehaviour
{
    //AQU� ESTARAN TODOS LOS ELEMENTOS PRINCIPALES DE LA ESCENA DE JUEGO

    [SerializeField] TextMeshProUGUI textoPuntosJugador;
    [SerializeField] TextMeshProUGUI textoCantidadEnemigos;
    [SerializeField] TextMeshProUGUI textoRonda;
    [SerializeField] TextMeshProUGUI textoCronometro;

    /*public float cronometro = 180;*/ //Tres minutos en segundos

    //PAUSA
    [SerializeField] Canvas Pausa;
    [SerializeField] Canvas GameOver;
    bool pausado;

    private void Start()
    {
        pausado = false;
        Pausa.gameObject.SetActive(pausado);
    }

    private void Update()
    {
        MostrarHud();

        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.partidaAcabada) //Keyboard.current.escapeKey.wasPressedThisFrame
        {
            pausado = !pausado;
            ComprobarPausa();
        }

        if (GameManager.partidaAcabada)
        {
            AcabarPartida();
        }
    }

    //HUD
    public void MostrarHud()
    {
        textoPuntosJugador.text = "Puntos: " + GameManager.puntosJugador.ToString(); //Para que no se muestren decimales en el hud
        textoCantidadEnemigos.text = "Enemigos: " + SpawnManager.cantidadEnemigosEnEscena.ToString();
        textoRonda.text = "Ronda: " + SpawnManager.rondaActual.ToString();
        //textoCronometro.text = cronometro.ToString("0");
    }

    //PAUSA Y MENU
    void ComprobarPausa()
    {
        Pausa.gameObject.SetActive(pausado);

        if (pausado)
        {
            GameManager.PararTiempo();
        }

        else
        {
            GameManager.ReanudarTiempo();
        }
    }

    void AcabarPartida()
    {
        //GameOver.gameObject.SetActive(true);
        GameManager.PararTiempo();
        Invoke("GameManager.CargarMenuPrincipal", 3);
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

    public void Reiniciar()
    {
        Reanudar();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
