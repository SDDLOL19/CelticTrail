using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCabeza : MonoBehaviour
{
    [SerializeField, Range(0f, 20f)] float rotationSpeed;
    float tiempoSpawnBalas = 0;
    [SerializeField] GameObject prefabBullet;
    [SerializeField] Transform shootPosition;

    [SerializeField] AudioClip sonidosDisparos;

    private new Camera camera;

    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        if (!GameManager.partidaAcabada)
        {
            Rotation();
            TemporizadorBalas();

            if (tiempoSpawnBalas <= 0)
            {
                Disparo();
                //Debug.Log("FuncionaElcontador");
            }
        }
    }

    void Rotation()
    {
        Vector2 mousePoint = camera.ScreenToWorldPoint(Input.mousePosition); //detecta las coordenadas del raton
        Vector2 direction = mousePoint - (Vector2)transform.position;
        //crea una tangente entre el up (la flecha verde del eje y) del objeto y la posicion x del raton (crea la linea en direccion a donde apuntara la torreta)
        //transform.up = direction; //movimiento menos realista gira instantaneo
        transform.up = Vector2.MoveTowards(transform.up, direction, rotationSpeed * Time.deltaTime);
        //mueve la orientacion de la torreta (desde el punto de pivote) (el move towards ralentiza un poco ese movimiento)
    }

    void TemporizadorBalas()
    {
        tiempoSpawnBalas -= Time.deltaTime;
    }

    void Disparo()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateBullet();

            for (int i = 1; i < StatManager.cantidadBalas; i++)
            {
                Invoke("CreateBullet", 0.15f);
            }
        }
    }

    void CreateBullet()
    {
        ActivarSonido();
        Instantiate(prefabBullet, shootPosition.position, transform.rotation);
        //spawnea una bala en la posicion del disparo y con la rotacion que tenga este objeto
        //para modificar esta posicion simplemente mueve el objeto ShootPosition
        tiempoSpawnBalas = StatManager.tiempoRecarga;
    }

    void ActivarSonido()
    {
        Instantiate(GameManager.Instance.prefabAudioSource).GetComponent<PrefabAudioSource>().EjecutaAudio(sonidosDisparos);
    }
}
