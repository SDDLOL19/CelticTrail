using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject prefabDroppeable;
    [SerializeField] int enemyVida;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        MoveToThePlayer();

        if (enemyVida <= 0)
        {
            Destroy(this.gameObject);
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
        }
    }

    void MoveToThePlayer()
    {
        agent.SetDestination(GameManager.objetivoPrincipalEnemigos.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BalasJugador")
        {
            enemyVida--;
        }
    }
}
