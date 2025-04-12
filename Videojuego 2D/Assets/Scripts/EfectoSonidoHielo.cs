using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoSonidoHielo : MonoBehaviour
{
    [SerializeField] private AudioClip sonidoHielo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sonidoObjetosAdicionales.instance.EjecutarSonido(sonidoHielo);
        }

    }
}
