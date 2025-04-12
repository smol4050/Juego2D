using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_Finish : MonoBehaviour
{
    private GameController_Milo gameController;
    private Timer time;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController_Milo>();
        time = FindObjectOfType<Timer>();
        if (gameController == null)
        {
            Debug.LogError("GameController_Milo not found in the scene.");
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

            if (gameController != null)
            {
                gameController.FinishGame();
                time.TimerStop();

            }
            Destroy(gameObject);
        }
    }
}
