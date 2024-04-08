using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource audioSource;
    void Singleton()
    {
        if (Instance == null) // If there is no instance already
        {
            DontDestroyOnLoad(this.gameObject); // Keep the GameObject, this component is attached to, across different scenes
            Instance = this;
        }

        else if (Instance != this) // If there is already an instance and it's not `this` instance
        {
            Destroy(this.gameObject); // Destroy the GameObject, this component is attached to
        }

        DontDestroyOnLoad(this.gameObject);
        audioSource.GetComponent<AudioSource>();
    }

    void Awake()
    {
        Singleton();
    }

    public void EjecutarSonido(AudioClip sonido) 
    {
        audioSource.PlayOneShot(sonido);
    }

}
