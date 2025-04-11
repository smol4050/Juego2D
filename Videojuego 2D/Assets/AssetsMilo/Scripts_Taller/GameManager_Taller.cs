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

    // Método de inicialización
    private void Awake()
    {
        // Hacer que esta clase persista a través de las escenas
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Esta línea hace que el objeto no se destruya al cambiar de escena
        }
        else
        {
            Destroy(gameObject);
        }

        // Inicialización de las variables de juego
        tiempoJugado = 0;
        puntaje = 0;
        cantElementosRecolectados = 0;

        // Inicialización del temporizador (si se usa)
        timer = 0f;
    }

    // Método que se ejecuta al comenzar la escena
    void Start()
    {
        // Aquí puedes inicializar cosas como el temporizador o puntajes si es necesario
    }

    // Método de actualización que se ejecuta cada frame
    void Update()
    {
        // Aquí va el código para actualizar el tiempo de juego, por ejemplo:
        ActualizarTiempo();
    }

    // Método para actualizar el tiempo de juego
    private void ActualizarTiempo()
    {
        // Aumentar el tiempo (en segundos)
        timer += Time.deltaTime;

        // Convertir el tiempo en un valor entero (por ejemplo, para mostrarlo en el HUD)
        tiempoJugado = Mathf.FloorToInt(timer);  // Convierte el tiempo en segundos a un valor entero
    }

    // Método para agregar puntaje (puede ser llamado desde otros scripts)
    public void AgregarPuntaje(int puntos)
    {
        puntaje += puntos;
    }

    // Método para agregar elementos recolectados (también llamado desde otros scripts)
    public void AgregarElementosRecolectados(int cantidad)
    {
        cantElementosRecolectados += cantidad;
    }

    // **Métodos de captura de datos** (Deberías implementar estos métodos según sea necesario)
    // Aquí irían las funciones donde capturas cada uno de los datos necesarios del juego
    // Ejemplo: Puntaje, tiempo, elementos recolectados, etc.

    // Obtener el tiempo jugado (este será el que se enviará al JSON)
    public void SetTiempo(int tiempo)
    {
        tiempoJugado = tiempo;  // Guardamos el tiempo
    }
    public int GetTiempo()
    {
        return tiempoJugado;
    }

    // Obtener el puntaje (este será el que se enviará al JSON)
    public int GetScore()
    {
        return puntaje;
    }

    // Obtener la cantidad de elementos recolectados (este será el que se enviará al JSON)
    public int GetCantElementos()
    {
        return cantElementosRecolectados;
    }

    // **Métodos adicionales** (puedes agregar más lógica aquí según tus necesidades)
    // Puedes agregar métodos para reiniciar los valores de tiempo, puntajes, elementos, etc., si es necesario.

    // Por ejemplo, un método para resetear los valores cuando comienza una nueva partida:
    public void ResetearJuego()
    {
        tiempoJugado = 0;
        puntaje = 0;
        cantElementosRecolectados = 0;
        timer = 0f;
    }
}
