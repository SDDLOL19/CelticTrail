using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject prefabDroppeable, shootPosition, prefabBullet, rotacionShooting, spriteEnemigo;
    public float enemyVida;
    public float tiempoDeRecarga, rangoDisparoMin, rangoDisparoMax, rotationSpeed = 100;
    [HideInInspector] public float timerSpawnBullet;                       //HideInInspector para que sea pública pero no se pueda cambiar fuera

    [HideInInspector] public bool disparando = false, estoyMuerto = false;

    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Transform objetivoActual;
    [HideInInspector] public RaycastHit2D hit;
    [HideInInspector] public Animator miAnimator;

    void Start()
    {
        Recarga();

        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        CambiarObjetivo(GameManager.player.transform);

        miAnimator = spriteEnemigo.GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        SpawnManager.cantidadEnemigosEnEscena--;
        SpawnManager.cantidadEnemigosMax--;
    }

    private void Update()
    {
        if (!GameManager.partidaAcabada && !estoyMuerto)
        {
            RotarShootingPoint();
            timerSpawnBullet -= Time.deltaTime;
            MoveToThePlayer();

            hit = Physics2D.Raycast(shootPosition.transform.position, objetivoActual.position - this.transform.position);
            Debug.DrawRay(shootPosition.transform.position, (objetivoActual.position - this.transform.position) * 10, Color.green);

            if (timerSpawnBullet <= 0)
            {
                DisparoBala();
            }

            if (enemyVida <= 0)
            {
                Morir();
            }
        }

        else
        {
            Parar();
        }
    }

    void MoveToThePlayer()
    {
        if (!disparando)
        {
            agent.SetDestination(objetivoActual.position);
            miAnimator.Play("Caminar");
        }

        else
        {
            Parar();
        }
    }

    void Parar()
    {
        agent.SetDestination(this.transform.position);
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
        //rotacionShooting.transform.up = GameManager.player.transform.position - rotacionShooting.transform.position;
        Vector3 look = rotacionShooting.transform.InverseTransformPoint(objetivoActual.transform.position);
        float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90;

        rotacionShooting.transform.Rotate(0, 0, angle);

    }

    public void GenerarBala()
    {
        Instantiate(prefabBullet, shootPosition.transform.position, rotacionShooting.transform.rotation);
        disparando = false;
        Recarga();
    }

    void DisparoBala()
    {
        if (hit.collider != null && hit.distance >= rangoDisparoMin && hit.distance < rangoDisparoMax && hit.collider.gameObject.tag == "Player") //El raycast es infinito, por lo que para evitar que detecte la cosa que queremos desde el infinito comprobamos su distance
        {
            //Debug.Log("Disparo");

            disparando = true;
            miAnimator.Play("Ataque");
        }
    }

    public void Destuirme()
    {
        if (StatManager.puedeDropear)
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
        }

        Destroy(this.gameObject);
    }

    void Morir()
    {
        estoyMuerto = true;
        miAnimator.Play("Muerte");
    }

    void Recarga()
    {
        timerSpawnBullet = tiempoDeRecarga;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BalaJugador")
        {
            enemyVida -= StatManager.danioBala * StatManager.multiplicadorDaño;
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
