using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartas : MonoBehaviour
{
    void LlamarPantallaCartas()
    {
        GameManager.hudMngr.PantallaCartas();
    }

    //CARTAS
    public void EtapaDeDefinicion()
    {
        //VENTAJAS
        StatManager.velocidad += StatManager.velocidad * 0.15f;

        //DESVENTAJAS
        StatManager.danioBala -= StatManager.danioBala * 0.15f;

        LlamarPantallaCartas();
    }

    public void EscudoItaliano()
    {
        //VENTAJAS
        StatManager.vidaEscudoMaxima += 2;

        //DESVENTAJAS
        StatManager.velocidad -= StatManager.velocidad * 0.10f;

        LlamarPantallaCartas();
    }

    public void Acorazado()
    {
        //VENTAJAS
        StatManager.vidaMaxTorreta *= 2;

        //DESVENTAJAS
        StatManager.danioTorreta -= StatManager.danioTorreta * 0.20f;

        LlamarPantallaCartas();
    }

    public void AutoDestruccion()
    {
        //VENTAJAS
        StatManager.puedeExplotarTorreta = true;

        //DESVENTAJAS
        StatManager.danioTorreta -= StatManager.danioTorreta * 0.4f;

        LlamarPantallaCartas();
    }

    public void CañonDeCristal()
    {
        //VENTAJAS
        StatManager.danioBala *= StatManager.danioBala * 0.50f;

        //DESVENTAJAS
        StatManager.vidaMaxima -= 5;

        GameManager.player.ControladorCarrosEnEscena();

        LlamarPantallaCartas();
    }

    public void CuatroPorCuatro()
    {
        //VENTAJAS
        StatManager.velocidad += StatManager.velocidad * 0.20f;

        //DESVENTAJAS
        StatManager.danioBala += StatManager.danioBala * 0.20f;

        LlamarPantallaCartas();
    }

    public void Temerario()
    {
        //VENTAJAS
        StatManager.velocidad += StatManager.velocidad * 0.20f;

        //DESVENTAJAS
        StatManager.vidaMaxima -= 2;

        GameManager.player.ControladorCarrosEnEscena();

        LlamarPantallaCartas();
    }

    public void Barret()
    {
        //VENTAJAS
        StatManager.vidaBala += StatManager.vidaBala * 0.30f;

        //DESVENTAJAS
        StatManager.multiplicadorVelocidadBala -= StatManager.multiplicadorVelocidadBala * 0.20f;

        LlamarPantallaCartas();
    }

    public void LecheVegana()
    {
        //VENTAJAS
        StatManager.tiempoRecarga -= StatManager.tiempoRecarga * 0.25f;

        //DESVENTAJAS
        StatManager.danioBala -= StatManager.danioBala * 0.35f;

        LlamarPantallaCartas();
    }

    public void SuperBalas()
    {
        //VENTAJAS
        StatManager.danioBala += StatManager.danioBala * 0.35f;

        //DESVENTAJAS
        StatManager.tiempoRecarga += StatManager.tiempoRecarga * 0.25f;

        LlamarPantallaCartas();
    }

    public void BalasSombras()
    {
        //VENTAJAS
        StatManager.cantidadBalas *= 2;

        //DESVENTAJAS
        StatManager.multpDanioRecibidoPlayer *= 2;

        LlamarPantallaCartas();
    }

    public void Holograma()
    {
        //VENTAJAS
        StatManager.cantidadEscudoRecuperada += 1;

        //DESVENTAJAS
        StatManager.vidaEscudoMaxima = 2;
        

        LlamarPantallaCartas();
    }

    public void Nuez()
    {
        //VENTAJAS
        StatManager.vidaMaxTorreta *= StatManager.vidaMaxTorreta * 3.00f;

        //DESVENTAJAS
        StatManager.puedenDispararTorreta = false;

        LlamarPantallaCartas();
    }

    public void BalasExplosivas()
    {
        //VENTAJAS
        StatManager.balaExplosiva = true;

        //DESVENTAJAS
        StatManager.tiempoRecarga += StatManager.tiempoRecarga * 0.25f;

        LlamarPantallaCartas();
    }

    public void LagrimasDeFantasma()
    {
        //VENTAJAS
        StatManager.puedeRegenerarVida = true;

        //DESVENTAJAS
        StatManager.puedeDropear = false;

        LlamarPantallaCartas();
    }

}
