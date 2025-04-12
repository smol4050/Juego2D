using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoSonidoVida : MonoBehaviour
{
    [SerializeField] private AudioClip sonidoLife;
     private void OnTriggerEnter2D(Collider2D collision)
    {

        sonidoObjetosAdicionales.instance.EjecutarSonido(sonidoLife);
        Destroy(gameObject);

    }
}
