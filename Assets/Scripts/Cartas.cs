using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Cartas : MonoBehaviour
{
    [SerializeField] AudioClip sonidoPulsarCarta, sonidoPasarPorEncima;

    //private void OnMouseOver()
    //{
    //    ActivarSonidoPasarPorEncima();
    //}

    void LlamarPantallaCartas()
    {
        GameManager.hudMngr.PantallaCartas();
    }

    //CARTAS
    public void EtapaDeDefinicion()
    {
        ActivarSonidoPulsarCarta();

        //VENTAJAS
        StatManager.velocidad += StatManager.velocidad * 0.15f;

        //DESVENTAJAS
        StatManager.danioBala -= StatManager.danioBala * 0.15f;

        LlamarPantallaCartas();
    }

    public void EscudoItaliano()
    {
        ActivarSonidoPulsarCarta();

        //VENTAJAS
        StatManager.vidaEscudoMaxima += 2;

        //DESVENTAJAS
        StatManager.velocidad -= StatManager.velocidad * 0.10f;

        LlamarPantallaCartas();
    }

    public void Acorazado()
    {
        ActivarSonidoPulsarCarta();

        //VENTAJAS
        StatManager.vidaMaxTorreta *= 2;

        //DESVENTAJAS
        StatManager.danioTorreta -= StatManager.danioTorreta * 0.20f;

        LlamarPantallaCartas();
    }

    public void AutoDestruccion()
    {
        ActivarSonidoPulsarCarta();

        //VENTAJAS
        StatManager.puedeExplotarTorreta = true;

        //DESVENTAJAS
        StatManager.danioTorreta -= StatManager.danioTorreta * 0.4f;

        LlamarPantallaCartas();
    }

    public void CañonDeCristal()
    {
        ActivarSonidoPulsarCarta();

        //VENTAJAS
        StatManager.danioBala *= StatManager.danioBala * 0.50f;

        //DESVENTAJAS
        StatManager.vidaMaxima -= 5;
        GameManager.player.ControladorCarrosEnEscena();
        LlamarPantallaCartas();
    }

    public void CuatroPorCuatro()
    {
        ActivarSonidoPulsarCarta();

        //VENTAJAS
        StatManager.velocidad += StatManager.velocidad * 0.20f;

        //DESVENTAJAS
        StatManager.danioBala += StatManager.danioBala * 0.20f;

        LlamarPantallaCartas();
    }

    public void Temerario()
    {
        ActivarSonidoPulsarCarta();

        //VENTAJAS
        StatManager.velocidad += StatManager.velocidad * 0.20f;

        //DESVENTAJAS
        StatManager.vidaMaxima -= 2;

        GameManager.player.ControladorCarrosEnEscena();

        LlamarPantallaCartas();
    }

    public void Barret()
    {
        ActivarSonidoPulsarCarta();

        //VENTAJAS
        StatManager.vidaBala += StatManager.vidaBala * 0.30f;

        //DESVENTAJAS
        StatManager.multiplicadorVelocidadBala -= StatManager.multiplicadorVelocidadBala * 0.20f;

        LlamarPantallaCartas();
    }

    public void LecheVegana()
    {
        ActivarSonidoPulsarCarta();

        //VENTAJAS
        StatManager.tiempoRecarga -= StatManager.tiempoRecarga * 0.25f;

        //DESVENTAJAS
        StatManager.danioBala -= StatManager.danioBala * 0.35f;

        LlamarPantallaCartas();
    }

    public void SuperBalas()
    {
        ActivarSonidoPulsarCarta();

        //VENTAJAS
        StatManager.danioBala += StatManager.danioBala * 0.35f;

        //DESVENTAJAS
        StatManager.tiempoRecarga += StatManager.tiempoRecarga * 0.25f;

        LlamarPantallaCartas();
    }

    public void BalasSombras()
    {
        ActivarSonidoPulsarCarta();

        //VENTAJAS
        StatManager.cantidadBalas *= 2;

        //DESVENTAJAS
        StatManager.multpDanioRecibidoPlayer *= 2;

        LlamarPantallaCartas();
    }

    public void Holograma()
    {
        ActivarSonidoPulsarCarta();

        //VENTAJAS
        StatManager.cantidadEscudoRecuperada += 1;

        //DESVENTAJAS
        StatManager.vidaEscudoMaxima = 2;
        

        LlamarPantallaCartas();
    }

    public void Nuez()
    {
        ActivarSonidoPulsarCarta();

        //VENTAJAS
        StatManager.vidaMaxTorreta *= StatManager.vidaMaxTorreta * 3.00f;

        //DESVENTAJAS
        StatManager.puedenDispararTorreta = false;

        LlamarPantallaCartas();
    }

    public void BalasExplosivas()
    {
        ActivarSonidoPulsarCarta();

        //VENTAJAS
        StatManager.balaExplosiva = true;

        //DESVENTAJAS
        StatManager.tiempoRecarga += StatManager.tiempoRecarga * 0.25f;

        LlamarPantallaCartas();
    }

    public void LagrimasDeFantasma()
    {
        ActivarSonidoPulsarCarta();

        //VENTAJAS
        StatManager.puedeRegenerarVida = true;

        //DESVENTAJAS
        StatManager.puedeDropear = false;

        LlamarPantallaCartas();
    }

    public void ActivarSonidoPulsarCarta()
    {
        Instantiate(GameManager.Instance.prefabAudioSource).GetComponent<PrefabAudioSource>().EjecutaAudio(sonidoPulsarCarta);
    }
    public void ActivarSonidoPasarPorEncima()
    {
        Instantiate(GameManager.Instance.prefabAudioSource).GetComponent<PrefabAudioSource>().EjecutaAudio(sonidoPasarPorEncima);
    }
}
