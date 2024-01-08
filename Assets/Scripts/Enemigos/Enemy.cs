using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject prefabDroppeable, shootPosition, prefabBullet, rotacionShooting, spriteEnemigo;
    public int velocidadMovimiento = 5;      //Para que est� a al velocidad por defecto ser�a 4 o 5
    public float enemyVida;
    public float tiempoDeRecarga, rangoDisparoMin, rangoDisparoMax, rotationSpeed = 100;
    protected float timerSpawnBullet;                       //HideInInspector para que sea p�blica pero no se pueda cambiar fuera

    protected bool disparando = false, muriendo = false;

    protected NavMeshAgent agent;
    protected Transform objetivoActual;
    protected RaycastHit2D hit;
    protected Animator miAnimator;

    void Start()
    {
        Recarga();

        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = velocidadMovimiento;

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
        if (!GameManager.partidaAcabada && !muriendo)
        {
            RotarShootingPoint();
            timerSpawnBullet -= Time.deltaTime;
            MoveToThePlayer();

            hit = Physics2D.Raycast(shootPosition.transform.position, objetivoActual.position - this.transform.position);
            Debug.DrawRay(shootPosition.transform.position, (objetivoActual.position - this.transform.position) * 10, Color.green);

            if (timerSpawnBullet <= 0 && disparando == false)
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

            if (GameManager.partidaAcabada)
            {
                AnimacionIdle();
            }
        }
    }

    void MoveToThePlayer()
    {
        if (!disparando)
        {
            agent.SetDestination(objetivoActual.position);
            AnimacionCaminar();
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

    public virtual void GenerarBala()
    {
        Instantiate(prefabBullet, shootPosition.transform.position, rotacionShooting.transform.rotation);
        disparando = false;
        Recarga();
    }

    protected virtual void DisparoBala()
    {
        if (hit.collider != null && hit.distance >= rangoDisparoMin && hit.distance < rangoDisparoMax && hit.collider.gameObject.tag == "Player") //El raycast es infinito, por lo que para evitar que detecte la cosa que queremos desde el infinito comprobamos su distance
        {
            //Debug.Log("Disparo");

            disparando = true;
            AnimacionAtaque();
        }
    }

    public void Destruirme()
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
        GetComponent<Collider2D>().enabled = false;
        muriendo = true;
        AnimacionMuerte();
    }

    protected void Recarga()
    {
        timerSpawnBullet = tiempoDeRecarga;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BalaJugador")
        {
            enemyVida -= StatManager.danioBala * StatManager.multiplicadorDa�o;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

    //ANIMACIONES

    protected void AnimacionAtaque()
    {
        miAnimator.Play("Ataque");
    }

    protected void AnimacionMuerte()
    {
        miAnimator.Play("Muerte");
    }

    protected void AnimacionIdle()
    {
        miAnimator.Play("Idle");
    }

    protected void AnimacionCaminar()
    {
        miAnimator.Play("Caminar");
    }
}
