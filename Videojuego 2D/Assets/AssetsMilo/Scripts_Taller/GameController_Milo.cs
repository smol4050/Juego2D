using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController_Milo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtGeneralScore;
    [SerializeField] public int itemScore = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        txtGeneralScore.text = GameManager_Taller.Instance.GetScore() + "";
    }

    public void SumValues(int count)
    {
        itemScore += count;
        GameManager_Taller.Instance.AgregarPuntaje(count);

    }
    public void itemsRecolectados(int item)
    {
        GameManager_Taller.Instance.AgregarElementosRecolectados(item);
    }
}
