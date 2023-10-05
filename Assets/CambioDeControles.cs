using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioDeControles : MonoBehaviour
{
    public KeyCode derecha = KeyCode.D;
    public bool esperandoTecla = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(derecha)) transform.position += Vector3.right * Time.deltaTime;

    }
    void OnGUI()
    {
        if (esperandoTecla && Event.current.isKey && Event.current.type == EventType.KeyDown)
        {
            if (Event.current.keyCode != KeyCode.None)
            {
                Debug.Log(Event.current.keyCode);
                derecha = Event.current.keyCode;
                esperandoTecla = false;
            }
        }
    }
    public void Reconfigurar()
    {
        esperandoTecla = true;
    }

}
