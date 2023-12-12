using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HUD_Manager : MonoBehaviour
{
    //AQUÍ ESTARAN TODOS LOS ELEMENTOS PRINCIPALES DE LA ESCENA DE JUEGO

    [SerializeField] TextMeshProUGUI textoPuntosJugador, textoCantidadEnemigos, textoCantidadEnemigosRestante, textoRonda, textoCronometro;

    /*public float cronometro = 180;*/ //Tres minutos en segundos

    //PAUSA
    [SerializeField] Canvas Pausa, CambioDeControles, GameOver, Cartas;
    bool pausado = false, pantallaPausa = false, pantallaCartas = false, pantallaControles = false;

    [SerializeField] RectTransform barraEscudo;
    Vector2 escalaBarra;
    float anchuraBarra;
    Head player;

    private void Start()
    {
        pausado = false;
        Pausa.gameObject.SetActive(pantallaPausa);
        player = GameManager.player;
    }

    private void Update()
    {
        MostrarHud();

        if (Input.GetKeyDown(KeyCode.Escape) && !pantallaControles && !pantallaCartas && !GameManager.partidaAcabada)
        {
            PantallaPausa();
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
        textoCantidadEnemigosRestante.text = "Faltan " + (SpawnManager.cantidadEnemigosMax).ToString() + " enemigos";
        textoRonda.text = "Ronda: " + SpawnManager.rondaActual.ToString();
        //textoCronometro.text = cronometro.ToString("0");
        CambiarBarraEscudo();
    }

    //ESCUDO
    void CambiarBarraEscudo()
    {
        anchuraBarra = (player.miEscudo.LeerEscudo() / player.miEscudo.LeerEscudoMaximo()) * 100;
        escalaBarra = new Vector2(anchuraBarra, 17);
        barraEscudo.sizeDelta = escalaBarra;
    }

    //PAUSA Y MENU
    void ComprobarPausa()
    {
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
        Invoke("DelayAcabar", 2);
        //GameManager.PararTiempo();
    }

    void DelayAcabar()
    {
        GameManager.CargarMenuPrincipal();
    }

    public void Pausar()
    {
        pausado = true;
        ComprobarPausa();
    }

    public void Reanudar()
    {
        pausado = false;
        PantallaPausa();
        ComprobarPausa();
    }

    public void Reiniciar()
    {
        Reanudar();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PantallaPausa()
    {
        pantallaPausa = !pantallaPausa;

        Pausa.gameObject.SetActive(pantallaPausa);
    }

    public void PantallaCartas()
    {
        pantallaCartas = !pantallaCartas;

        Cartas.gameObject.SetActive(pantallaCartas);

        if (pantallaCartas)
        {
            pausado = true;
        }

        else
        {
            pausado = false;
        }
    }

    public void PantallaControles()
    {
        pantallaControles = !pantallaControles;

        CambioDeControles.gameObject.SetActive(pantallaControles);
    }
}
