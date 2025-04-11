using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CapturaAtributosJugador : MonoBehaviour
{

    // Referencia al Input Field donde el jugador escribir� su nombre
    public TMP_InputField inputNombreJugador;

    // Referencia a la clase GameManager donde tienes el puntaje, el tiempo y otros datos del juego

    // M�todo que se ejecuta cuando el jugador presiona el bot�n de "Finalizar juego"
    public void GuardarAtributosJugador()
    {
        // Obtener el nombre ingresado en el campo de texto
        string nombreJugador = inputNombreJugador.text;

        // Asegurarnos de que el nombre no est� vac�o
        if (!string.IsNullOrEmpty(nombreJugador))
        {
            Debug.Log("Nombre del jugador: " + nombreJugador);
            // Obtener los datos de la clase GameManager
            int tiempo = GameManager_Taller.Instance.GetTiempo();  // M�todo que retorna el tiempo jugado
            int score = GameManager_Taller.Instance.GetScore();  // M�todo que retorna el puntaje
            int cantElementos = GameManager_Taller.Instance.GetCantElementos();  // M�todo que retorna la cantidad de elementos recolectados

            // Crear el objeto ClaseScore con los datos
            ClaseScore ObjClaseScore = new ClaseScore(nombreJugador, tiempo, score, cantElementos);
            Debug.Log("Nuevo puntaje: " + ObjClaseScore.nombreJugador + ", Tiempo: " + ObjClaseScore.tiempo + ", Score: " + ObjClaseScore.score + ", Elementos: " + ObjClaseScore.cantElementos);

            // Llamar al m�todo de ArchivoJSON para guardar el puntaje
            ArchivoJSON.Instance.GuardarPuntaje(ObjClaseScore);

            // Mostrar un mensaje de confirmaci�n en la consola (opcional)
            Debug.Log("Puntaje guardado para " + nombreJugador + " con un score de " + score);
        }
        else
        {
            // Mostrar un mensaje de error si el nombre est� vac�o
            Debug.LogWarning("Por favor, ingresa tu nombre.");
        }
    }
}
