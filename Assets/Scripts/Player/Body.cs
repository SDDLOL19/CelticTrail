using UnityEngine;
using UnityEngine.AI;

public class Body : MonoBehaviour
{
    [SerializeField] GameObject torreta;
    SpriteRenderer miRenderer;
    SpriteRenderer torretaRenderer;
    [SerializeField] SpriteRenderer escudoRenderer;
    NavMeshObstacle miObstacle;
    Collider2D miCollider;

    public Color defaultColor;
    public Color changedColor;
    [SerializeField] float tiempoParpadeo = 0.5f;

    Animator miAnimator;

    private void Awake()
    {
        miRenderer = GetComponent<SpriteRenderer>();
        torretaRenderer = torreta.GetComponent<SpriteRenderer>();
        miObstacle = GetComponent<NavMeshObstacle>();
        miCollider = GetComponent<Collider2D>();
        miAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!GameManager.partidaAcabada)
        {
            Movement();
        }
    }

    void Movement()
    {
        transform.Translate(Vector2.up * GameManager.player.playerSpeed * Time.deltaTime); //mueve el objeto en el en direccion flecha verde(la del eje y)           
    }

    public void MovementLeft(float posicionEnHorizontal, float posicionEnVertical)
    {
        if (!GameManager.partidaAcabada)
        {
            transform.eulerAngles = new Vector3(0f, 0, 90); //rota el objeto a izquierda
            new Vector3(posicionEnHorizontal, posicionEnVertical, transform.position.z);
        }
    }

    public void MovementRight(float posicionEnHorizontal, float posicionEnVertical)
    {
        if (!GameManager.partidaAcabada)
        {
            transform.eulerAngles = new Vector3(0f, 0, -90); //rota el objeto a derecha
            transform.position = new Vector3(posicionEnHorizontal, posicionEnVertical, transform.position.z);
        }
    }

    public void MovementUp(float posicionEnHorizontal, float posicionEnVertical)
    {
        if (!GameManager.partidaAcabada)
        {
            transform.eulerAngles = Vector3.zero; ; //rota el objeto hacia arriba
            transform.position = new Vector3(posicionEnHorizontal, posicionEnVertical, transform.position.z);
        }
    }

    public void MovementDown(float posicionEnHorizontal, float posicionEnVertical)
    {
        if (!GameManager.partidaAcabada)
        {
            transform.eulerAngles = new Vector3(0f, 0, 180); //rota el objeto hacia abajo
            transform.position = new Vector3(posicionEnHorizontal, posicionEnVertical, transform.position.z);
        }
    }

    public void ReSpawn(Transform posicionPlayer, float distancia)
    {
        transform.eulerAngles = Vector3.zero;
        transform.position = new Vector3(posicionPlayer.position.x, posicionPlayer.position.y - distancia ,transform.position.z);
    }

    public void Aparecerme()
    {
        miRenderer.enabled = true;
        miObstacle.enabled = true;
        miCollider.enabled = true;
        escudoRenderer.enabled = true;
        torreta.SetActive(true);
    }

    public void Esconderme()
    {
        miRenderer.enabled = false;
        miObstacle.enabled = false;
        miCollider.enabled = false;
        escudoRenderer.enabled = false;
        torreta.SetActive(false);
    }

    void MeHicePupa()
    {
        GameManager.player.Shrinkage();
        //StartCoroutine(ParpadeoTemporal());
    }

    protected void CambioColor()
    {
        miRenderer.color = changedColor;
        torretaRenderer.color = changedColor;
    }

    protected void ResetColor()
    {
        miRenderer.color = defaultColor;
        torretaRenderer.color = defaultColor;
    }

    public void AnimacionMorir()
    {
        miAnimator.Play("Muerte");
    }

    public void AnimacionEscudoRoto()
    {
        miAnimator.Play("EscudoRoto");
    }

    public void AnimacionEscudoCasiRoto()
    {
        miAnimator.Play("EscudoCasiRoto");
    }

    public void AnimacionEscudoBien()
    {
        miAnimator.Play("EscudoBien");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BalaEnemigo")
        {
            MeHicePupa();

            CambioColor();
            Invoke("ResetColor", tiempoParpadeo);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            MeHicePupa();

            CambioColor();
            Invoke("ResetColor", tiempoParpadeo);
        }
    }
}
