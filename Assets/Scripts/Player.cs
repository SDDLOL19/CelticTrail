using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int lenght;

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position += Vector3.up * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position -= Vector3.up * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += Vector3.right * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position -= Vector3.right * Time.deltaTime * speed;
        }
    }

    void Growth()
    {
        lenght++;
    }

    void Shrinkage()
    {
        lenght++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Droppeable")
        {
            Destroy(collision);
            Growth();
        }
    }
}
