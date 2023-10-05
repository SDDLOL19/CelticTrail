using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOriginal : MonoBehaviour
{
    public KeyCode derecha = KeyCode.D/*, izquierda = KeyCode.A, arriba = KeyCode.W, abajo = KeyCode.S*/;
    public bool esperandoTecla = false;

    void Update()
    {
        if (Input.GetKey(derecha))
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
    void OnGUI()
    {
        if (esperandoTecla && Event.current.isKey && Event.current.type == EventType.KeyDown)
        {
            if (Event.current.keyCode != KeyCode.None)
            {
                Debug.Log(Event.current.keyCode);
                derecha = Event.current.keyCode;
                esperandoTecla = false;
            }
        }
    }
    public void Reconfigurar()
    {
        esperandoTecla = true;
    }

}