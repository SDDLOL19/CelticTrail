using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject prefabDroppeable, shootPosition, prefabBullet, rotacionShooting, spriteEnemigo;
    public int velocidadMovimiento = 5;      //Para que esté a al velocidad por defecto sería 4 o 5
    [SerializeField] protected float vidaEscogida;
    [HideInInspector] public float enemyVida;
    public float tiempoDeRecarga, rangoDisparoMin, rangoDisparoMax, rotationSpeed = 100;
    protected float timerSpawnBullet;                       //HideInInspector para que sea pública pero no se pueda cambiar fuera

    protected bool disparando = false, muriendo = false, DoneOnceAccExtrUno = false;

    protected NavMeshAgent agent;
    protected Transform objetivoActual;
    protected RaycastHit2D hit;
    protected Animator miAnimator;

    [SerializeField] float detectionDistance;

    protected SpriteRenderer miRenderer;
    public Color defaultColor;
    public Color changedColor;
    [SerializeField] float tiempoParpadeo = 0.5f;

    [SerializeField] int probabilidadDrop;

    bool explosionOnce = true;
    bool explosionOnceTorreta = true;

    [SerializeField] AudioClip sonidoDisparo, sonidoMorir;


    void Start()
    {
        Recarga();

        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = velocidadMovimiento;

        CambiarObjetivo(GameManager.player.transform);

        miAnimator = spriteEnemigo.GetComponent<Animator>();

        miRenderer = spriteEnemigo.GetComponent<SpriteRenderer>();

        enemyVida = vidaEscogida;

        StartExtraUno();
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
            AccionExtraUno();
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
        ActivarSonidoDisparo();
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

    void Dropeable()
    {
        int randomSpawnDropeable = Random.Range(0, 100);

        if (randomSpawnDropeable <= probabilidadDrop)
        {
            Instantiate(prefabDroppeable, transform.position, Quaternion.identity);
            //Debug.Log("Funciona");
        }

        else
        {
            //Debug.Log("Funciona otra parte");
        }
    }

    public void Destruirme()
    {
        if (StatManager.puedeDropear)
        {
            Dropeable();
        }

        Destroy(this.gameObject);
    }

    void Morir()
    {
        GetComponent<Collider2D>().enabled = false;
        muriendo = true;
        //////ActivarSonidoMorir();
        AnimacionMuerte();
    }

    protected void Recarga()
    {
        timerSpawnBullet = tiempoDeRecarga;
    }

    protected void CambioColor()
    {
        miRenderer.color = changedColor;
    }

    protected void ResetColor()
    {
        miRenderer.color = defaultColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BalaJugador")
        {
            enemyVida -= StatManager.danioBala * StatManager.multiplicadorDaño;

            CambioColor();
            Invoke("ResetColor", tiempoParpadeo);
        }

        if (collision.gameObject.tag == "BalaTorreta")
        {
            enemyVida -= StatManager.danioBala * StatManager.multiplicadorDaño;

            Vector2 radioDeteccion = Random.insideUnitCircle * detectionDistance; //genera un radio alrededor del objeto
            Vector2 radioDeteccionMovido = new Vector2(transform.position.x + radioDeteccion.x, transform.position.y + radioDeteccion.y);

            CambiarObjetivo(Physics2D.OverlapCircle(radioDeteccionMovido, detectionDistance).transform);

            CambioColor();
            Invoke("ResetColor", tiempoParpadeo);
        }

        if (collision.gameObject.tag == "RadioExplosion" && explosionOnce)
        {
            enemyVida -= StatManager.danioBala * StatManager.multiplicadorDaño;

            Debug.Log("FuncionaElDaño");
            CambioColor();
            Invoke("ResetColor", tiempoParpadeo);
            explosionOnce = false;
        }

        if (collision.gameObject.tag == "RadioExplosionTorreta" && explosionOnceTorreta)
        {
            enemyVida -= 4;

            Debug.Log("FuncionaElDaño");
            CambioColor();
            Invoke("ResetColor", tiempoParpadeo);
            explosionOnceTorreta = false;
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

    //EXTRA
    protected virtual void StartExtraUno()
    {

    }

    protected virtual void AccionExtraUno()
    {

    }

    public void ActivarSonidoDisparo()
    {
        Instantiate(GameManager.Instance.prefabAudioSource).GetComponent<PrefabAudioSource>().EjecutaAudio(sonidoDisparo);
    } 
    
    public void ActivarSonidoMorir()
    {
        Instantiate(GameManager.Instance.prefabAudioSource).GetComponent<PrefabAudioSource>().EjecutaAudio(sonidoMorir);
    }
}
