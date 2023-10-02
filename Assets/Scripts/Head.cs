using System.Collections;
using System;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] float distance;
    [SerializeField, Range(1, 9)] int lenght;
    [SerializeField] GameObject[] bodies;
    [SerializeField] GameObject puntaCabeza;
    float tiempo, temporizadorGiro = 0, delay = 0.05f;

    float distanciaRaycast = 0.2f;

    Rigidbody2D miRb;
    RaycastHit2D hit;
    Vector2 direccionRayo;

    private void Start()
    {
        ControladorCarrosEnEscena();
        direccionRayo = Vector2.up;
    }

    void Update()
    {
        Movement();
        DetectarChoqueFrontal();
    }

    void DetectarChoqueFrontal()
    {
        hit = Physics2D.Raycast(puntaCabeza.transform.position, direccionRayo);
        Debug.DrawRay(puntaCabeza.transform.position, direccionRayo * distanciaRaycast, Color.green); //Para ver a donde se lanza el raycast solo en la ventana scene

        if (hit.collider != null && hit.distance <= distanciaRaycast && hit.collider.gameObject.tag == "MiCulo") //El raycast es infinito, por lo que para evitar que detecte la cosa que queremos desde el infinito comprobamos su distance
        {
            GameManager.AcabarPartida();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Droppeable")
        {
            Destroy(collision.gameObject);
            Growth();
        }

        if (collision.gameObject.tag == "Enemigo")
        {
            Destroy(collision.gameObject);
            Shrinkage(1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstaculo")
        {
            GameManager.AcabarPartida();
        }
    }
    //QQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQ

    //CANTIDAD DE CUERPOS

    void ControladorCarrosEnEscena()
    {
        for (int i = bodies.Length - 1; i >= lenght; i--)
        {
            bodies[i].GetComponent<Body>().Esconderme();
        }

        if (lenght > 0)
        {
            for (int i = 0; i < lenght; i++)
            {
                bodies[i].GetComponent<Body>().Aparecerme();
            }
        }

        GameManager.playerSpeed = 10 - lenght;
    }

    void Growth()
    {
        lenght++;
        ControladorCarrosEnEscena();
    }

    void Shrinkage(int longitudAQuitar)
    {
        lenght -= longitudAQuitar;
        ControladorCarrosEnEscena();
    }
    //QQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQ

    //MOVIMIENTO
    void Movement()
    {
        transform.Translate(Vector2.up * GameManager.playerSpeed * Time.deltaTime); //mueve el objeto en el en direccion flecha verde(la del eje y)

        if (true)
        {
            if (Input.GetKey(GameManager.movimientoDerecha))
            {
                if (this.transform.eulerAngles.z != 90f)
                {
                    MovementRight();
                    BodiesRight();
                }
            }

            if (Input.GetKey(GameManager.movimientoIzquierda))
            {
                if (this.transform.eulerAngles.z != 270f)
                {
                    MovementLeft();
                    BodiesLeft();
                }
            }

            if (Input.GetKey(GameManager.movimientoArriba))
            {
                if (this.transform.eulerAngles.z != 180f)
                {
                    MovementUp();
                    BodiesUp();
                }
            }

            if (Input.GetKey(GameManager.movimientoAbajo))
            {
                if (this.transform.eulerAngles.z != 0)
                {
                    MovementDown();
                    BodiesDown();
                }
            }
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
        tiempo = (distance * (i + 1)) / GameManager.playerSpeed;
        yield return new WaitForSeconds(tiempo);
        bodies[i].GetComponent<Body>().MovementUp();
        bodies[i].transform.position = new Vector3(posicionEnHorizontal, bodies[i].transform.position.y, bodies[i].transform.position.z);
    }

    IEnumerator WaitForDown(int i, float posicionEnHorizontal)
    {
        tiempo = (distance * (i + 1)) / GameManager.playerSpeed;
        yield return new WaitForSeconds(tiempo);
        bodies[i].GetComponent<Body>().MovementDown();
        bodies[i].transform.position = new Vector3(posicionEnHorizontal, bodies[i].transform.position.y, bodies[i].transform.position.z);
    }

    IEnumerator WaitForLeft(int i, float posicionEnVertical)
    {
        tiempo = (distance * (i + 1)) / GameManager.playerSpeed;
        yield return new WaitForSeconds(tiempo);
        bodies[i].GetComponent<Body>().MovementLeft();
        bodies[i].transform.position = new Vector3(bodies[i].transform.position.x, posicionEnVertical, bodies[i].transform.position.z);
    }

    IEnumerator WaitForRight(int i, float posicionEnVertical)
    {
        tiempo = (distance * (i + 1)) / GameManager.playerSpeed;
        yield return new WaitForSeconds(tiempo);
        bodies[i].GetComponent<Body>().MovementRight();
        bodies[i].transform.position = new Vector3(bodies[i].transform.position.x, posicionEnVertical, bodies[i].transform.position.z);
    }

    void Temporizador()
    {
        temporizadorGiro += Time.deltaTime;
        if (temporizadorGiro >= delay)
        {
            temporizadorGiro = 0;
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
}
