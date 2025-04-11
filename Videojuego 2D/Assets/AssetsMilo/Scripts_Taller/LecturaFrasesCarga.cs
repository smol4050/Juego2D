using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class LecturaFrasesCarga : MonoBehaviour
{
    System.Random random;
    private List<string> listaFrases;
    public TextMeshProUGUI txtFrase;

    // Start is called before the first frame update
    void Start()
    {

        listaFrases = new List<string>();
        random = new System.Random();
        listaFrases.Clear();
        LecturaFrases();
        mostrarFrase();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LecturaFrases()
    {
        string lineaLeida = "";
        try
        {
            // Usando una ruta más segura con StreamingAssets
            string filePath = Path.Combine(Application.streamingAssetsPath, "Frases_PantallaCarga_Taller.txt");
            StreamReader sr1 = new StreamReader(filePath);

            while ((lineaLeida = sr1.ReadLine()) != null)
            {
                listaFrases.Add(lineaLeida);
            }

            if (listaFrases.Count == 0)
            {
                Debug.LogWarning("El archivo de frases está vacío.");
            }
            else
            {
                Debug.Log("Tamaño de Lista de Frases: " + listaFrases.Count);
            }

        }
        catch (Exception e)
        {
            Debug.LogError("Error al leer el archivo: " + e.ToString());
        }
        //string lineaLeida = "";
        //try
        //{
        //    StreamReader sr1 = new StreamReader("Assets\\AssetsMilo\\Files\\Frases_PantallaCarga_Taller.txt");
        //    while ((lineaLeida = sr1.ReadLine()) != null)
        //    {

        //        listaFrases.Add(lineaLeida);
        //    }
        //    Debug.Log("Tamaño de Lista de Frases: " + listaFrases.Count());
        //}
        //catch (Exception e)
        //{
        //    Debug.Log("ERROR " + e.ToString());
        //}
        //// Debug.Log("Frases cargadas: " + listaFrases.Count);
    }

    public void mostrarFrase()
    {
        if (listaFrases.Count > 0)
        {
            Debug.Log("MostrandoFrase");
            int res = random.Next(0, listaFrases.Count);
            txtFrase.text = listaFrases[res];
        }
        else
        {
            Debug.LogWarning("La lista de frases está vacía.");
        }
    }
}
