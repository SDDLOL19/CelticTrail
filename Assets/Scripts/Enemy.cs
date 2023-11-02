using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject prefabDroppeable;
    [SerializeField] int enemyVida;
    NavMeshAgent agent;
    Transform objetivoActual;
    RaycastHit2D hit;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        CambiarObjetivo(GameManager.player.transform);
    }

    private void Update()
    {
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

        Disparo();
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

    private void Disparo()
    {
        hit = Physics2D.Raycast(this.transform.position, objetivoActual.position);
        Debug.DrawRay(this.transform.position,  (objetivoActual.position-this.transform.position) * 10, Color.green);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BalasJugador")
        {
            enemyVida--;
        }
    }
}
