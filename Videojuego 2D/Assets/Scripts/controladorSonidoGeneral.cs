using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controladorSonidoGeneral : MonoBehaviour
{

    [SerializeField] private AudioClip mainTheme;
    

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        soundMainTheme(0.01f);
    }


  

    public void soundMainTheme(float volume)
    {
        audioSource.clip = mainTheme;
        audioSource.Play();
    }


}
