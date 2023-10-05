using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(CambioDeControles.derecha))
        {
            transform.position += Vector3.right * Time.deltaTime;
        }
    }
}
