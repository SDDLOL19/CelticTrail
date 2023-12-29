using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    //player
    public static bool puedeRegenerarVida = false;
    public static int vidaMaxima = 8, cantidadVidaRecuperada = 1, multpDanioRecibidoPlayer = 1, vidaEscudoMaxima = 3, cantidadEscudoRecuperada = 1;
    public static float velocidad = 1, tiempoRecarga = 1;

    //torreta soltada
    public static bool puedenDispararTorreta = false, puedeExplotarTorreta = false;
    public static float vidaMaxTorreta = 1, multplDanioRecibidoTorreta = 1, dañoTorreta = 1, areaExplosion = 1, dañoExplosion = 1;

    //bala jugador
    public static bool balaExplosiva = false;
    public static int cantidadBalas = 1;
    public static float velocidadBala = 10, vidaBala = 1, dañoBala = 1, multiplicadorDaño = 1;
   
}
