using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    [SerializeField, Range(1f, 20f)] float speed;
    float timeDestruction;
    [SerializeField] string tagDeMiCreador;
    [SerializeField] GameObject prefabAreaExplosion;

    private void Start()
    {
        timeDestruction = StatManager.vidaBala;
    }

    void Update()
    {
        if (!GameManager.partidaAcabada)
        {
            Movement(Vector2.up);
            Destroy();
        }
    }

    void Movement(Vector2 bullet)
    {
        transform.Translate(bullet * speed * StatManager.multiplicadorVelocidadBala * Time.deltaTime);
    }

    void SpanwDañoArea()
    {
        Instantiate(prefabAreaExplosion, transform.position, transform.rotation);
    }

    void Destroy()
    {
        timeDestruction -= Time.deltaTime;
        if (timeDestruction <= 0)
        {
            if (StatManager.balaExplosiva)
            {
                SpanwDañoArea();
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemigo") //collision.gameObject.tag != tagDeMiCreador
        {
            //Debug.Log("AAAAAAAAAAAAA");
            if (StatManager.balaExplosiva)
            {
                SpanwDañoArea();
            }
            Destroy(this.gameObject);
        }

        else if (collision.gameObject.tag == "Obstaculo")
        {
            if (StatManager.balaExplosiva)
            {
                SpanwDañoArea();
            }
            Destroy(this.gameObject);
        }

        else if (collision.gameObject.tag != tagDeMiCreador)
        {

        }
    }
}