using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    public static TransitionController Instance;

    private Animator animator;
    [SerializeField] private AnimationClip animacionFinal;

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
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void CambiarEscena(string nombreEscena)
    {
        Debug.Log("Cambiando a la escena: " + nombreEscena);
        StartCoroutine(Transicion(nombreEscena));
    }

    private IEnumerator Transicion(string nombreEscena)
    {

        Debug.Log("Iniciando la transición a la escena: " + nombreEscena);

        animator.SetTrigger("TransicionActiva"); // activa la animación

        yield return new WaitForSeconds(0.01f);
        SceneManager.LoadScene(nombreEscena);

        
    }
}
