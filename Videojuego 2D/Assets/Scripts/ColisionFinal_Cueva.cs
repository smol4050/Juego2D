using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionFinal_Cueva : MonoBehaviour
{
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
    // Start is called before the first frame update
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colision");

        if (other.CompareTag("Player"))
        {
            Debug.Log("Colision con el jugador");
            gameController.CambiarEscena("Stiven");
            Destroy(other.gameObject);
        }
    }
}
