using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public Slider volumGeneral, volumMusica;

    void Start()
    {
        volumGeneral.value = GameManager.volumenGeneral;
        volumMusica.value = GameManager.volumenMusica;
    }

    void Update()
    {
        GameManager.volumenGeneral = volumGeneral.value;
        GameManager.volumenMusica = volumMusica.value;
    }
}
