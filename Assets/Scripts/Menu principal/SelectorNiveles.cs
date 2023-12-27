using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorNiveles : MonoBehaviour
{
    [SerializeField] GameObject controles;

    public void CargarNivelUno() 
    {
        SceneManager.LoadScene("EscenaJuego");
    }

    public void AbrirMenuAjustes()
    {
        controles.SetActive(!controles.gameObject.activeSelf);
    }
}
