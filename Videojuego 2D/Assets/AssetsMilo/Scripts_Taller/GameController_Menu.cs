using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



#if UNITY_EDITOR
using UnityEditor; // Importante para detener la ejecución en el editor
#endif

public class GameController_Menu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TextPuntajesHistorial;

    private List<ClaseScore> puntajes;

    private void Start()
    {
        puntajes = new List<ClaseScore>();
        puntajes = ArchivoJSON.Instance.CargarPuntajes();

        MostrarPuntajes();
    }

    public void IniciarJuego(string nameScene)
    {
        TransitionController.Instance.CambiarEscena(nameScene);
    }

    public void MostrarCreditos(string nameScene)
    {
        Debug.Log("Mostrar Creditos");
        TransitionController.Instance.CambiarEscena(nameScene);
    }

    public void Exit()
    {
        Debug.Log("Exit");

        // Si estamos en el editor de Unity, detenemos la ejecución
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
           
            Application.Quit();
#endif
    }

    private void MostrarPuntajes()
    {
        string puntajesText = "";  // Variable para almacenar el texto de los puntajes

        // Recorrer la lista de puntajes y agregar cada puntaje al texto
        foreach (ClaseScore res in puntajes)
        {
            puntajesText += "Jugador: " + res.nombreJugador + "\n";
            puntajesText += "Tiempo: " + res.tiempo + "\n";
            puntajesText += "Puntaje: " + res.score + "\n";
            puntajesText += "Elementos: " + res.cantElementos + "\n";
            puntajesText += "-------------------------------" + "\n\n";

        }

        // Asignar el texto al componente TextMeshProUGUI
        TextPuntajesHistorial.text = puntajesText;
    }
}
