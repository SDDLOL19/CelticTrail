using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    //player
    public static bool puedeRegenerarVida;
    public static int vidaMaxima, cantidadVidaRecuperada, multpDanioRecibidoPlayer, vidaEscudoMaxima, cantidadEscudoRecuperada;
    public static float velocidad, tiempoRecarga;

    [SerializeField] bool SF_puedeRegenerarVida = false;
    [SerializeField] int SF_vidaMaxima = 8, SF_cantidadVidaRecuperada = 1, SF_multpDanioRecibidoPlayer = 1, SF_vidaEscudoMaxima = 3, SF_cantidadEscudoRecuperada = 1;
    [SerializeField] float SF_velocidad = 1, SF_tiempoRecarga = 0.5f;

    //torreta soltada
    public static bool puedenDispararTorreta, puedeExplotarTorreta;
    public static float vidaMaxTorreta, multplDanioRecibidoTorreta, danioTorreta, areaExplosion, danioExplosion;

    [SerializeField] bool SF_puedenDispararTorreta = true, SF_puedeExplotarTorreta = false;
    [SerializeField] float SF_vidaMaxTorreta = 1, SF_multplDanioRecibidoTorreta = 1, SF_danioTorreta = 1, SF_areaExplosion = 1, SF_danioExplosion = 1;

    //bala jugador
    public static bool balaExplosiva;
    public static int cantidadBalas;
    public static float multiplicadorVelocidadBala, vidaBala, danioBala, multiplicadorDaño;

    [SerializeField] bool SF_balaExplosiva = false;
    [SerializeField] int SF_cantidadBalas = 1;
    [SerializeField] float SF_multiplicadorVelocidadBala = 1, SF_vidaBala = 2, SF_danioBala = 1, SF_multiplicadorDaño = 1;

    //dropeable
    public static bool puedeDropear = true;

    [SerializeField] bool SF_puedeDropear = true;


    private void Awake()
    {
        //PLAYER
        puedeRegenerarVida = SF_puedeRegenerarVida;
        vidaMaxima = SF_vidaMaxima;
        cantidadVidaRecuperada = SF_cantidadVidaRecuperada;
        multpDanioRecibidoPlayer = SF_multpDanioRecibidoPlayer;
        vidaEscudoMaxima = SF_vidaEscudoMaxima;
        cantidadEscudoRecuperada = SF_cantidadEscudoRecuperada;
        velocidad = SF_velocidad;
        tiempoRecarga = SF_tiempoRecarga;

        //TORRETA SOLTADA
        puedenDispararTorreta = SF_puedenDispararTorreta;
        puedeExplotarTorreta = SF_puedeExplotarTorreta;
        vidaMaxTorreta = SF_vidaMaxTorreta;
        multplDanioRecibidoTorreta = SF_multplDanioRecibidoTorreta;
        danioTorreta = SF_danioTorreta;
        areaExplosion = SF_areaExplosion;
        danioExplosion = SF_danioExplosion;

        //BALA JUGADOR
        balaExplosiva = SF_balaExplosiva;
        cantidadBalas = SF_cantidadBalas;
        multiplicadorVelocidadBala = SF_multiplicadorVelocidadBala;
        vidaBala = SF_vidaBala;
        danioBala = SF_danioBala;
        multiplicadorDaño = SF_multiplicadorDaño;

        //DROPPEABLE
        puedeDropear = SF_puedeDropear;
    }
}