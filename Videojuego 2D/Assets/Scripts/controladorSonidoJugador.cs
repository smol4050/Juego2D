using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controladorSonidoJugador : MonoBehaviour
{

    [SerializeField] private AudioClip[] AtackAudios;
    [SerializeField] private AudioClip JumpAudio;
    [SerializeField] private AudioClip RunAudio;

    public AudioSource audioSource;
    public AudioSource pasos;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void selectAudioAtack(float volume)
    {
        int sonidoElegido = Random.Range(0, AtackAudios.Length);
        audioSource.PlayOneShot(AtackAudios[sonidoElegido], volume);

    }

    public void soundJump(float volume)
    {
        audioSource.PlayOneShot(JumpAudio, volume);
    }

    public void soundRun(float volume)
    {
        pasos.Play();
    }
}





