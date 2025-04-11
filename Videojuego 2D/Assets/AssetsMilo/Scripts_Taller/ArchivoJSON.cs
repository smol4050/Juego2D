using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ArchivoJSON : MonoBehaviour
{
    // Ruta del archivo JSON en StreamingAssets
    private string jsonFilePath = Path.Combine(Application.streamingAssetsPath, "scores.json");

    // M�todo para guardar un puntaje (objeto ClaseScore) en el archivo JSON
    public void GuardarPuntaje(ClaseScore nuevoScore)
    {
        List<ClaseScore> scores = CargarPuntajes();  // Cargar puntajes existentes, si los hay
        scores.Add(nuevoScore);  // A�adir el nuevo puntaje a la lista

        // Convertir la lista de puntajes a formato JSON
        ScoreList scoreList = new ScoreList { scoreDataList = scores };
        string json = JsonUtility.ToJson(scoreList, true);

        // Escribir el JSON en el archivo
        File.WriteAllText(jsonFilePath, json);
        Debug.Log("Puntajes guardados en: " + jsonFilePath);
    }

    // M�todo para leer el archivo JSON y convertirlo en una lista de objetos ClaseScore
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
            Debug.LogWarning("No se encontr� el archivo de puntajes.");
            return new List<ClaseScore>();  // Retornar lista vac�a si el archivo no existe
        }
    }
}
