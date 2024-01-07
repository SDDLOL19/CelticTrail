using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartas : MonoBehaviour
{
    //CARTAS
    public void EtapaDeDefinicion()
    {
        //VENTAJAS
        StatManager.velocidad += StatManager.velocidad * 0.15f;

        //DESVENTAJAS
        StatManager.danioBala -= StatManager.danioBala * 0.20f;
    }

    public void EscudoItaliano()
    {
        //VENTAJAS
        StatManager.vidaEscudoMaxima += 2;

        //DESVENTAJAS
        StatManager.velocidad -= StatManager.velocidad * 0.10f;
    }

    public void Acorazado()
    {
        //VENTAJAS
        StatManager.vidaMaxTorreta *= 2;

        //DESVENTAJAS
        StatManager.danioTorreta -= StatManager.danioTorreta * 0.20f;
    }

    public void AutoDestruccion()
    {
        //VENTAJAS
        StatManager.puedeExplotarTorreta = true;

        //DESVENTAJAS
        StatManager.danioTorreta -= StatManager.danioTorreta * 0.4f;
    }

    public void CañonDeCristal()
    {
        //VENTAJAS
        StatManager.danioBala *= StatManager.danioBala * 0.50f;

        //DESVENTAJAS
        StatManager.vidaMaxima = -5;
    }

    public void CuatroPorCuatro()
    {
        //VENTAJAS
        StatManager.velocidad += StatManager.velocidad * 0.20f;

        //DESVENTAJAS
        StatManager.danioBala += StatManager.danioBala * 0.20f;
    }

    public void Temerario()
    {
        //VENTAJAS
        StatManager.velocidad += StatManager.velocidad * 0.20f;

        //DESVENTAJAS
        StatManager.vidaMaxima -= 2;
    }

    public void Barret()
    {
        //VENTAJAS
        StatManager.vidaBala += StatManager.vidaBala * 0.30f;

        //DESVENTAJAS
        StatManager.multiplicadorVelocidadBala -= StatManager.multiplicadorVelocidadBala * 0.20f;
    }

    public void LecheVegana()
    {
        //VENTAJAS
        StatManager.tiempoRecarga -= StatManager.tiempoRecarga * 0.25f;

        //DESVENTAJAS
        StatManager.danioBala -= StatManager.danioBala * 0.35f;
    }

    public void SuperBalas()
    {
        //VENTAJAS
        StatManager.danioBala += StatManager.danioBala * 0.35f;

        //DESVENTAJAS
        StatManager.tiempoRecarga += StatManager.tiempoRecarga * 0.25f;
    }

    public void BalasSombras()
    {
        //VENTAJAS
        StatManager.cantidadBalas *= 2;

        //DESVENTAJAS
        StatManager.multpDanioRecibidoPlayer *= 2;
    }

    public void Holograma()
    {
        //VENTAJAS
        StatManager.cantidadEscudoRecuperada += 1;

        //DESVENTAJAS
        StatManager.vidaEscudoMaxima = 3;
    }

    public void Nuez()
    {
        //VENTAJAS
        StatManager.vidaMaxTorreta *= StatManager.vidaMaxTorreta * 3.00f;

        //DESVENTAJAS
        StatManager.puedenDispararTorreta = false;
    }

    public void BalasExplosivas()
    {
        //VENTAJAS
        StatManager.balaExplosiva = true;

        //DESVENTAJAS
        StatManager.tiempoRecarga += StatManager.tiempoRecarga * 0.25f;
    }

    public void LagrimasDeFantasma()
    {
        //VENTAJAS
        StatManager.puedeRegenerarVida = true;

        //DESVENTAJAS
        StatManager.puedeDropear = false;
    }

}
