using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple_Recolected : MonoBehaviour
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
