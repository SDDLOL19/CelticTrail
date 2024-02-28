using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Body[] bodies;
    [SerializeField] Head head;
    [SerializeField] int lenghtSnake;
    Transform culoDeTren;
    EnergyBar miEnergiaController;

    private void Awake()
    {
        miEnergiaController = gameObject.GetComponentInParent<EnergyBar>();
    }

    void Start()
    {
        culoDeTren = bodies[lenghtSnake].transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReSpawn()
    {
        for (int i = 0; i < lenghtSnake; i++)
        {
            bodies[i].ReSpawn(spawnPointPlayer, distance * (i + 1));
        }
    }

    void AnimacionMorir()
    {
        head.AnimacionMorir();

        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].AnimacionMorir();
        }
    }

    public void AnimacionEscudo()
    {
        if (miEscudo.LeerEscudo() <= 0)
        {
            head.AnimacionEscudo();

            for (int i = 0; i < lenghtSnake; i++)
            {
                bodies[i].AnimacionEscudoRoto();
            }
        }

        else if (miEscudo.LeerEscudo() == 1)
        {
            miAnimator.Play("EscudoCasiRoto");

            for (int i = 0; i < lenghtSnake; i++)
            {
                bodies[i].AnimacionEscudoCasiRoto();
            }
        }

        else
        {
            miAnimator.Play("EscudoBien");

            for (int i = 0; i < lenghtSnake; i++)
            {
                bodies[i].AnimacionEscudoBien();
            }
        }
    }
}
