using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorNiveles : MonoBehaviour
{
    public void CargarNivelUno() 
    {
        SceneManager.LoadScene("EscenaJuego");
    }
}
