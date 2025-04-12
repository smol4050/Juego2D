using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoSonidoBoost : MonoBehaviour
{
    [SerializeField] private AudioClip sonidoBoost;
    [SerializeField] private AudioClip sonidoLife;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        sonidoObjetosAdicionales.instance.EjecutarSonido(sonidoBoost);
        Destroy(gameObject);

    }
}
