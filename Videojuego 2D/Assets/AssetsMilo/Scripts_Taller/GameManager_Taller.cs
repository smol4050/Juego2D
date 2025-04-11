using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Taller : MonoBehaviour
{
    // Atributos para almacenar los datos del juego
    private int tiempoJugado;
    private int puntaje;
    private int cantElementosRecolectados;

    // Variables de control, por ejemplo, un temporizador para el tiempo
    private float timer;

    // Variable para mantener la referencia al GameManager_Taller
    public static GameManager_Taller Instance;

    // M�todo de inicializaci�n
    private void Awake()
    {
        // Hacer que esta clase persista a trav�s de las escenas
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Esta l�nea hace que el objeto no se destruya al cambiar de escena
        }
        else
        {
            Destroy(gameObject);
        }

        // Inicializaci�n de las variables de juego
        tiempoJugado = 0;
        puntaje = 0;
        cantElementosRecolectados = 0;

        // Inicializaci�n del temporizador (si se usa)
        timer = 0f;
    }

    // M�todo que se ejecuta al comenzar la escena
    void Start()
    {
        // Aqu� puedes inicializar cosas como el temporizador o puntajes si es necesario
    }

    // M�todo de actualizaci�n que se ejecuta cada frame
    void Update()
    {
        // Aqu� va el c�digo para actualizar el tiempo de juego, por ejemplo:
        ActualizarTiempo();
    }

    // M�todo para actualizar el tiempo de juego
    private void ActualizarTiempo()
    {
        // Aumentar el tiempo (en segundos)
        timer += Time.deltaTime;

        // Convertir el tiempo en un valor entero (por ejemplo, para mostrarlo en el HUD)
        tiempoJugado = Mathf.FloorToInt(timer);  // Convierte el tiempo en segundos a un valor entero
    }

    // M�todo para agregar puntaje (puede ser llamado desde otros scripts)
    public void AgregarPuntaje(int puntos)
    {
        puntaje += puntos;
    }

    // M�todo para agregar elementos recolectados (tambi�n llamado desde otros scripts)
    public void AgregarElementosRecolectados(int cantidad)
    {
        cantElementosRecolectados += cantidad;
    }

    // **M�todos de captura de datos** (Deber�as implementar estos m�todos seg�n sea necesario)
    // Aqu� ir�an las funciones donde capturas cada uno de los datos necesarios del juego
    // Ejemplo: Puntaje, tiempo, elementos recolectados, etc.

    // Obtener el tiempo jugado (este ser� el que se enviar� al JSON)
    public void SetTiempo(int tiempo)
    {
        tiempoJugado = tiempo;  // Guardamos el tiempo
    }
    public int GetTiempo()
    {
        return tiempoJugado;
    }

    // Obtener el puntaje (este ser� el que se enviar� al JSON)
    public int GetScore()
    {
        return puntaje;
    }

    // Obtener la cantidad de elementos recolectados (este ser� el que se enviar� al JSON)
    public int GetCantElementos()
    {
        return cantElementosRecolectados;
    }

    // **M�todos adicionales** (puedes agregar m�s l�gica aqu� seg�n tus necesidades)
    // Puedes agregar m�todos para reiniciar los valores de tiempo, puntajes, elementos, etc., si es necesario.

    // Por ejemplo, un m�todo para resetear los valores cuando comienza una nueva partida:
    public void ResetearJuego()
    {
        tiempoJugado = 0;
        puntaje = 0;
        cantElementosRecolectados = 0;
        timer = 0f;
    }
}
