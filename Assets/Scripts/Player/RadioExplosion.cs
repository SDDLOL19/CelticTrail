using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioExplosion : MonoBehaviour
{
    [SerializeField] float timeDestruction;

    void Update()
    {
        Destroy();
    }

    void Destroy()
    {
        timeDestruction -= Time.deltaTime;
        if (timeDestruction <= 0)
        {
            Destroy(gameObject);
        }
    }
}
