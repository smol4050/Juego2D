using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Taller : MonoBehaviour
{
    private float tiempoJugadoTotal;
    private int Totalscore;
    private int cantElementosRecolectados;

    // Variable para mantener la referencia al GameManager_Taller
    public static GameManager_Taller Instance;
    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  
        }
        else
        {
            Destroy(gameObject);
        }

        tiempoJugadoTotal = 0;
        Totalscore = 0;
        cantElementosRecolectados = 0;
    }


    void Start()
    {
        ResetearJuego();
    }
    public void AgregarTiempo(float tiempo)
    {
        // Aumentar el tiempo (en segundos)
        tiempoJugadoTotal += tiempo;
        Debug.Log("Tiempo jugado total: " + tiempoJugadoTotal);
    }

    // M�todo para agregar puntaje (puede ser llamado desde otros scripts)
    public void AgregarPuntaje(int puntos)
    {
        Totalscore += puntos;
    }

    // M�todo para agregar elementos recolectados (tambi�n llamado desde otros scripts)
    public void AgregarElementosRecolectados(int cantidad)
    {
        cantElementosRecolectados += cantidad;
    }

    //public void SetTiempo(int tiempo)
    //{
    //    tiempoJugadoTotal = tiempo;  // Guardamos el tiempo
    //}
    public float GetTiempo()
    {
        return tiempoJugadoTotal;
    }

    // Obtener el puntaje (este ser� el que se enviar� al JSON)
    public int GetScore()
    {
        return Totalscore;
    }

    // Obtener la cantidad de elementos recolectados (este ser� el que se enviar� al JSON)
    public int GetCantElementos()
    {
        return cantElementosRecolectados;
    }
    public void ResetearJuego()
    {
        tiempoJugadoTotal = 0;
        Totalscore = 0;
        cantElementosRecolectados = 0;
    }


}
