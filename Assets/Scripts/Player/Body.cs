using UnityEngine;
using UnityEngine.AI;

public class Body : MonoBehaviour
{
    [SerializeField] GameObject torreta;
    SpriteRenderer miRenderer;
    NavMeshObstacle miObstacle;
    Collider2D miCollider;

    private void Start()
    {
        miRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        miObstacle = this.gameObject.GetComponent<NavMeshObstacle>();
        miCollider = this.gameObject.GetComponent<Collider2D>();
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.Translate(Vector2.up * GameManager.player.playerSpeed * Time.deltaTime); //mueve el objeto en el en direccion flecha verde(la del eje y)           
    }

    public void MovementLeft()
    {
        transform.eulerAngles = new Vector3(0f, 0, 90); //rota el objeto a izquierda
    }

    public void MovementRight()
    {
        transform.eulerAngles = new Vector3(0f, 0, -90); //rota el objeto a derecha
    }

    public void MovementUp()
    {
        transform.eulerAngles = new Vector3(0f, 0, 0); //rota el objeto hacia arriba
    }

    public void MovementDown()
    {
        transform.eulerAngles = new Vector3(0f, 0, 180); //rota el objeto hacia abajo
    }

    public void Aparecerme()
    {
        miRenderer.enabled = true;
        miObstacle.enabled = true;
        miCollider.enabled = true;
        torreta.SetActive(true);
    }

    public void Esconderme()
    {
        miRenderer.enabled = false;
        miObstacle.enabled = false;
        miCollider.enabled = false;
        torreta.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BalaEnemigo")
        {
            Debug.Log("PA TI MI COLA");
            Destroy(collision.gameObject);
            GameManager.player.Shrinkage();
            //StartCoroutine(ParpadeoTemporal());
        }
    }
}
