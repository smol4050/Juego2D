using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sonidoObjetosAdicionales : MonoBehaviour
{

    public static sonidoObjetosAdicionales instance;

    public AudioSource audioSource;

    public void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void EjecutarSonido(AudioClip sonido)
    {
        audioSource.PlayOneShot(sonido);

    }




}
