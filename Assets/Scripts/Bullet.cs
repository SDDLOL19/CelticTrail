using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField, Range(0f, 20f)] float speed;
    [SerializeField] float timeDestruction;
    [SerializeField] string tagDeMiCreador;

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

    void Destroy()
    {
        timeDestruction -= Time.deltaTime;
        if (timeDestruction <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != tagDeMiCreador && collision.gameObject.tag != "BalaJugador" && collision.gameObject.tag != "BalaEnemigo") //collision.gameObject.tag != tagDeMiCreador
        {
            //Debug.Log("AAAAAAAAAAAAA");
            Destroy(this.gameObject);
        }
    }
}

