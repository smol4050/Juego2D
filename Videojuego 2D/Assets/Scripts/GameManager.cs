using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SumValues(int count)
    {
        appleScore += count;

        GameControllerScene1 gameController = FindObjectOfType<GameControllerScene1>();
        if (gameController != null)
        {
            gameController.ShowScore();
        }
    }



}
