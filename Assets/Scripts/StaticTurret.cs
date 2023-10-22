using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTurret : MonoBehaviour
{
    [SerializeField, Range(1f, 20f)] float rotationSpeed, detectionDistance, timeSpawnBullet;
    float tiempoAux;
    [SerializeField] GameObject prefabBullet;
    [SerializeField] Transform shootPosition;

    void Start()
    {
        tiempoAux = timeSpawnBullet;

    }

    void Update()
    {
        EnemyDetection();
    }

    void EnemyDetection()
    {
        {
            Vector2 radioDeteccion = Random.insideUnitCircle * detectionDistance; //genera un radio alrededor del objeto
            Vector2 radioDeteccionMovido = new Vector2(transform.position.x + radioDeteccion.x, transform.position.y + radioDeteccion.y); //genera un radio de deteccion alrededor del objeto aunque se mueva
            //transform.position = radioDeteccionMovido;
            Collider2D[] objetosCercanos = Physics2D.OverlapCircleAll(radioDeteccionMovido, detectionDistance); //crea un array de objetos con collider 2d
            foreach (Collider2D objeto in objetosCercanos) //detecta todos los objetos con collider 2D
            {
                if (objeto.gameObject.tag == "Enemigo") //actua solo cuando esos objetos tengan el tag enemigo
                {
                    Vector2 enemyPoint = objeto.transform.position; //detecta las coordenadas del raton
                    Vector2 direction = enemyPoint - (Vector2)transform.position;
                    //crea una tangente entre el up (la flecha verde del eje y) del objeto y la posicion x del raton (crea la linea en direccion a donde apuntara la torreta)
                    transform.up = Vector2.MoveTowards(transform.up, direction, rotationSpeed * Time.deltaTime);
                    //mueve la orientacion de la torreta (desde el punto de pivote) (el move towards ralentiza un poco ese movimiento)
                    //Debug.Log("Enemigo detectado");
                    TimerSpawnBullet();
                    CreateBullet();
                }
            }
        }

        void TimerSpawnBullet()
        {
            timeSpawnBullet -= Time.deltaTime;
        }

        void CreateBullet()
        {
            if (timeSpawnBullet < 0)
            {
                Vector2 spawnPosition = (Vector2)transform.position + (Vector2)(transform.up * 1f);
                Instantiate(prefabBullet, spawnPosition, transform.rotation);
                //spawnea una bala en la posicion del disparo y con la rotacion que tenga este objeto
                //para modificar esta posicion simplemente mueve el objeto ShootPosition
                timeSpawnBullet = tiempoAux; //resetea el tiempo de spawn
            }
        }
    }
}
