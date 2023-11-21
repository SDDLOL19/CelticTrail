using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float temporizadorParpadeo = 0.5f;
    private Animator animatorParpadeo;
    void Start()
    {
        animatorParpadeo = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Entra");
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("Funciona");
            animatorParpadeo.SetBool("Choque", true); // activa la condicion para que la animacion empiece
            StartCoroutine(DesactivarAnimacion(temporizadorParpadeo)); // corutina para que la booleana cambie a falso
        }
    }
    private IEnumerator DesactivarAnimacion(float temporizador)
    {
        yield return new WaitForSeconds(temporizador);
        animatorParpadeo.SetBool("Choque", false);
    }
}
