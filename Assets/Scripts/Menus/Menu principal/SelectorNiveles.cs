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

    public void AbrirMenuMejoras()
    {
        SceneManager.LoadScene("Mejoras");
    }

    public void AbrirMenuLore()
    {
        SceneManager.LoadScene("Lore");
    }

    public void AbrirMenuTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void AbrirMenuCredits()
    {
        SceneManager.LoadScene("Creditos");
    }
}
