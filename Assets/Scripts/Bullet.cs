using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField, Range(1f, 20f)] float speed;
    [SerializeField, Range(1f, 20f)] float timeDestruction;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement(Vector2.up);
        Destroy();
    }
    void Movement(Vector2 bullet)
    {
        transform.Translate(bullet * speed * Time.deltaTime);
    }
    void Destroy()
    {
        timeDestruction -= Time.deltaTime;
        if (timeDestruction <= 0)
            Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}

