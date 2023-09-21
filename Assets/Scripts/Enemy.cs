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
            Instantiate(prefabDroppeable).transform.position = this.transform.position;
            Destroy(this.gameObject);
        }
    }

    void MoveToThePlayer()
    {
        agent.SetDestination(GameManager.objetivoEnemigos.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BalasJugador")
        {
            enemyVida--;
        }
    }
}
