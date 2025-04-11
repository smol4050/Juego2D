using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controladorSonidoJugador : MonoBehaviour
{

    [SerializeField] private AudioClip[] AtackAudios;
    [SerializeField] private AudioClip[] DamageReceived;
    [SerializeField] private AudioClip JumpAudio;
    [SerializeField] private AudioClip RunAudio;

    public AudioSource audioSource;
    public AudioSource pasos;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void selectAudioAtack()
    {
        int sonidoElegido = Random.Range(0, AtackAudios.Length);
        audioSource.PlayOneShot(AtackAudios[sonidoElegido]);

    }

    public void selectAudioDamageReceived()
    {
        int sonidoElegido = Random.Range(0, DamageReceived.Length);
        audioSource.PlayOneShot(DamageReceived[sonidoElegido]);
    }

    public void soundJump()
    {
        audioSource.PlayOneShot(JumpAudio);
    }

    public void soundRun()
    {
        pasos.Play();
    }
}





