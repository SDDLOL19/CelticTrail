using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barril : MonoBehaviour
{
    [SerializeField] GameObject prefabRotoBarril;
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
        Instantiate(prefabRotoBarril, this.transform.position + new Vector3(0, 0, 7.1f), Quaternion.identity);
        Destroy(this.gameObject);
    }
}
