using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClaseScore
{
    private string nombreJugador;
    private int tiempo;
    private int score;
    private int cantElementos;

    public ClaseScore()
    {
    }

    public ClaseScore(string nombreJugador, int tiempo, int score, int cantElementos)
    {
        this.nombreJugador = nombreJugador;
        this.tiempo = tiempo;
        this.score = score;
        this.cantElementos = cantElementos;
    }

    public string NombreJugador { get => nombreJugador; set => nombreJugador = value; }
    public int Tiempo { get => tiempo; set => tiempo = value; }
    public int Score { get => score; set => score = value; }
    public int CantElementos { get => cantElementos; set => cantElementos = value; }



    // Start is called before the first frame update

}
