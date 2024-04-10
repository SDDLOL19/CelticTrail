using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingMachine : MonoBehaviour
{
    [SerializeField] string nombreEscena;

    public void CargarEscena()
    {
        SceneManager.LoadScene(nombreEscena);
    }
}
