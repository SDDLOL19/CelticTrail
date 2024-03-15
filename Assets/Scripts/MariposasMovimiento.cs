using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MariposasMovimiento : MonoBehaviour
{
    private float rangoMovimiento = 10f;
    private float LimiteX = 60, LimiteY = 33, posicionobjetivoX, posicionobjetivoY;
    public float velocidad = 1f;
    Animator animator;
    private Vector3 posicionObjetivo;
    // Start is called before the first frame update
    void Start()
    {
        MoverAleatoriamente();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.Lerp(transform.position, posicionObjetivo, velocidad * Time.deltaTime);
        if (Vector3.Distance(transform.position, posicionObjetivo) < 0.5f)
        {
            MoverAleatoriamente();
        }
    }
    void MoverAleatoriamente()
    {
        // Genera una nueva posición aleatoria dentro de los límites del mapa
        posicionobjetivoX = Random.Range(-LimiteX, LimiteX);
        posicionobjetivoY = Random.Range(-LimiteY, LimiteY);

        // Asegúrate de que la nueva posición no esté demasiado cerca de la posición actual
        while (Vector3.Distance(new Vector3(posicionobjetivoX, posicionobjetivoY, 0), transform.position) > rangoMovimiento)
        {
            posicionobjetivoX = Random.Range(-LimiteX, LimiteX);
            posicionobjetivoY = Random.Range(-LimiteY, LimiteY);
        }

        posicionObjetivo = new Vector3(posicionobjetivoX, posicionobjetivoY, transform.position.z);
    }
    //void MoverAleatoriamente()
    //{
    //    // Genera una nueva posición aleatoria dentro del rango de movimiento

    //    posicionobjetivoX = Random.Range(-(transform.position.x + rangoMovimiento), (transform.position.x + rangoMovimiento));
    //    posicionobjetivoX = Mathf.Clamp(posicionobjetivoX, -LimiteX, LimiteX);

    //    posicionobjetivoY = Random.Range(-(transform.position.y + rangoMovimiento), (transform.position.y + rangoMovimiento));
    //    posicionobjetivoY = Mathf.Clamp(posicionobjetivoY, -LimiteX, LimiteY);
    //    posicionObjetivo = new Vector3(posicionobjetivoX, posicionobjetivoY, this.transform.position.z);
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "BalaJugador")
        {
            animator.SetBool("muere", true);
            
        }
    }
    void Muerte()
    {
        Destroy(this.gameObject);

    }

}
