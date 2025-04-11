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
            StreamReader sr1 = new StreamReader("Assets\\AssetsMilo\\Files\\Frases_PantallaCarga_Taller.txt");
            while ((lineaLeida = sr1.ReadLine()) != null)
            {
                
                listaFrases.Add(lineaLeida);
            }
            Debug.Log("Tamaño de Lista de Frases: " + listaFrases.Count());
        }
        catch (Exception e)
        {
            Debug.Log("ERROR " + e.ToString());
        }
        // Debug.Log("Frases cargadas: " + listaFrases.Count);
    }

    public void mostrarFrase()
    {
        int res;

        res = random.Next(0, listaFrases.Count);

        txtFrase.text = listaFrases[res];
    }
}
