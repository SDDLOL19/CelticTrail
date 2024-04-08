using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barril : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] AudioClip sonidoRomperBarril;

    [SerializeField] GameObject prefabRotoBarril;

    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = sonidoRomperBarril;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BalaJugador")
        {
            MeRompo();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            MeRompo();
        }
    }

    void MeRompo()
    {
        audioSource.PlayOneShot(sonidoRomperBarril);
        Instantiate(prefabRotoBarril, this.transform.position + new Vector3(0, 0, 7.1f), Quaternion.identity);
        Destroy(this.gameObject);
    }
}

