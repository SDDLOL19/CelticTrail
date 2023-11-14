using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animatorParpadeo;
    void Start()
    {
        animatorParpadeo = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Funciona");
            animatorParpadeo.SetBool("Choque", true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Entra");
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Funciona");
            animatorParpadeo.SetBool("Choque", true);
        }
   }
}
