using System.Collections;
using System;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] float distance, delay = 0.05f;
    [SerializeField, Range(0, 9)] int lenght;
    [SerializeField, Range(1, 10)] int speedEscogida;
    [SerializeField] GameObject[] bodies;
    [SerializeField] GameObject puntaCabeza;
    float tiempo, temporizadorGiro = 0;

    public float playerSpeed;
    float distanciaRaycast = 0.2f;

    RaycastHit2D hit;
    Vector2 direccionRayo;

    bool delayAcabado;

    [SerializeField] GameObject serpiente;
    [SerializeField] float tiempoInvulnerable;

    [SerializeField] GameObject prefabStaticTurret;

    private void Awake()
    {
        GameManager.player = this;
    }

    private void Start()
    {
        ControladorCarrosEnEscena();
        direccionRayo = Vector2.up;
    }

    void Update()
    {
        if (!GameManager.partidaAcabada)
        {
            Movement();
            DetectarChoqueFrontal();
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
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("ME CHOQUÉ");

        if (collision.gameObject.tag == "Obstaculo")
        {
            Morir();
        }

        if (collision.gameObject.tag == "Enemigo")
        {
            Destroy(collision.gameObject);
            Shrinkage();
        }
    }

    //QQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQ

    //CANTIDAD DE CUERPOS

    void ControladorCarrosEnEscena()
    {
        playerSpeed = speedEscogida - (lenght / 1.5f);

        for (int i = bodies.Length - 1; i >= lenght; i--)
        {
            bodies[i].GetComponent<Body>().Esconderme();
        }

        if (lenght >= 0)
        {
            for (int i = 0; i < lenght; i++)
            {
                bodies[i].GetComponent<Body>().Aparecerme();
            }
        }
    }

    void Growth()
    {
        if (lenght <= 8)
        {
            lenght++;
        }
        
        ControladorCarrosEnEscena();
    }

    public void Shrinkage()
    {
        if (lenght > 0)
        {
            lenght--;
        }

        else
        {
            Morir();
        }

        ControladorCarrosEnEscena();
    }
    //QQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQ

    //MOVIMIENTO
    void Movement()
    {
        transform.Translate(Vector2.up * playerSpeed * Time.deltaTime); //mueve el objeto en el en direccion flecha verde(la del eje y)        

        if (delayAcabado)
        {
            if (Input.GetKey(GameManager.movimientoDerecha))
            {
                if (this.transform.eulerAngles.z != 90f)
                {
                    MovementRight();
                    BodiesRight();
                    delayAcabado = false;
                }
            }

            else if (Input.GetKey(GameManager.movimientoIzquierda))
            {
                if (this.transform.eulerAngles.z != 270f)
                {
                    MovementLeft();
                    BodiesLeft();
                    delayAcabado = false;
                }
            }

            else if (Input.GetKey(GameManager.movimientoArriba))
            {
                if (this.transform.eulerAngles.z != 180f)
                {
                    MovementUp();
                    BodiesUp();
                    delayAcabado = false;
                }
            }

            else if (Input.GetKey(GameManager.movimientoAbajo))
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

    IEnumerator WaitForUp(int i, float posicionEnHorizontal)
    {
        tiempo = (distance * (i + 1)) / playerSpeed;
        yield return new WaitForSeconds(tiempo);
        bodies[i].GetComponent<Body>().MovementUp(posicionEnHorizontal);
    }

    IEnumerator WaitForDown(int i, float posicionEnHorizontal)
    {
        tiempo = (distance * (i + 1)) / playerSpeed;
        yield return new WaitForSeconds(tiempo);
        bodies[i].GetComponent<Body>().MovementDown(posicionEnHorizontal);
    }

    IEnumerator WaitForLeft(int i, float posicionEnVertical)
    {
        tiempo = (distance * (i + 1)) / playerSpeed;
        yield return new WaitForSeconds(tiempo);
        bodies[i].GetComponent<Body>().MovementLeft(posicionEnVertical);
    }

    IEnumerator WaitForRight(int i, float posicionEnVertical)
    {
        tiempo = (distance * (i + 1)) / playerSpeed;
        yield return new WaitForSeconds(tiempo);
        bodies[i].GetComponent<Body>().MovementRight(posicionEnVertical);
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
            StartCoroutine(WaitForUp(i, this.transform.position.x));
        }
    }

    void BodiesDown()
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            StartCoroutine(WaitForDown(i, this.transform.position.x));
        }
    }

    void BodiesRight()
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            StartCoroutine(WaitForRight(i, this.transform.position.y));
        }
    }

    void BodiesLeft()
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            StartCoroutine(WaitForLeft(i, this.transform.position.y));
        }
    }

    //QQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQ

    IEnumerator ParpadeoTemporal()
    {
        while (tiempoInvulnerable > 0)
        {
            StartCoroutine(TemporizadorInvulnerable());
            InvokeRepeating("Parpadeo", 0, 0.1f); // Llama a la función Parpadeo
            yield return new WaitForSeconds(0.1f); // Espera 0.1 segundos antes de la siguiente iteración
            // Reduce el tiempo invulnerable
        }
        tiempoInvulnerable = 0.5f;  // Reinicia el tiempo invulnerable
    }

    IEnumerator TemporizadorInvulnerable()
    {
        while (tiempoInvulnerable > 0)
        {
            tiempoInvulnerable -= Time.deltaTime;
            yield return null;  // Espera hasta el siguiente frame antes de la siguiente iteración
        }
    }

    void Parpadeo()  //Hay que rehacerlo del todo. Nunca funcionará como queremos de esta forma
    {
        serpiente.SetActive(!serpiente.activeInHierarchy);
    }

    void Morir()
    {
        GameManager.partidaAcabada = true;
    }
}

