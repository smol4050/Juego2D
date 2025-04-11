using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClaseScore
{
    public string nombreJugador;
    public float tiempo;
    public int score;
    public int cantElementos;

    public ClaseScore(string nombreJugador, float tiempo, int score, int cantElementos)
    {
        this.nombreJugador = nombreJugador;
        this.tiempo = tiempo;
        this.score = score;
        this.cantElementos = cantElementos;
    }
}
