using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_1_Steven : MonoBehaviour
{
    private GameController_Steven gameController;
    [SerializeField] private int itemValue;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController_Steven>();
        if (gameController == null)
        {
            Debug.LogError("GameController_Stevenn not found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameController.itemsRecolectados(1);

            if (gameController != null)
            {
                gameController.siguienteNivel();
                gameController.SumValues(itemValue);
            }
            Destroy(gameObject);
        }
    }
}
