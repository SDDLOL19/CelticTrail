using System.Collections;
using System;
using UnityEngine;

public class Head : MonoBehaviour
{
    //[SerializeField, Range(1, 10)] int hp = 0;
    [SerializeField, Range(1, 100)] public float speed = 20;
    [SerializeField] float distance;
    [SerializeField] int lenght;
    [SerializeField] GameObject[] bodies;
    float tiempo;
    float temporizadorGiro = 0;
    float delay = 0.05f;

    private void Start()
    {
        controladorCarrosEnEscena();
    }

    void Update()
    {
        Movement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Droppeable")
        {
            Destroy(collision);
            Growth();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            Shrinkage(1);
        }
    }

    //CANTIDAD DE CUERPOS

    void controladorCarrosEnEscena()
    {
        if (lenght > 0)
        {
            for (int i = bodies.Length; i >= lenght; i--)
            {
                bodies[i].GetComponent<Body>().Esconderme();
            }

            for (int i = 0; i < lenght; i++)
            {
                bodies[i].GetComponent<Body>().Aparecerme();
            }
        }

    }

    void Growth()
    {
        lenght++;
        controladorCarrosEnEscena();
    }

    void Shrinkage(int longitudAQuitar)
    {
        lenght -= longitudAQuitar;
        controladorCarrosEnEscena();
    }

    //MOVIMIENTO

    void Movement()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime); //mueve el objeto en el en direccion flecha verde(la del eje y)

        if (true)
        {
            if (Input.GetKey(KeyCode.D))
            {
                MovementRight();
                BodiesRight();
            }

            if (Input.GetKey(KeyCode.A))
            {
                MovementLeft();
                BodiesLeft();
            }

            if (Input.GetKey(KeyCode.W))
            {
                MovementUp();
                BodiesUp();
            }

            if (Input.GetKey(KeyCode.S))
            {
                MovementDown();
                BodiesDown();
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

    IEnumerator WaitForUp(int i)
    {
        tiempo = (distance * (i + 1)) / speed;
        yield return new WaitForSeconds(tiempo);
        bodies[i].GetComponent<Body>().MovementUp();
    }

    IEnumerator WaitForDown(int i)
    {
        tiempo = (distance * (i + 1)) / speed;
        yield return new WaitForSeconds(tiempo);
        bodies[i].GetComponent<Body>().MovementDown();
    }

    IEnumerator WaitForLeft(int i)
    {
        tiempo = (distance * (i + 1)) / speed;
        yield return new WaitForSeconds(tiempo);
        bodies[i].GetComponent<Body>().MovementLeft();
    }

    IEnumerator WaitForRight(int i)
    {
        tiempo = (distance * (i + 1)) / speed;
        yield return new WaitForSeconds(tiempo);
        bodies[i].GetComponent<Body>().MovementRight();
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
            StartCoroutine(WaitForUp(i));
        }
    }

    void BodiesDown()
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            StartCoroutine(WaitForDown(i));
        }
    }

    void BodiesRight()
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            StartCoroutine(WaitForRight(i));
        }
    }

    void BodiesLeft()
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            StartCoroutine(WaitForLeft(i));
        }
    }
}
