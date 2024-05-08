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
    [SerializeField] Canvas Pausa, CambioDeControles, canvasCartas, HudPrincipal;
    bool pausado = false, pantallaPausa = false, pantallaControles = false;
    public static bool pantallaCartasActivada = false;

    //ESCUDO
    [SerializeField] RectTransform barraEscudo;
    Vector2 escalaBarra;
    float anchuraBarra;
    Head player;

    //GAME OVER
    [SerializeField] Canvas CanvasGameOver, CanvasVictoria;
    bool doneOnceGameOver = false;

    //CARTAS
    [SerializeField] GameObject[] prefabsCartas;
    GameObject cartaUno, cartaDos, cartaTres;
    [SerializeField] RectTransform[] posicionCarta;
    int numeroRandomCarta;

    //STATS
    [SerializeField] TextMeshProUGUI textoDanioBala, textoVelocidadBala, textoRecarga, textoEscudoMaximo, textoEscudoRegenerado;

    //AUDIO
    [SerializeField] AudioClip sonidoPulsarBoton, sonidoVictoria, sonidoDerrota;


    private void Start()
    {
        pausado = false;
        Pausa.gameObject.SetActive(pantallaPausa);
        player = GameManager.player;
        GameManager.hudMngr = this;
    }

    private void Update()
    {
        if (!GameManager.partidaAcabada)
        {
            MostrarHud();

            if (Input.GetKeyDown(KeyCode.Escape) && !pantallaControles && !pantallaCartasActivada && !GameManager.partidaAcabada)
            {
                PantallaPausa();
                pausado = !pausado;
                ComprobarPausa();
            }
        }

        else
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

        textoDanioBala.text = (StatManager.danioBala * 100).ToString() + "%";
        textoVelocidadBala.text = (StatManager.multiplicadorVelocidadBala * 100).ToString() + "%";
        textoRecarga.text = (StatManager.tiempoRecarga * 100).ToString() + "%";
        textoEscudoMaximo.text = (StatManager.vidaEscudoMaxima).ToString();
        textoEscudoRegenerado.text = (StatManager.cantidadEscudoRecuperada).ToString();
    }

    //ESCUDO
    void CambiarBarraEscudo()
    {
        anchuraBarra = (player.miEscudo.LeerEscudo() / player.miEscudo.LeerEscudoMaximo()) * 84;
        escalaBarra = new Vector2(anchuraBarra, 18);
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
        if (!doneOnceGameOver)
        {
            if (GameManager.player.hePerdido)
            {
                ActivarSonidoDerrota();
                CanvasGameOver.gameObject.SetActive(true);
            }

            else
            {
                ActivarSonidoVictoria();
                CanvasVictoria.gameObject.SetActive(true);
            }

            HudPrincipal.gameObject.SetActive(false);
            Invoke("DelayAcabar", 2);

            doneOnceGameOver = true;
            //GameManager.PararTiempo();
        }
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
        ActivarSonidoPulsarBoton();

        pausado = false;
        PantallaPausa();
        ComprobarPausa();
    }

    public void Reiniciar()
    {
        ActivarSonidoPulsarBoton();

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
        pantallaCartasActivada = !pantallaCartasActivada;

        canvasCartas.gameObject.SetActive(pantallaCartasActivada);

        if (pantallaCartasActivada)
        {
            pausado = true;
            ComprobarPausa();
            SpawnearCartas();
        }

        else
        {
            pausado = false;
            ComprobarPausa();
            DestruirCartas();
        }
    }


    void SpawnearCartas()
    {
        numeroRandomCarta = Random.Range(0, prefabsCartas.Length - 1);
        cartaUno = Instantiate(prefabsCartas[numeroRandomCarta]);
        cartaUno.transform.SetParent(canvasCartas.transform, false);
        cartaUno.GetComponent<RectTransform>().position = posicionCarta[0].position;

        numeroRandomCarta = Random.Range(0, prefabsCartas.Length - 1);
        cartaDos = Instantiate(prefabsCartas[numeroRandomCarta]);
        cartaDos.transform.SetParent(canvasCartas.transform, false);
        cartaDos.GetComponent<RectTransform>().position = posicionCarta[1].position;

        numeroRandomCarta = Random.Range(0, prefabsCartas.Length - 1);
        cartaTres = Instantiate(prefabsCartas[numeroRandomCarta]);
        cartaTres.transform.SetParent(canvasCartas.transform, false);
        cartaTres.GetComponent<RectTransform>().position = posicionCarta[2].position;

    }
    void DestruirCartas()
    {
        Destroy(cartaUno);
        Destroy(cartaDos);
        Destroy(cartaTres);
    }

    public void PantallaControles()
    {
        ActivarSonidoPulsarBoton();
        pantallaControles = !pantallaControles;

        CambioDeControles.gameObject.SetActive(pantallaControles);
    }

    public void ActivarSonidoPulsarBoton()
    {
        Instantiate(GameManager.Instance.prefabAudioSource).GetComponent<PrefabAudioSource>().EjecutaAudio(sonidoPulsarBoton);
    }

    public void ActivarSonidoVictoria()
    {
        Instantiate(GameManager.Instance.prefabAudioSource).GetComponent<PrefabAudioSource>().EjecutaAudio(sonidoVictoria);
    }

    public void ActivarSonidoDerrota()
    {
        Instantiate(GameManager.Instance.prefabAudioSource).GetComponent<PrefabAudioSource>().EjecutaAudio(sonidoDerrota);
    }
}
