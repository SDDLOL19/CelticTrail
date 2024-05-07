using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ada : Enemy
{
    protected override void AccionExtraUno()
    {
        Vector2 radioDeteccion = Random.insideUnitCircle * detectionDistance; //genera un radio alrededor del objeto
        Vector2 radioDeteccionMovido = new Vector2(transform.position.x + radioDeteccion.x, transform.position.y + radioDeteccion.y);

        Transform enemigo = Physics2D.OverlapCircle(radioDeteccionMovido, detectionDistance).transform;

        if (enemigo.GetComponent<Enemy>() && !enemigo.GetComponent<Ada>())
        {
            CambiarObjetivo(enemigo.transform);
        }

        else
        {
            //CambiarObjetivo(GameManager.player.transform);
            CambiarObjetivo(enemigo.transform);
        }
    }

    protected override void ColisionExtraUno(Collider2D collision)
    {

    }
}
