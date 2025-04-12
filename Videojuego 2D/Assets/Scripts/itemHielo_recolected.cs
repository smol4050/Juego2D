using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemHielo_recolected : MonoBehaviour
{

    [SerializeField] private float cantidadPuntos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.SumValues(cantidadPuntos);
            Destroy(gameObject);
        }

    }
}
