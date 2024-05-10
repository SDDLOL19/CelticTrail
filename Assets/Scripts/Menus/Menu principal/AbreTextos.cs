using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbreTextos : MonoBehaviour
{
    [SerializeField] GameObject textoAEsconder0, textoAEsconder1, textoAEsconder2, textoAEsconder3, textoAEsconder4, textoAEsconder5, textoAEsconder6, textoAEsconder7;

    void Start()
    {
        textoAEsconder0.SetActive(false);
        textoAEsconder1.SetActive(false); 
        textoAEsconder2.SetActive(false);
        textoAEsconder3.SetActive(false);
        textoAEsconder4.SetActive(false);
        textoAEsconder5.SetActive(false);
        textoAEsconder6.SetActive(false);
        textoAEsconder7.SetActive(false);
    }

    public void AbrirTexto0()
    {
        textoAEsconder0.SetActive(!textoAEsconder0.gameObject.activeSelf);
    }
    
    public void AbrirTexto1()
    {
        textoAEsconder1.SetActive(!textoAEsconder1.gameObject.activeSelf);
    }
    
    public void AbrirTexto2()
    {
        textoAEsconder2.SetActive(!textoAEsconder2.gameObject.activeSelf);
    }
    
    public void AbrirTexto3()
    {
        textoAEsconder3.SetActive(!textoAEsconder3.gameObject.activeSelf);
    }
    
    public void AbrirTexto4()
    {
        textoAEsconder4.SetActive(!textoAEsconder4.gameObject.activeSelf);
    }
    
    public void AbrirTexto5()
    {
        textoAEsconder5.SetActive(!textoAEsconder5.gameObject.activeSelf);
    }
    
    public void AbrirTexto6()
    {
        textoAEsconder6.SetActive(!textoAEsconder6.gameObject.activeSelf);
    }
    
    public void AbrirTexto7()
    {
        textoAEsconder7.SetActive(!textoAEsconder7.gameObject.activeSelf);
    }

    public void CerrarTexto()
    {
        if (!textoAEsconder0.gameObject.activeSelf || !textoAEsconder1.gameObject.activeSelf || !textoAEsconder2.gameObject.activeSelf || !textoAEsconder3.gameObject.activeSelf || !textoAEsconder4.gameObject.activeSelf || !textoAEsconder5.gameObject.activeSelf || !textoAEsconder6.gameObject.activeSelf || !textoAEsconder7.gameObject.activeSelf)
        {
            textoAEsconder0.SetActive(false);
            textoAEsconder1.SetActive(false);
            textoAEsconder2.SetActive(false);
            textoAEsconder3.SetActive(false);
            textoAEsconder4.SetActive(false);
            textoAEsconder5.SetActive(false);
            textoAEsconder6.SetActive(false);
            textoAEsconder7.SetActive(false);
        }
    }
}
