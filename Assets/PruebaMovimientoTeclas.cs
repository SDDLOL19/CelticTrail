using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaMovimientoTeclas : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(CambioDeControles.derecha))
        {
            transform.position += Vector3.right * Time.deltaTime;
        }

        if (Input.GetKey(CambioDeControles.izquierda))
        {
            transform.position += Vector3.left * Time.deltaTime;
        }

        if (Input.GetKey(CambioDeControles.arriba))
        {
            transform.position += Vector3.up * Time.deltaTime;
        }

        if (Input.GetKey(CambioDeControles.abajo))
        {
            transform.position += Vector3.down * Time.deltaTime;
        }
    }
}
