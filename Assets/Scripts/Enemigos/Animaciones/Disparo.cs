using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    [SerializeField] Enemy miPadre;

    void GenerarAtaque()
    {
        miPadre.GenerarBala();
    }
}
