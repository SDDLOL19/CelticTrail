using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbreTextos_Tutorial : MonoBehaviour
{
    [SerializeField] GameObject textoAEsconder0, textoAEsconder1, textoAEsconder2, textoAEsconder3, textoAEsconder4, textoAEsconder5, textoAEsconder6, textoAEsconder7, textoAEsconder8, textoAEsconder9, textoAEsconder10, textoAEsconder11, textoAEsconder12;

    void Start()
    {
        textoAEsconder0.SetActive(true);
        textoAEsconder1.SetActive(false);
        textoAEsconder2.SetActive(false);
        textoAEsconder3.SetActive(false);
        textoAEsconder4.SetActive(false);
        textoAEsconder5.SetActive(false);
        textoAEsconder6.SetActive(false);
        textoAEsconder7.SetActive(false);
        textoAEsconder8.SetActive(false);
        textoAEsconder9.SetActive(false);
        textoAEsconder10.SetActive(false);
        textoAEsconder11.SetActive(false);
        textoAEsconder12.SetActive(false);
    }

    public void AbrirTexto0()
    {
        textoAEsconder0.SetActive(!textoAEsconder0.gameObject.activeSelf);
        textoAEsconder1.SetActive(false);
    }

    public void AbrirTexto1()
    {
        textoAEsconder1.SetActive(!textoAEsconder1.gameObject.activeSelf);
        textoAEsconder0.SetActive(false);
        textoAEsconder2.SetActive(false);
    }

    public void AbrirTexto2()
    {
        textoAEsconder2.SetActive(!textoAEsconder2.gameObject.activeSelf);
        textoAEsconder1.SetActive(false);
        textoAEsconder3.SetActive(false);
    }

    public void AbrirTexto3()
    {
        textoAEsconder3.SetActive(!textoAEsconder3.gameObject.activeSelf);
        textoAEsconder2.SetActive(false);
        textoAEsconder4.SetActive(false);
    }

    public void AbrirTexto4()
    {
        textoAEsconder4.SetActive(!textoAEsconder4.gameObject.activeSelf);
        textoAEsconder3.SetActive(false);
        textoAEsconder5.SetActive(false);
    }

    public void AbrirTexto5()
    {
        textoAEsconder5.SetActive(!textoAEsconder5.gameObject.activeSelf);
        textoAEsconder4.SetActive(false);
        textoAEsconder6.SetActive(false);
    }

    public void AbrirTexto6()
    {
        textoAEsconder6.SetActive(!textoAEsconder6.gameObject.activeSelf);
        textoAEsconder5.SetActive(false);
        textoAEsconder7.SetActive(false);
    }

    public void AbrirTexto7()
    {
        textoAEsconder7.SetActive(!textoAEsconder7.gameObject.activeSelf);
        textoAEsconder6.SetActive(false);
        textoAEsconder8.SetActive(false);
    }

    public void AbrirTexto8()
    {
        textoAEsconder8.SetActive(!textoAEsconder8.gameObject.activeSelf);
        textoAEsconder7.SetActive(false);
        textoAEsconder9.SetActive(false);
    }

    public void AbrirTexto9()
    {
        textoAEsconder9.SetActive(!textoAEsconder9.gameObject.activeSelf);
        textoAEsconder8.SetActive(false);
        textoAEsconder10.SetActive(false);
    }

    public void AbrirTexto10()
    {
        textoAEsconder10.SetActive(!textoAEsconder10.gameObject.activeSelf);
        textoAEsconder9.SetActive(false);
        textoAEsconder11.SetActive(false);
    }

    public void AbrirTexto11()
    {
        textoAEsconder11.SetActive(!textoAEsconder11.gameObject.activeSelf);
        textoAEsconder10.SetActive(false);
        textoAEsconder12.SetActive(false);
    }

    public void AbrirTexto12()
    {
        textoAEsconder12.SetActive(!textoAEsconder12.gameObject.activeSelf);
        textoAEsconder11.SetActive(false);
    }

    public void CerrarTexto()
    {
        if (!textoAEsconder0.gameObject.activeSelf || !textoAEsconder1.gameObject.activeSelf || !textoAEsconder2.gameObject.activeSelf || !textoAEsconder3.gameObject.activeSelf || !textoAEsconder4.gameObject.activeSelf || !textoAEsconder5.gameObject.activeSelf || !textoAEsconder6.gameObject.activeSelf || !textoAEsconder7.gameObject.activeSelf || !textoAEsconder8.gameObject.activeSelf || !textoAEsconder9.gameObject.activeSelf || !textoAEsconder10.gameObject.activeSelf || !textoAEsconder11.gameObject.activeSelf || !textoAEsconder12.gameObject.activeSelf)
        {
            textoAEsconder0.SetActive(false);
            textoAEsconder1.SetActive(false);
            textoAEsconder2.SetActive(false);
            textoAEsconder3.SetActive(false);
            textoAEsconder4.SetActive(false);
            textoAEsconder5.SetActive(false);
            textoAEsconder6.SetActive(false);
            textoAEsconder7.SetActive(false);
            textoAEsconder8.SetActive(false);
            textoAEsconder9.SetActive(false);
            textoAEsconder10.SetActive(false);
            textoAEsconder11.SetActive(false);
            textoAEsconder12.SetActive(false);
        }
    }
}
