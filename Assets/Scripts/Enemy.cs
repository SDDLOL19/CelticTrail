using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform player;
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
    }

    void MoveToThePlayer()
    {
        agent.SetDestination(player.position);
    }
}
