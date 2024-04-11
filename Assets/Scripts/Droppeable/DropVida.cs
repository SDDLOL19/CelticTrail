using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropVida : MonoBehaviour
{
    [SerializeField] float tiempoDeVida;
    float temporizador = 0;
    private void Update()
    {
        temporizador += Time.deltaTime;

        if (temporizador >= tiempoDeVida)
        {
            Destroy(this.gameObject);
        }
    }
}
