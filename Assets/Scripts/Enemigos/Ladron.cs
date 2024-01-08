using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ladron : Enemy
{
    [SerializeField] float tiempoEntreRafaga;
    int cantidadTiros = 0;

    public override void GenerarBala()
    {
        Instantiate(prefabBullet, shootPosition.transform.position, rotacionShooting.transform.rotation);
        cantidadTiros++;

        if (cantidadTiros >= 2)
        {
            disparando = false;
            cantidadTiros = 0;
            Recarga();
        }       
    }

    protected override void DisparoBala()
    {
        if (hit.collider != null && hit.distance >= rangoDisparoMin && hit.distance < rangoDisparoMax && hit.collider.gameObject.tag == "Player") //El raycast es infinito, por lo que para evitar que detecte la cosa que queremos desde el infinito comprobamos su distance
        {
            //Debug.Log("Disparo");

            disparando = true;
            AnimacionAtaque();
        }
    }
}