using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barril : MonoBehaviour
{
    [SerializeField] GameObject prefabRotoBarril;
    [SerializeField] AudioClip sonidoRomperBarril;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BalaJugador")
        {
            MeRompoConAudio();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            MeRompoConAudio();
        }
    }

    void MeRompoConAudio()
    {
        //soundManager.EjecutarSonido(sonidoRomperBarril);
        //audioSource.Play();
        //Invoke("MeRompo", 0.5f);
        Instantiate(GameManager.Instance.prefabAudioSource).GetComponent<PrefabAudioSource>().EjecutaAudio(sonidoRomperBarril);
        MeRompo();      
    }

    void MeRompo()
    {
        Instantiate(prefabRotoBarril, this.transform.position + new Vector3(0, 0, 7.1f), Quaternion.identity);
        Destroy(this.gameObject);
    }
}

