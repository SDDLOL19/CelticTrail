using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject prefabDroppeable, shootPosition, prefabBullet, rotacionShooting;
    [SerializeField] int enemyVida;
    [SerializeField] float tiempoDeRecarga;
    float timerSpawnBullet;
    [SerializeField] float rangoDisparoMin, rangoDisparoMax;

    NavMeshAgent agent;
    Transform objetivoActual;
    RaycastHit2D hit;

    void Start()
    {
        timerSpawnBullet = tiempoDeRecarga;

        SpawnManager.cantidadEnemigosEnEscena++;

        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        CambiarObjetivo(GameManager.player.transform);
    }

    private void OnDestroy()
    {
        SpawnManager.cantidadEnemigosEnEscena--;
        SpawnManager.cantidadEnemigosMax--;
    }

    private void Update()
    {
        if (!GameManager.partidaAcabada)
        {
            RotarShootingPoint();
            timerSpawnBullet -= Time.deltaTime;
            MoveToThePlayer();

            if (enemyVida <= 0)
            {
                int randomSpawnDropeable = Random.Range(0, 10);

                if (randomSpawnDropeable <= 5)
                {
                    Instantiate(prefabDroppeable, transform.position, Quaternion.identity);
                    //Debug.Log("Funciona");
                }

                else
                {
                    //Debug.Log("Funciona otra parte");
                }

                Destroy(this.gameObject);
            }

            hit = Physics2D.Raycast(shootPosition.transform.position, objetivoActual.position - this.transform.position);
            Debug.DrawRay(shootPosition.transform.position, (objetivoActual.position - this.transform.position) * 10, Color.green);

            if (timerSpawnBullet <= 0)
            {
                Disparo();
            }
        }

        else
        {
            agent.SetDestination(this.transform.position);
        }
    }

    void MoveToThePlayer()
    {
        agent.SetDestination(objetivoActual.position);
    }

    void CambiarObjetivo(Transform objetivoNuevo)
    {
        objetivoActual = objetivoNuevo;
    }

    void ObjetivoPrincipal()
    {
        objetivoActual = GameManager.objetivoPrincipalEnemigos;
    }

    void RotarShootingPoint()
    {
        //rotacionShooting.transform.LookAt(GameManager.player.transform.position);
        rotacionShooting.transform.up = GameManager.player.transform.position - rotacionShooting.transform.position;
    }

    private void Disparo()
    {
        if (hit.collider != null && hit.distance >= rangoDisparoMin && hit.distance < rangoDisparoMax && hit.collider.gameObject.tag == "Player") //El raycast es infinito, por lo que para evitar que detecte la cosa que queremos desde el infinito comprobamos su distance
        {
            //Debug.Log("Disparo");
            Instantiate(prefabBullet, shootPosition.transform.position, rotacionShooting.transform.rotation);
        }
        timerSpawnBullet = tiempoDeRecarga;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BalaJugador")
        {
            enemyVida--;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
