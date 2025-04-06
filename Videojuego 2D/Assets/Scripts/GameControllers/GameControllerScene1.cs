using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameControllerScene1 : MonoBehaviour
{
    [SerializeField] private TextMeshPro txtAppleScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetAppleValue(string appleType)
    {
        switch (appleType)
        {
            case "RedApple":
                return 3;
            case "GreenApple":
                return 1;
            default:
                return 0;
        }
    }


    public void ShowScore()
    {
        txtAppleScore.text = GameManager.Instance.AppleScore.ToString();
    }
}
