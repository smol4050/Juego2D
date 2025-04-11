using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor; // Importante para detener la ejecuci�n en el editor
#endif

public class ExitProgram : MonoBehaviour
{
    public void Exit()
    {
        Debug.Log("Exit");

        // Si estamos en el editor de Unity, detenemos la ejecuci�n
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
           
            Application.Quit();
#endif
    }
}
