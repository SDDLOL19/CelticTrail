using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbreTextos : MonoBehaviour
{
    [SerializeField] GameObject textoAEsconder;

    void Start()
    {
        textoAEsconder.SetActive(false);
    }

    public void AbrirTexto()
    {
        textoAEsconder.SetActive(!textoAEsconder.gameObject.activeSelf);
    }
}
