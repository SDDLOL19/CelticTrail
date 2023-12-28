using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    [SerializeField] Enemy miPadre;
    [SerializeField] GameObject prefabHuellaDer, prefabHuellaIzq;

    void GenerarAtaque()
    {
        miPadre.GenerarBala();
    }

    public void HuellaDer()
    {
        Instantiate(prefabHuellaDer, this.gameObject.transform.position, this.transform.rotation);
    }

    public void HuellaIzq()
    {
        Instantiate(prefabHuellaIzq, this.gameObject.transform.position, this.transform.rotation);
    }
}
