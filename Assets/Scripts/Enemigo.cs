using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] float distanciaRaycast, timerSpawnBullet = 2;
    [SerializeField] GameObject prefabBullet, shootPosition;
    RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timerSpawnBullet-=Time.deltaTime;
        Debug.DrawRay(transform.position, Vector2.up * distanciaRaycast);

        if (timerSpawnBullet <= 0)
        {
            if (hit.collider != null && hit.distance <= distanciaRaycast && hit.collider.gameObject.tag == "Player1") //El raycast es infinito, por lo que para evitar que detecte la cosa que queremos desde el infinito comprobamos su distance
            {
                Instantiate(prefabBullet, shootPosition.transform.position, transform.rotation);
            }
            timerSpawnBullet = 2;
        }
        
    }
}