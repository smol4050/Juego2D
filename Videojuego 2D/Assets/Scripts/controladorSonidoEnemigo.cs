using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controladorSonidoEnemigo : MonoBehaviour
{
    [SerializeField] private AudioClip[] DamageReceived;
    [SerializeField] private AudioClip Died;
    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void selectAudioDied()
    {
       Debug.Log("Se intentó reproducir sonido de muerte del enemigo.");
        if (audioSource == null)
        {
         Debug.LogError("audioSource no está asignado en controladorSonidoEnemigo.");
        }
        audioSource.PlayOneShot(Died);
    }
    public void selectAudioDamageReceived()
    {
        int sonidoElegido = Random.Range(0, DamageReceived.Length);
        audioSource.PlayOneShot(DamageReceived[sonidoElegido]);
    }

    
}
