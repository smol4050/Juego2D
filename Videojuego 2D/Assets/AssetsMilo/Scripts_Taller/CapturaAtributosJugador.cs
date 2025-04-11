using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapturaAtributosJugador : MonoBehaviour
{

    // Referencia al Input Field donde el jugador escribirá su nombre
    public InputField inputNombreJugador;

    // Referencia a la clase GameManager donde tienes el puntaje, el tiempo y otros datos del juego
    public GameManager_Taller gameManager;

    // Método que se ejecuta cuando el jugador presiona el botón de "Finalizar juego"
    public void GuardarAtributosJugador()
    {
        // Obtener el nombre ingresado en el campo de texto
        string nombreJugador = inputNombreJugador.text;

        // Asegurarnos de que el nombre no esté vacío
        if (!string.IsNullOrEmpty(nombreJugador))
        {
            // Obtener los datos de la clase GameManager
            int tiempo = gameManager.GetTiempo();  // Método que retorna el tiempo jugado
            int score = gameManager.GetScore();  // Método que retorna el puntaje
            int cantElementos = gameManager.GetCantElementos();  // Método que retorna la cantidad de elementos recolectados

            // Crear el objeto ClaseScore con los datos
            ClaseScore nuevoScore = new ClaseScore(nombreJugador, tiempo, score, cantElementos);

            // Instanciar el ArchivoJSON solo cuando necesitemos guardar los datos
            ArchivoJSON archivoJSON = new ArchivoJSON();

            // Llamar al método de ArchivoJSON para guardar el puntaje
            archivoJSON.GuardarPuntaje(nuevoScore);

            // Mostrar un mensaje de confirmación en la consola (opcional)
            Debug.Log("Puntaje guardado para " + nombreJugador + " con un score de " + score);
        }
        else
        {
            // Mostrar un mensaje de error si el nombre está vacío
            Debug.LogWarning("Por favor, ingresa tu nombre.");
        }
    }
}
