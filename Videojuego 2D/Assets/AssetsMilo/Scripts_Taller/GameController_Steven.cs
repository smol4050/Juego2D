using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController_Steven : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtGeneralScore;
    [SerializeField] public int itemScore = 0;


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

    public void siguienteNivel()
    {
        TransitionController.Instance.CambiarEscena("Milo");
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToSceneMenu()
    {
        SceneManager.LoadScene("Menu_IU");
    }
}
