using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionFinal_Cueva : MonoBehaviour
{
    // Start is called before the first frame update
    private GameControllerScene1 gameController;
    private void OnTriggerEnter(Collider other)
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
