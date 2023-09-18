using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Head : MonoBehaviour
{
    [SerializeField, Range(1, 20)] int hp = 0;
    [SerializeField, Range(1, 100)] protected float moveVelocity;
    [SerializeField] GameObject[] bodies;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        transform.Translate(Vector2.up * moveVelocity * Time.deltaTime); //mueve el objeto en el en direccion flecha verde(la del eje y)

        if (Input.GetKey(KeyCode.D))
        {
            MovementRight();
        }
        if (Input.GetKey(KeyCode.A))
        {
            MovementLeft();
        }
        if (Input.GetKey(KeyCode.W))
        {
            MovementUp();
        }
        if (Input.GetKey(KeyCode.S))
        {
            MovementDown();        
        }
    }
    void MovementLeft()
    {
        transform.eulerAngles = new Vector3(0f, 0, 90); //rota el objeto a izquierda
    }
    void MovementRight()
    {
        transform.eulerAngles = new Vector3(0f, 0, -90); //rota el objeto a derecha
    }
    void MovementUp()
    {
        transform.eulerAngles = new Vector3(0f, 0, 0); //rota el objeto hacia arriba
    }
    void MovementDown()
    {
        transform.eulerAngles = new Vector3(0f, 0, 180); //rota el objeto hacia abajo
    }


    void UpdateBodies()
    {
        Vector2 spawnPosition = transform.position; //spawnea en la misma posicion
        Quaternion spawnRotation = transform.rotation; //spawnea con la misma rotacion
        for (int i = 0; i < bodies.Length; i++)
        {
            if (hp > i)
            {
                Instantiate(bodies[i], spawnPosition, spawnRotation);
                Invoke("CallBodies",3f);
            }

        }
    }
    void CallBodies()
    {

    }
}
