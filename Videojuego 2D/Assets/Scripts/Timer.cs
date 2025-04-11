using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    #region sonidos
    [SerializeField]
    private AudioClip stop;
    [SerializeField]
    private AudioSource respuestaAudio;
    //Reloj objReloj;
    #endregion

    public TextMeshProUGUI timerMinutes;
    public TextMeshProUGUI timerSeconds;
    public TextMeshProUGUI timerSeconds100;

    private float startTime;
    private float stopTime;
    private float timerTime;
    private bool isRunning = false;

    // Use this for initialization
    void Start()
    {
        TimerReset();
    }

    public void TimerStart()
    {
        if (!isRunning)
        {
            print("START");
            isRunning = true;
            startTime = Time.time - timerTime;  // Asegurarse que el tiempo continue donde se dejó al cambiar de escena
        }
    }

    public void TimerStop()
    {
        if (isRunning)
        {
            print("STOP");
            isRunning = false;
            stopTime = timerTime;
            Debug.Log("Tiempo detenido" + stopTime.ToString());
            ///
            if (stopTime >= 30)
            {
                respuestaAudio.clip = stop;
                respuestaAudio.Play();
            }
            // Guardar el tiempo en el GameManager_Taller
            GameManager_Taller.Instance.SetTiempo((int)stopTime);

        }
    }

    public void TimerReset()
    {
        print("RESET");
        stopTime = 0;
        isRunning = false;
        timerMinutes.text = timerSeconds.text = timerSeconds100.text = "00";
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            timerTime = stopTime + (Time.time - startTime);
            int minutesInt = (int)timerTime / 60;
            int secondsInt = (int)timerTime % 60;
            int seconds100Int = (int)(Mathf.Floor((timerTime - (secondsInt + minutesInt * 60)) * 100));


            timerMinutes.text = (minutesInt < 10) ? "0" + minutesInt : minutesInt.ToString();
            timerSeconds.text = (secondsInt < 10) ? "0" + secondsInt : secondsInt.ToString();
            timerSeconds100.text = (seconds100Int < 10) ? "0" + seconds100Int : seconds100Int.ToString();
        }
    }
}
