using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbreTextos : MonoBehaviour
{
    [SerializeField] GameObject texto;
    // Start is called before the first frame update
    void Start()
    {
        texto.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AbrirTexto()
    {
        texto.SetActive(!texto.gameObject.activeSelf);
    }
}
