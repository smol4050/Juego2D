using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemHielo_recolected : MonoBehaviour
{

    [SerializeField] private int cantidadPuntos;
    private GameController_Milo gameController;

    void Start()
    {
        // Find the GameControllerScene1 instance in the scene
        gameController = FindObjectOfType<GameController_Milo>();
        if (gameController == null)
        {
            Debug.LogError("GameController_Milo not found in the scene.");
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
