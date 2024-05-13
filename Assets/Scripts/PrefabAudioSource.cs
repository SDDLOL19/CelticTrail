using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabAudioSource : MonoBehaviour
{
    private AudioSource audioSource;
    float tiempoClip;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.volume = GameManager.volumenGeneral;
    }

    public void EjecutaAudio(AudioClip clipAudio)
    {
        audioSource.clip = clipAudio;
        tiempoClip = clipAudio.length;

        StartCoroutine("DestruccionAudio");
    }
    IEnumerator DestruccionAudio()
    {
        audioSource.Play();
        yield return new WaitForSeconds(tiempoClip);
        Destroy(this.gameObject);
    }
}
