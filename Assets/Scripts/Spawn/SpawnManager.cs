using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] public static int cantidadEnemigosEnEscena, cantidadEnemigosMax, rondaActual;
    [SerializeField] int[] cantidadEnemigosRonda;
    [SerializeField] float tiempoEscogidoSpawn, radioEscogido, tiempoEsperaRonda;
    public static float timeToSpawn, radioDeSpawn;
    [SerializeField] SpawnArea[] areasDeSpawn;

    [SerializeField] int rondaFinal;
    [SerializeField] bool[] rondasCartas;
    [SerializeField] HUD_Manager manejadorHud;

    bool puedeCambiarRonda = false;
    float temporizadorEspecial;

    [SerializeField] AudioClip sonidoFinRonda;

    private void Start()
    {
        timeToSpawn = tiempoEscogidoSpawn;
        radioDeSpawn = radioEscogido;
        temporizadorEspecial = tiempoEscogidoSpawn + 0.2f;
        rondaActual = 1;
        cantidadEnemigosMax = 0;
        cantidadEnemigosEnEscena = 0;
        Invoke("ComenzarRonda", tiempoEsperaRonda);
    }

    private void Update()
    {
        temporizadorEspecial -= Time.deltaTime;

        if (!GameManager.partidaAcabada)
        {
            if (cantidadEnemigosMax == 0 && puedeCambiarRonda)
            {
                Invoke("ActualizarRonda", 4f);                                 //SE ACTUALIZA LAS RONDAS
                puedeCambiarRonda = false; //Para que solo lo haga una vez
            }

            if (temporizadorEspecial <= 0)
            {
                ControlarSpawnAreas();
                temporizadorEspecial = tiempoEscogidoSpawn + 0.2f;
            }
        }
    }

    void ControlarSpawnAreas()
    {
        for (int i = 0; i < areasDeSpawn.Length; i++)
        {
            if (cantidadEnemigosEnEscena < cantidadEnemigosMax)
            {
                areasDeSpawn[i].CrearSpawnPoint();
                cantidadEnemigosEnEscena++;
            }
        }
    }

    void ActualizarRonda()
    {
        if (rondasCartas[rondaActual] == true)
        {
            manejadorHud.PantallaCartas();
        }

        if (rondaActual != rondaFinal)
        {
            ActivarSonidoFinRonda();
            rondaActual++;
            Invoke("ComenzarRonda", tiempoEsperaRonda);
        }

        else
        {
            GameManager.player.hePerdido = false;
            GameManager.partidaAcabada = true;
        }
    }

    void ComenzarRonda()
    {
        cantidadEnemigosMax = cantidadEnemigosRonda[rondaActual - 1];
        puedeCambiarRonda = true;
    }

    public void ActivarSonidoFinRonda()
    {
        Instantiate(GameManager.Instance.prefabAudioSource).GetComponent<PrefabAudioSource>().EjecutaAudio(sonidoFinRonda);
    }
}
