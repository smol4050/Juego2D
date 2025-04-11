using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


#if UNITY_EDITOR
using UnityEditor; // Importante para detener la ejecución en el editor
#endif

public class GameController_Menu : MonoBehaviour
{
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
}
