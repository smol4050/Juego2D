using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameControllerScene1 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtAppleScore;

    public void Update()
    {
        txtAppleScore.text = GameManager.Instance.appleScore.ToString();
    }
    public void ShowScore()
    {
        
        
    }
}
