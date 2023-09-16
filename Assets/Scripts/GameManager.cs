using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    public static Transform objetivoEnemigos;
    
    public int framerate = 60;
    [SerializeField] TextMeshProUGUI textoPuntosJugador;
    [SerializeField] TextMeshProUGUI textoCronometro;
    public int puntosJugador = 0;
    public float cronometro = 180;     //Tres minutos en segundos

    bool partidaAcabada = false;

    [SerializeField] Canvas Pausa;
    [SerializeField] Canvas GameOver;
    bool pausado = false;

    public static GameManager Instance;

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
    }

    void Update()
    {
        /*MostrarHud();
        cronometro -= Time.deltaTime;
        cronometro = Mathf.Clamp(cronometro, 0, 180);

        if (cronometro <= 0)
        {
            AcabarPartida();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !partidaAcabada) //Keyboard.current.escapeKey.wasPressedThisFrame
        {
            pausado = !pausado;
            ComprobarPausa();
        }*/
    }

    void ActualizarObjetivo()
    {
        
    }

    void MostrarHud()
    {
        textoPuntosJugador.text = puntosJugador.ToString("0"); //Para que no se muestren decimales en el hud
        textoCronometro.text = cronometro.ToString("0");
    }

    void ComprobarPausa()
    {
        Pausa.gameObject.SetActive(pausado);

        if (pausado)
        {
            Time.timeScale = 0;
        }

        else
        {
            Time.timeScale = 1;
        }
    }

    void ReanudarTiempo()
    {
        Time.timeScale = 1;
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

    void AcabarPartida()
    {
        partidaAcabada = true;

        Invoke("CargarMenuPrincipal", 3);
    }

    public void CargarMenuPrincipal()
    {
        ReanudarTiempo();
        SceneManager.LoadScene(0);
    }
}