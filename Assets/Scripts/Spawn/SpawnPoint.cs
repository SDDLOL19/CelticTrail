using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject prefabEnemy;

    public void SpawnEnemy()
    {
        Instantiate(prefabEnemy, transform.position, Quaternion.identity); //spawnea el enemigo
        Destroy(this.gameObject);
    }
}
