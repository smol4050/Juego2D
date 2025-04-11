using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CapturaAtributosJugador : MonoBehaviour
{

    // Referencia al Input Field donde el jugador escribirá su nombre
    public TMP_InputField inputNombreJugador;

    // Referencia a la clase GameManager donde tienes el puntaje, el tiempo y otros datos del juego

    // Método que se ejecuta cuando el jugador presiona el botón de "Finalizar juego"
    public void GuardarAtributosJugador()
    {
        // Obtener el nombre ingresado en el campo de texto
        string nombreJugador = inputNombreJugador.text;

        // Asegurarnos de que el nombre no esté vacío
        if (!string.IsNullOrEmpty(nombreJugador))
        {
            Debug.Log("Nombre del jugador: " + nombreJugador);
            // Obtener los datos de la clase GameManager
            int tiempo = GameManager_Taller.Instance.GetTiempo();  // Método que retorna el tiempo jugado
            int score = GameManager_Taller.Instance.GetScore();  // Método que retorna el puntaje
            int cantElementos = GameManager_Taller.Instance.GetCantElementos();  // Método que retorna la cantidad de elementos recolectados

            // Crear el objeto ClaseScore con los datos
            ClaseScore ObjClaseScore = new ClaseScore(nombreJugador, tiempo, score, cantElementos);
            Debug.Log("Nuevo puntaje: " + ObjClaseScore.nombreJugador + ", Tiempo: " + ObjClaseScore.tiempo + ", Score: " + ObjClaseScore.score + ", Elementos: " + ObjClaseScore.cantElementos);

            // Llamar al método de ArchivoJSON para guardar el puntaje
            ArchivoJSON.Instance.GuardarPuntaje(ObjClaseScore);

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
