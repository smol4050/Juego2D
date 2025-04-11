using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoBoss : MonoBehaviour
{
    [SerializeField] private AudioClip[] Voice;
    [SerializeField] private AudioClip[] AtackAudios;
    private AudioSource audioSource;
    private Coroutine vozCoroutine;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void IniciarSonidosRandom(float delay)
    {
        vozCoroutine = StartCoroutine(reproducirSonidosAleatorios(delay));

    }

    public void DetenerSonidosRandom() 
    {
        if (vozCoroutine != null)
        {
            StopCoroutine(vozCoroutine);
            vozCoroutine = null;
        }
    }

    public void selectAudio()
    {
        int sonidoElegido = Random.Range(0, Voice.Length);
        audioSource.PlayOneShot(Voice[sonidoElegido]);
    }

    public void selectAudioAtack()
    {
        int sonidoElegido = Random.Range(0, AtackAudios.Length);
        audioSource.PlayOneShot(AtackAudios[sonidoElegido]);

    }

    private IEnumerator reproducirSonidosAleatorios(float delay)
    {
        while (true)
        {
            int sonidoElegido = Random.Range(0, Voice.Length);
            AudioClip clip = Voice[sonidoElegido];
            audioSource.PlayOneShot(clip);
            delay = clip.length + Random.Range(0.5f, 2f);
            yield return new WaitForSeconds(delay);
        }
    }




}
