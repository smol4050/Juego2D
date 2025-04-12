using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoSonidoManzanas : MonoBehaviour
{

    [SerializeField] private AudioClip sonidoManzana;

    private void OnTriggerEnter2D(Collider2D collision)
    {   
     sonidoObjetosAdicionales.instance.EjecutarSonido(sonidoManzana);
     Destroy(gameObject);

    }
}
