using System.Collections;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Body : MonoBehaviour
{
    [SerializeField] GameObject[] torreta;
    [SerializeField] int cantidadTorretas;
    SpriteRenderer miRenderer;
    SpriteRenderer[] torretaRenderer;
    [SerializeField] SpriteRenderer escudoRenderer;
    NavMeshObstacle miObstacle;
    Collider2D miCollider;

    public Color defaultColor;
    public Color changedColor;
    [SerializeField] float tiempoParpadeo = 0.5f;

    float velocidadBody;
    [HideInInspector] public int posicion;

    Animator miAnimator;

    private void Awake()
    {
        torretaRenderer = new SpriteRenderer[cantidadTorretas];
        miRenderer = GetComponent<SpriteRenderer>();

        for (int i = 0; i < torretaRenderer.Length; i++)
        {
            torretaRenderer[i] = torreta[i].GetComponent<SpriteRenderer>();
        }

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
        transform.Translate(Vector2.up * velocidadBody * Time.deltaTime); //mueve el objeto en el en direccion flecha verde(la del eje y)           
    }

    void MovementLeft(float posicionEnHorizontal, float posicionEnVertical)
    {
        if (!GameManager.partidaAcabada)
        {
            transform.eulerAngles = new Vector3(0f, 0, 90); //rota el objeto a izquierda
            //transform.position = new Vector3(transform.position.x, posicionEnVertical, transform.position.z);
        }
    }

    void MovementRight(float posicionEnHorizontal, float posicionEnVertical)
    {
        if (!GameManager.partidaAcabada)
        {
            transform.eulerAngles = new Vector3(0f, 0, -90); //rota el objeto a derecha
            //transform.position = new Vector3(transform.position.x, posicionEnVertical, transform.position.z);
        }
    }

    public void MovementUp(float posicionEnHorizontal, float posicionEnVertical)
    {
        if (!GameManager.partidaAcabada)
        {
            transform.eulerAngles = Vector3.zero; ; //rota el objeto hacia arriba
            //transform.position = new Vector3(posicionEnHorizontal, transform.position.y, transform.position.z);
        }
    }

    void MovementDown(float posicionEnHorizontal, float posicionEnVertical)
    {
        if (!GameManager.partidaAcabada)
        {
            transform.eulerAngles = new Vector3(0f, 0, 180); //rota el objeto hacia abajo
            //transform.position = new Vector3(posicionEnHorizontal, transform.position.y, transform.position.z);
        }
    }

    public IEnumerator WaitForUp(float posicionEnHorizontal, float posicionEnVertical)
    {
        float tiempo = (GameManager.player.distance * (posicion + 1)) / velocidadBody;
        yield return new WaitForSeconds(tiempo);
        MovementUp(posicionEnHorizontal, posicionEnVertical);
    }

    public IEnumerator WaitForDown(float posicionEnHorizontal, float posicionEnVertical)
    {
        float tiempo = (GameManager.player.distance * (posicion + 1)) / velocidadBody;
        yield return new WaitForSeconds(tiempo);
        MovementDown(posicionEnHorizontal, posicionEnVertical);
    }

    public IEnumerator WaitForLeft(float posicionEnHorizontal, float posicionEnVertical)
    {
        float tiempo = (GameManager.player.distance * (posicion + 1)) / velocidadBody;
        yield return new WaitForSeconds(tiempo);
        MovementLeft(posicionEnHorizontal, posicionEnVertical);
    }

    public IEnumerator WaitForRight(float posicionEnHorizontal, float posicionEnVertical)
    {
        float tiempo = (GameManager.player.distance * (posicion + 1)) / velocidadBody;
        yield return new WaitForSeconds(tiempo);
        MovementRight(posicionEnHorizontal, posicionEnVertical);
    }

    public void ReSpawn(Transform posicionPlayer, float distancia)
    {
        transform.eulerAngles = Vector3.zero;
        transform.position = new Vector3(posicionPlayer.position.x, posicionPlayer.position.y - distancia, transform.position.z);
    }

    public void Aparecerme()
    {
        miRenderer.enabled = true;
        miObstacle.enabled = true;
        miCollider.enabled = true;
        escudoRenderer.enabled = true;

        for (int i = 0; i < torretaRenderer.Length; i++)
        {
            torreta[i].SetActive(true);
        }
    }

    public void Esconderme()
    {
        miRenderer.enabled = false;
        miObstacle.enabled = false;
        miCollider.enabled = false;
        escudoRenderer.enabled = false;

        for (int i = 0; i < torretaRenderer.Length; i++)
        {
            torreta[i].SetActive(false);
        }
    }

    void MeHicePupa()
    {
        GameManager.player.Shrinkage();
        //StartCoroutine(ParpadeoTemporal());
    }

    protected void CambioColor()
    {
        miRenderer.color = changedColor;
        for (int i = 0; i < torretaRenderer.Length; i++)
        {
            torretaRenderer[i].color = changedColor;
        }
    }

    protected void ResetColor()
    {
        miRenderer.color = defaultColor;
        for (int i = 0; i < torretaRenderer.Length; i++)
        {
            torretaRenderer[i].color = defaultColor;
        }
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

    public void SetSpeed(float speed)
    {
        velocidadBody = speed;
    }

    public void WaitForSpeed(float headSpeed)
    {
        StartCoroutine(WaitToChangeSpeed(headSpeed));
    }

    public void SavePosition(int i)
    {
        posicion = i;
    }

    IEnumerator WaitToChangeSpeed(float speed)
    {
        float tiempo = (GameManager.player.distance * (posicion + 1)) / velocidadBody; //Tiene que ser la velocidad actual del body, no la nueva
        yield return new WaitForSeconds(tiempo);
        SetSpeed(speed);
    }
}
