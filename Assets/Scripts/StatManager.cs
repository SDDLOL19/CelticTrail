using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    //player
    public static bool puedeRegenerarVida = false;
    public static int vidaMaxima = 8, cantidadVidaRecuperada = 1, multpDanioRecibidoPlayer = 1, vidaEscudoMaxima = 3, cantidadEscudoRecuperada = 1;
    public static float velocidad = 1, tiempoRecarga = 0.5f;

    //torreta soltada
    public static bool puedenDispararTorreta = true, puedeExplotarTorreta = false;
    public static float vidaMaxTorreta = 1, multplDanioRecibidoTorreta = 1, danioTorreta = 1, areaExplosion = 1, danioExplosion = 1;

    //bala jugador
    public static bool balaExplosiva = false;
    public static int cantidadBalas = 1;
    public static float multiplicadorVelocidadBala = 1, vidaBala = 2, danioBala = 1, multiplicadorDaņo = 1;

    //dropeable
    public static bool puedeDropear = true;

}