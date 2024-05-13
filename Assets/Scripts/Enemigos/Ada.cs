using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ada : Enemy
{
    protected override void AccionExtraUno()
    {
        //Enemy enemigo = Physics2D.OverlapCircle(transform.position, detectionDistance).GetComponent<Enemy>();

        //if (enemigo != null /*&& enemigo.GetComponent<Ada>() == null*/)
        //{
        //    Debug.Log("Enemigo encontrao");

        //    if (enemigo.GetComponent<Ada>() == null)
        //    {
        //        CambiarObjetivo(enemigo.transform);
        //    }
        //}

        //else
        //{
        //    CambiarObjetivo(GameManager.player.transform);
        //}

        CambiarObjetivo(GameManager.player.transform);
    }

    protected override void ColisionExtraUno(Collider2D collision)
    {

    }
}
