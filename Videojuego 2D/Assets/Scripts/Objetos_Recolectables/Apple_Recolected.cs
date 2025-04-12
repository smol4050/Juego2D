using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple_Recolected : MonoBehaviour
{
    [SerializeField] private int cantidadPuntos;

    private GameControllerScene1 gameController;

    void Start()
    {
        // Find the GameControllerScene1 instance in the scene
        gameController = FindObjectOfType<GameControllerScene1>();
        if (gameController == null)
        {
            Debug.LogError("GameControllerScene1 not found in the scene.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameController.itemsRecolectados(1);

            if (gameController != null)
            {
                gameController.SumValues(cantidadPuntos);
            }
            Destroy(gameObject);
        }
    }
}
