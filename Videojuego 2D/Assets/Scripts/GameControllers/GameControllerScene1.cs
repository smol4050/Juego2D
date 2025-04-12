using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameControllerScene1 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtAppleScore;
    [SerializeField] public int appleScore = 0;

    public void Update()
    {
        txtAppleScore.text = GameManager_Taller.Instance.GetScore() + "";
    }
    public void ShowScore()
    {
        
        
    }

    public void SumValues(int count)
    {
        appleScore += count;
        GameManager_Taller.Instance.AgregarPuntaje(count);

    }
    public void itemsRecolectados (int item)
    {
        GameManager_Taller.Instance.AgregarElementosRecolectados(item);
    }

    public void CambiarEscena(string nameScene)
    {
        TransitionController.Instance.CambiarEscena(nameScene);
    }

    
}

