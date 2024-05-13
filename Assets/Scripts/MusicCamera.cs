using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCamera : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] AudioClip[] musicasJuego;
    float tiempoClip;
    bool musicaActiva;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (musicaActiva == false)
        {
            EjecutaAudio(musicasJuego[Random.Range(0, musicasJuego.Length)]);
            musicaActiva = true;
        }

        audioSource.volume = GameManager.volumenMusica;
    }

    public void EjecutaAudio(AudioClip clipAudio)
    {
        audioSource.clip = clipAudio;
        tiempoClip = clipAudio.length;

        StartCoroutine("CambiarCancion");
    }

    IEnumerator CambiarCancion()
    {
        audioSource.Play();
        yield return new WaitForSeconds(tiempoClip);
        musicaActiva = false;
    }
}
