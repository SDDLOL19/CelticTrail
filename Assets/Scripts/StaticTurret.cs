using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTurret : MonoBehaviour
{
    [SerializeField, Range(1f, 20f)] float rotationSpeed, detectionDistance, timeSpawnBullet, vidaTorreta;
    float tiempoAux;
    [SerializeField] GameObject prefabBullet, rotacionShooting;
    [SerializeField] Transform shootPosition;

    Collider2D enemigo;

    bool tengoEnemigo = false;

    void Start()
    {
        tiempoAux = timeSpawnBullet;
    }

    void Update()
    {
        if (!GameManager.partidaAcabada)
        {
            EnemyDetection();
        }
    }

    void EnemyDetection()
    {

        Vector2 radioDeteccion = Random.insideUnitCircle * detectionDistance; //genera un radio alrededor del objeto
        Vector2 radioDeteccionMovido = new Vector2(transform.position.x + radioDeteccion.x, transform.position.y + radioDeteccion.y); //genera un radio de deteccion alrededor del objeto aunque se mueva
                                                                                                                                      //transform.position = radioDeteccionMovido;
        if (enemigo == null || Mathf.Abs(this.transform.position.x - enemigo.transform.position.x) >= detectionDistance || Mathf.Abs(this.transform.position.y - enemigo.transform.position.y) >= detectionDistance)
        {
            tengoEnemigo = false;
        }

        if (!tengoEnemigo)
        {
            enemigo = Physics2D.OverlapCircle(radioDeteccionMovido, detectionDistance);

            if (enemigo.gameObject.tag == "Enemigo")
            {
                tengoEnemigo = true;
            }
        }

        else
        {
            RotarShootingPoint();
            TimerSpawnBullet();

            if (StatManager.puedenDispararTorreta)
            {
                CreateBullet();
            }
        }

    }

    void RotarShootingPoint()
    {
        //rotacionShooting.transform.up = GameManager.player.transform.position - rotacionShooting.transform.position;
        Vector3 look = rotacionShooting.transform.InverseTransformPoint(enemigo.transform.position);
        float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90;

        rotacionShooting.transform.Rotate(0, 0, angle);

    }

    void TimerSpawnBullet()
    {
        timeSpawnBullet -= Time.deltaTime;
    }

    void RegeneracionVida()
    {
        vidaTorreta -= Time.deltaTime;
    }

    void CreateBullet()
    {
        if (timeSpawnBullet < 0)
        {
            Instantiate(prefabBullet, shootPosition.transform.position, rotacionShooting.transform.rotation);
            //spawnea una bala en la posicion del disparo y con la rotacion que tenga este objeto
            //para modificar esta posicion simplemente mueve el objeto ShootPosition
            timeSpawnBullet = tiempoAux; //resetea el tiempo de spawn
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BalaEnemigo")
        {
            vidaTorreta -= 1 * StatManager.multplDanioRecibidoTorreta;
        }
    }
}
