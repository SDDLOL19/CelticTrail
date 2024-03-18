using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public bool CorutinasParadas = false;

    void Singleton()
    {
        if (Instance == null) // If there is no instance already
        {
            Instance = this;
        }

        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    private void Awake()
    {
        Singleton();
    }
}
