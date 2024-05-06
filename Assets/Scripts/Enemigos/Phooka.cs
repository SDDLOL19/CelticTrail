using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Phooka : Enemy
{
    [SerializeField] float rangoMaxVidaMenos;


    protected override void AccionExtraUno()
    {
        if (DoneOnceAccExtrUno && enemyVida <= vidaEscogida/2)
        {
            rangoDisparoMax = rangoMaxVidaMenos;

            velocidadMovimiento /= 2;

            DoneOnceAccExtrUno = true;
        }
    }
}
