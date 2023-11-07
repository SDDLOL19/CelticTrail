using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject prefabDroppeable, shootPosition, prefabBullet, rotacionShooting;
    [SerializeField] int enemyVida;
    [SerializeField] float tiempoDeRecarga;
    float timerSpawnBullet;

    [SerializeField] float rangoDisparo;

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
        RotarShootingPoint();
        timerSpawnBullet -= Time.deltaTime;
        MoveToThePlayer();

        if (enemyVida <= 0)
        {
            int randomSpawnDropeable = Random.Range(0, 10);

            if (randomSpawnDropeable <= 5)
            {
                Instantiate(prefabDroppeable, transform.position, Quaternion.identity);
                Debug.Log("Funciona");
            }

            else
            {
                Debug.Log("Funciona otra parte");
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
        if (hit.collider != null && hit.distance <= rangoDisparo && hit.collider.gameObject.tag == "Player") //El raycast es infinito, por lo que para evitar que detecte la cosa que queremos desde el infinito comprobamos su distance
        {
            Debug.Log("Disparo");
            Instantiate(prefabBullet, shootPosition.transform.position, rotacionShooting.transform.rotation);
        }
        timerSpawnBullet = tiempoDeRecarga;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BalasJugador")
        {
            enemyVida--;
        }
    }
}
