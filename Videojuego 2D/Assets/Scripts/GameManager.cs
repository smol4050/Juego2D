using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    private GameControllerScene1 gameController;

    private int appleScore = 0;
   

    public int AppleScore { get => appleScore; set => appleScore = value; }


    private void Awake() 
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameControllerScene1>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SumValues(int count)
    {
        appleScore += count;

        if (gameController != null)
        {
            gameController.ShowScore();
        }
    }
}
