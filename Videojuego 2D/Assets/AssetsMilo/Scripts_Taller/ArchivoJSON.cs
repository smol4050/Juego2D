using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ArchivoJSON : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TextPuntajesHistorial;

    private List<ClaseScore> puntajes;  // Lista para almacenar los puntajes
    
    // Ruta del archivo JSON en StreamingAssets
    private string jsonFilePath = Path.Combine(Application.streamingAssetsPath, "scores.json");
    public static ArchivoJSON Instance;

    private void Awake()
    {
        // Verificar si no hay una instancia
        if (Instance == null)
        {
            Instance = this;  // Asignar esta instancia como la única
            DontDestroyOnLoad(gameObject);  // No destruir el objeto al cambiar de escena
        }
        else
        {
            Destroy(gameObject);  // Si ya existe una instancia, destruir este objeto
        }
    }

    private void Start()
    {
        puntajes = new List<ClaseScore>();
        puntajes = CargarPuntajes();

        MostrarPuntajes(); 
    }

    // Método para guardar un puntaje (objeto ClaseScore) en el archivo JSON
    public void GuardarPuntaje(ClaseScore nuevoScore)
    {
        Debug.Log("Guardando puntaje...");

        Debug.Log("Nombre Jugador: " + nuevoScore.nombreJugador);
        Debug.Log("Tiempo: " + nuevoScore.tiempo);
        Debug.Log("Puntaje: " + nuevoScore.score);
        Debug.Log("Cantidad Elementos: " + nuevoScore.cantElementos);

        List<ClaseScore> scores = CargarPuntajes();  // Cargar puntajes existentes, si los hay
        scores.Add(nuevoScore);  // Añadir el nuevo puntaje a la lista

        // Convertir la lista de puntajes a formato JSON
        ScoreList scoreList = new ScoreList { scoreDataList = scores };
        string json = JsonUtility.ToJson(scoreList, true);

        // Escribir el JSON en el archivo
        File.WriteAllText(jsonFilePath, json);
        Debug.Log("Puntajes guardados en: " + jsonFilePath);
    }

    // Método para leer el archivo JSON y convertirlo en una lista de objetos ClaseScore
    public List<ClaseScore> CargarPuntajes()
    {
        if (File.Exists(jsonFilePath))
        {
            // Leer el archivo JSON
            string json = File.ReadAllText(jsonFilePath);

            // Deserializar el JSON a un objeto ScoreList
            ScoreList scoreList = JsonUtility.FromJson<ScoreList>(json);
            return scoreList.scoreDataList;  // Retornar la lista de puntajes
        }
        else
        {
            Debug.LogWarning("No se encontró el archivo de puntajes.");
            return new List<ClaseScore>();  // Retornar lista vacía si el archivo no existe
        }
    }

    private void MostrarPuntajes()
    {
        string puntajesText = "";  // Variable para almacenar el texto de los puntajes

        // Recorrer la lista de puntajes y agregar cada puntaje al texto
        foreach (ClaseScore res in puntajes)
        {
            puntajesText += "Jugador:" + res.nombreJugador+"\n";
            puntajesText += "Tiempo:" + res.tiempo+ "\n";
            puntajesText += "Puntaje:" +res.score + "\n";
            puntajesText += "Elementos:" + res.cantElementos+"\n\n"; 
        }

        // Asignar el texto al componente TextMeshProUGUI
        TextPuntajesHistorial.text = puntajesText;
    }
}
