using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Head : MonoBehaviour
{
    [SerializeField, Range(1, 20)] int hp = 0;
    [SerializeField, Range(1, 100)] protected float moveVelocity, rotationVelocity;
    [SerializeField] GameObject[] prefabBody;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * moveVelocity * Time.deltaTime); //mueve el objeto en el en direccion flecha verde(la del eje y)

        if (Input.GetKey(KeyCode.A))
        {
            transform.eulerAngles += new Vector3(0f, 0, -rotationVelocity) * Time.deltaTime; //rota el objeto a izquierda
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles += new Vector3(0f, 0, rotationVelocity) * Time.deltaTime; //rota el objeto a derecha
        }
        CreateBody();
    }
    //void Movement(Vector2 head)
    //{
    //    transform.Translate(head * moveVelocity * Time.deltaTime);
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        transform.eulerAngles += new Vector3(0f, 0, -rotationVelocity) * Time.deltaTime;
    //    }
    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        transform.eulerAngles += new Vector3(0f, 0, rotationVelocity) * Time.deltaTime;
    //    }
    //}
    void CreateBody()
    {
        //if (hp == 1)
        //{
        //    Vector2 spawnPosition = transform.position; //spawnea en la misma posicion
        //    Quaternion spawnRotation = transform.rotation; //spawnea con la misma rotacion
        //    Instantiate(prefabBody[1], spawnPosition, spawnRotation);
        //}
        Vector2 spawnPosition = transform.position; //spawnea en la misma posicion
        Quaternion spawnRotation = transform.rotation; //spawnea con la misma rotacion
        for (int i = 0; i < prefabBody.Length; i++)
        {
            if (hp > i)
            {
                Instantiate(prefabBody[i], spawnPosition, spawnRotation);
            }

        }
    }
}
