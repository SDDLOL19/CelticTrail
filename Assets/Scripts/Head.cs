using System.Collections;
using System;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] float distance;
    [SerializeField, Range(1, 9)] int lenght;
    [SerializeField] GameObject[] bodies;
    float tiempo;
    float temporizadorGiro = 0;
    float delay = 0.05f;

    Rigidbody2D miRb;
    RaycastHit2D hit;

    private void Start()
    {
        ControladorCarrosEnEscena();
    }

    void Update()
    {
        DetectarChoqueFrontal();
        Movement();
    }

    void DetectarChoqueFrontal()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.up);
        hit.distance = 0.1f;

        if (hit.collider.gameObject.tag == "MiCulo")
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
    }
    void MovementRight()
    {
        transform.eulerAngles = new Vector3(0f, 0, -90); //rota el objeto a derecha
    }
    void MovementUp()
    {
        transform.eulerAngles = new Vector3(0f, 0, 0); //rota el objeto hacia arriba
    }
    void MovementDown()
    {
        transform.eulerAngles = new Vector3(0f, 0, 180); //rota el objeto hacia abajo
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
