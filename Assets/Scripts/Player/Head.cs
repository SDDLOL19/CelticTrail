using System.Collections;
using System;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine.AI;

public class Head : MonoBehaviour
{
    [SerializeField] float distance, delay = 0.05f;
    [SerializeField, Range(1, 10)] int speedEscogida;
    [SerializeField] GameObject puntaCabeza;
    [SerializeField] GameObject torreta;
    public Shield miEscudo;
    float tiempo, temporizadorGiro = 0, contadorRegeneracionVida = 30;

    [HideInInspector] public float playerSpeed;
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


    private void Awake()
    {
        miEscudo = gameObject.GetComponentInParent<Shield>();
        GameManager.player = this;
    }

    private void Start()
    {
        miRenderer = GetComponent<SpriteRenderer>();
        miAnimator = GetComponent<Animator>();
        torretaRenderer = torreta.GetComponent<SpriteRenderer>();
        ControladorCarrosEnEscena();
        direccionRayo = Vector2.up;
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
            UsarTurbo();
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
            //StartCoroutine(ParpadeoTemporal());
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
        PerderVida();
        PerderVida();

        MovementUp();
        transform.position = spawnPointPlayer.position;
    }

    void ControladorCarrosEnEscena()
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
        playerSpeed = StatManager.velocidad * (speedEscogida - (lenghtSnake / 1.5f)) * miEnergiaController.velocidadActual;

        CambiarVelocidadBodies(playerSpeed);
    }

    void CambiarVelocidadBodies(float speed)
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            //bodies[i].SetSpeed(speed);
            StartCoroutine(WaitToChangeSpeed(i, speed)); ////////////////////  EL PROBLEMA RESIDE EN EL TIEMPO QUE TARDA EN CAMBIARLA YA QUE COGE PLAYER SPEED INCLUSO CUANDO CAMBIA AL TURBO, HAY QUE USAR UNA VARIABLE DIFERENTE QUE SE CAMBIE CUANDO EL CULO CAMBIE
        }
    }

    public void Growth()
    {
        if (lenghtSnake <= StatManager.vidaMaxima)
        {
            lenghtSnake++;
        }

        ControladorCarrosEnEscena();
    }

    public void Shrinkage()
    {
        if (miEscudo.LeerEscudo() > 0)
        {
            miEscudo.QuitarEscudo();
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

                contadorRegeneracionVida = 40;
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

    void Esconderme()
    {

    }

    void Movement()
    {
        transform.Translate(Vector2.up * playerSpeed * Time.deltaTime); //mueve el objeto en el en direccion flecha verde(la del eje y)        

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

    IEnumerator WaitForUp(int i, float posicionEnHorizontal, float posicionEnVertical, float speed)
    {
        tiempo = (distance * (i + 1)) / speed;
        yield return new WaitForSeconds(tiempo);
        bodies[i].SetSpeed(speed);
        bodies[i].MovementUp(posicionEnHorizontal, posicionEnVertical);
    }

    IEnumerator WaitForDown(int i, float posicionEnHorizontal, float posicionEnVertical, float speed)
    {
        tiempo = (distance * (i + 1)) / speed;
        yield return new WaitForSeconds(tiempo);
        bodies[i].SetSpeed(speed);
        bodies[i].MovementDown(posicionEnHorizontal, posicionEnVertical);
    }

    IEnumerator WaitForLeft(int i, float posicionEnHorizontal, float posicionEnVertical, float speed)
    {
        tiempo = (distance * (i + 1)) / speed;
        yield return new WaitForSeconds(tiempo);
        bodies[i].SetSpeed(speed);
        bodies[i].MovementLeft(posicionEnHorizontal, posicionEnVertical);
    }

    IEnumerator WaitForRight(int i, float posicionEnHorizontal, float posicionEnVertical, float speed)
    {
        tiempo = (distance * (i + 1)) / speed;
        yield return new WaitForSeconds(tiempo);
        bodies[i].SetSpeed(speed);
        bodies[i].MovementRight(posicionEnHorizontal, posicionEnVertical);
    }

    IEnumerator WaitToChangeSpeed(int i, float speed)
    {
        tiempo = (distance * (i + 1)) / speed;
        yield return new WaitForSeconds(tiempo);
        bodies[i].SetSpeed(speed);
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
            StartCoroutine(WaitForUp(i, transform.position.x, transform.position.y, playerSpeed));
        }
    }

    void BodiesDown()
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            StartCoroutine(WaitForDown(i, transform.position.x, transform.position.y, playerSpeed));
        }
    }

    void BodiesRight()
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            StartCoroutine(WaitForRight(i, transform.position.x, transform.position.y, playerSpeed));
        }
    }

    void BodiesLeft()
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            StartCoroutine(WaitForLeft(i, transform.position.x, transform.position.y, playerSpeed));
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
    }

    public void AnimacionEscudoRoto()
    {
        miAnimator.Play("EscudoRoto");
    }

    public void AnimacionEscudoCasiRoto()
    {
        miAnimator.Play("EscudoCasiRoto");
    }

    public void AnimacionEscudoBien()
    {
        miAnimator.Play("EscudoBien");
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
}