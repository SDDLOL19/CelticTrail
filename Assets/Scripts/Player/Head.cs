using System.Collections;
using System;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine.AI;

public class Head : MonoBehaviour
{
    [SerializeField] float delay = 0.05f;
    public float distance = 2f;
    [SerializeField, Range(1, 10)] int speedEscogida;
    [SerializeField] GameObject puntaCabeza;
    [SerializeField] GameObject torreta;
    [SerializeField] public int lenghtSnake;
    [SerializeField] Body[] bodies;
    Transform culoDeTren;
    EnergyBar miEnergiaController;
    public Shield miEscudo;
    float temporizadorGiro = 0, contadorRegeneracionVida = 30;

    float distanciaRaycast = 0.2f;

    RaycastHit2D hit;
    Vector2 direccionRayo;

    bool delayAcabado;
    [HideInInspector] public bool hePerdido = false;

    [SerializeField] GameObject serpiente;
    [SerializeField] float tiempoInvulnerable;

    [SerializeField] GameObject prefabStaticTurret;

    SpriteRenderer miRenderer;
    SpriteRenderer torretaRenderer;
    public Color defaultColor;
    public Color changedColor;
    [SerializeField] float tiempoParpadeo = 0.5f;

    Animator miAnimator;

    [SerializeField] Transform spawnPointPlayer;

    //SONIDOS
    [SerializeField] AudioClip sonidoPerderEscudo, sonidoPerderVida, sonidoSumarVida, sonidoSoltarTorreta, sonidoCambiarEscudoPorVida, sonidoMorir;



    private void Awake()
    {
        miEscudo = gameObject.GetComponentInParent<Shield>();
        miEnergiaController = gameObject.GetComponentInParent<EnergyBar>();
        GameManager.player = this;
    }

    private void Start()
    {
        miRenderer = GetComponent<SpriteRenderer>();
        miAnimator = GetComponent<Animator>();
        torretaRenderer = torreta.GetComponent<SpriteRenderer>();
        ControladorCarrosEnEscena();
        culoDeTren = bodies[lenghtSnake].transform;
        direccionRayo = Vector2.up;
        for (int i = 0; i < lenghtSnake; i++)
        {
            bodies[i].SavePosition(i);
        }
    }

    void Update()
    {
        if (!GameManager.partidaAcabada)
        {
            Movement();
            DetectarChoqueFrontal();
            RegeneracionVida();
            SoltarTorreta();
            GastarEscudo();
            //UsarTurbo();
        }
    }

    void DetectarChoqueFrontal()
    {
        hit = Physics2D.Raycast(puntaCabeza.transform.position, direccionRayo);
        Debug.DrawRay(puntaCabeza.transform.position, direccionRayo * distanciaRaycast, Color.green); //Para ver a donde se lanza el raycast solo en la ventana scene

        if (hit.collider != null && hit.distance <= distanciaRaycast && hit.collider.gameObject.tag == "Player") //El raycast es infinito, por lo que para evitar que detecte la cosa que queremos desde el infinito comprobamos su distance
        {
            Morir();
        }
    }

    protected void CambioColor()
    {
        miRenderer.color = changedColor;
        torretaRenderer.color = changedColor;
    }

    protected void ResetColor()
    {
        miRenderer.color = defaultColor;
        torretaRenderer.color = defaultColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Droppeable")
        {
            Destroy(collision.gameObject);
            Growth();
        }

        if (collision.gameObject.tag == "BalaEnemigo")
        {
            Shrinkage();
            CambioColor();
            Invoke("ResetColor", tiempoParpadeo);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstaculo")
        {
            ReSpawm();
            
        }


        if (collision.gameObject.tag == "Enemigo")
        {
            if (!miEnergiaController.estoyCorriendo)
            {
                Debug.Log("ME CHOQUÉ");
                Shrinkage();

                CambioColor();
                Invoke("ResetColor", tiempoParpadeo);
            }
        }
    }

    //QQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQ

    //CANTIDAD DE CUERPOS

    void ReSpawm()
    {
        PlayerManager.Instance.CorutinasParadas = true;
        PerderVida();
        PerderVida();

        MovementUp();
        transform.position = spawnPointPlayer.position;

        for (int i = 0; i < lenghtSnake; i++)
        {
            bodies[i].ReSpawn(spawnPointPlayer, distance * (i + 1));
        }
    }

    public void ControladorCarrosEnEscena()
    {
        CalcularVelocidad();

        if (lenghtSnake > StatManager.vidaMaxima)
        {
            lenghtSnake = StatManager.vidaMaxima;
        }

        if (lenghtSnake <= 0)
        {
            Morir();
        }

        for (int i = bodies.Length - 1; i >= lenghtSnake; i--)
        {
            bodies[i].Esconderme();
        }

        for (int i = 0; i < lenghtSnake; i++)
        {
            bodies[i].Aparecerme();
        }

        AnimacionEscudo();
    }

    void CalcularVelocidad()
    {
        PlayerManager.Instance.playerSpeed = StatManager.velocidad * (speedEscogida - (lenghtSnake / distance/*1.5f*/)) * miEnergiaController.velocidadActual;
    }

    public void Growth()
    {
        if (lenghtSnake <= StatManager.vidaMaxima)
        {
            lenghtSnake++;
            ActivarSonidoSumarVida();
        }

        ControladorCarrosEnEscena();
    }

    public void Shrinkage()
    {
        if (miEscudo.LeerEscudo() > 0)
        {
            miEscudo.QuitarEscudo();
            ActivarSonidoPerderEscudo();
        }

        else
        {
            if (lenghtSnake > 0)
            {
                PerderVida();
            }
        }

        ControladorCarrosEnEscena();
    }

    void PerderVida()
    {
        lenghtSnake -= 1 * StatManager.multpDanioRecibidoPlayer;
        ActivarSonidoPerderVida();
        if (lenghtSnake < 0)
        {
            lenghtSnake = 0;
        }

        ControladorCarrosEnEscena();
    }

    void RegeneracionVida()
    {
        if (StatManager.puedeRegenerarVida)
        {
            ContadorVida();

            if (contadorRegeneracionVida == 0)
            {
                Growth();

                contadorRegeneracionVida = 25;
            }
        }
    }
    void ContadorVida()
    {
        contadorRegeneracionVida -= Time.deltaTime;
    }

    //QQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQ

    //CONTROLES
    void SoltarTorreta()
    {

        if (Input.GetKeyDown(GameManager.botonTorretaSuelta))
        {
            if (lenghtSnake > 0)
            {
                lenghtSnake--;
                ControladorCarrosEnEscena();
                Instantiate(prefabStaticTurret, culoDeTren.position, culoDeTren.rotation);
                culoDeTren = bodies[lenghtSnake].transform;
                ActivarSonidoSoltarTorreta();
            }
        }
    }

    void GastarEscudo()
    {
        if (Input.GetKeyDown(GameManager.botonGastarEscudo))
        {
            if (miEscudo.LeerEscudo() == miEscudo.LeerEscudoMaximo())
            {
                miEscudo.VaciarEscudo();
                Growth();
                ActivarSonidoCambiarEscudoPorVida();
            }
        }
    }

    void UsarTurbo()
    {
        if (Input.GetKeyDown(GameManager.botonUsarTurbo))
        {
            miEnergiaController.UsarTurbo();

            CalcularVelocidad();
        }

        if (Input.GetKeyUp(GameManager.botonUsarTurbo))
        {
            miEnergiaController.PararTurbo();

            CalcularVelocidad();
        }
    }

    void Movement()
    {
        transform.Translate(Vector2.up * PlayerManager.Instance.playerSpeed * Time.deltaTime); //mueve el objeto en el en direccion flecha verde(la del eje y)        

        if (delayAcabado)
        {
            if (Input.GetKeyDown(GameManager.botonMovimientoDerecha))
            {
                if (this.transform.eulerAngles.z != 90f)
                {
                    MovementRight();
                    BodiesRight();
                    delayAcabado = false;
                }
            }

            else if (Input.GetKeyDown(GameManager.botonMovimientoIzquierda))
            {
                if (this.transform.eulerAngles.z != 270f)
                {
                    MovementLeft();
                    BodiesLeft();
                    delayAcabado = false;
                }
            }

            else if (Input.GetKeyDown(GameManager.botonMovimientoArriba))
            {
                if (this.transform.eulerAngles.z != 180f)
                {
                    MovementUp();
                    BodiesUp();
                    delayAcabado = false;
                }
            }

            else if (Input.GetKeyDown(GameManager.botonMovimientoAbajo))
            {
                if (this.transform.eulerAngles.z != 0)
                {
                    MovementDown();
                    BodiesDown();
                    delayAcabado = false;
                }
            }
        }

        else
        {
            Temporizador();
        }
    }

    void MovementLeft()
    {
        transform.eulerAngles = new Vector3(0f, 0, 90); //rota el objeto a izquierda
        direccionRayo = Vector2.left;
    }

    void MovementRight()
    {
        transform.eulerAngles = new Vector3(0f, 0, -90); //rota el objeto a derecha
        direccionRayo = Vector2.right;
    }

    void MovementUp()
    {
        transform.eulerAngles = new Vector3(0f, 0, 0); //rota el objeto hacia arriba
        direccionRayo = Vector2.up;
    }

    void MovementDown()
    {
        transform.eulerAngles = new Vector3(0f, 0, 180); //rota el objeto hacia abajo
        direccionRayo = Vector2.down;
    }

    void Temporizador()
    {
        temporizadorGiro += Time.deltaTime;
        if (temporizadorGiro >= delay)
        {
            temporizadorGiro = 0;
            delayAcabado = true;
        }
    }

    void BodiesUp()
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            StartCoroutine(bodies[i].WaitForUp(transform.position.x, transform.position.y));
        }
    }

    void BodiesDown()
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            StartCoroutine(bodies[i].WaitForDown(transform.position.x, transform.position.y));
        }
    }

    void BodiesRight()
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            StartCoroutine(bodies[i].WaitForRight(transform.position.x, transform.position.y));
        }
    }

    void BodiesLeft()
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            StartCoroutine(bodies[i].WaitForLeft(transform.position.x, transform.position.y));
        }
    }

    //QQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQ

    void Morir()
    {
        hePerdido = true;
        GameManager.partidaAcabada = true;
        //miEscudo.VaciarEscudo();
        AnimacionMorir();
    }

    public void AnimacionMorir()
    {
        miAnimator.Play("Muerte");

        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].AnimacionMorir();
        }
    }

    public void AnimacionEscudo()
    {
        if (miEscudo.LeerEscudo() <= 0)
        {
            miAnimator.Play("EscudoRoto");

            for (int i = 0; i < lenghtSnake; i++)
            {
                bodies[i].AnimacionEscudoRoto();
            }
        }

        else if (miEscudo.LeerEscudo() == 1)
        {
            miAnimator.Play("EscudoCasiRoto");

            for (int i = 0; i < lenghtSnake; i++)
            {
                bodies[i].AnimacionEscudoCasiRoto();
            }
        }

        else
        {
            miAnimator.Play("EscudoBien");

            for (int i = 0; i < lenghtSnake; i++)
            {
                bodies[i].AnimacionEscudoBien();
            }
        }
    }

    void ActivarSonidoSumarVida()
    {
        Instantiate(GameManager.Instance.prefabAudioSource).GetComponent<PrefabAudioSource>().EjecutaAudio(sonidoSumarVida);
    }

    void ActivarSonidoPerderVida()
    {
        Instantiate(GameManager.Instance.prefabAudioSource).GetComponent<PrefabAudioSource>().EjecutaAudio(sonidoPerderVida);
    }

    void ActivarSonidoPerderEscudo()
    {
        Instantiate(GameManager.Instance.prefabAudioSource).GetComponent<PrefabAudioSource>().EjecutaAudio(sonidoPerderEscudo);
    }
    
    void ActivarSonidoSoltarTorreta()
    {
        Instantiate(GameManager.Instance.prefabAudioSource).GetComponent<PrefabAudioSource>().EjecutaAudio(sonidoSoltarTorreta);
    }
    
    void ActivarSonidoCambiarEscudoPorVida()
    {
        Instantiate(GameManager.Instance.prefabAudioSource).GetComponent<PrefabAudioSource>().EjecutaAudio(sonidoCambiarEscudoPorVida);
    }
    
    void ActivarSonidoMorir()
    {
        Instantiate(GameManager.Instance.prefabAudioSource).GetComponent<PrefabAudioSource>().EjecutaAudio(sonidoMorir);
    }
}