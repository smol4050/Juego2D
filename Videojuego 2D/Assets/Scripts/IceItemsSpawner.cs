using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceItemsSpawner : MonoBehaviour
{

    [SerializeField] private GameObject cristal;
    [SerializeField] private GameObject vallas;
    [SerializeField] private GameObject gotaHelada;
    [SerializeField] private GameObject personita;

    [SerializeField] private float probCristal = 0.4f;
    [SerializeField] private float probVallas = 0.3f;
    [SerializeField] private float probGotaHelada = 0.2f;
    [SerializeField] private float probPersonita = 0.1f;

    [SerializeField] private int cantidadDeObjetos = 7;

    [SerializeField] private List<GameObject> spawnPoints;


    private void Start()
    {
        GenerarItemsHielo();
    }



    private void GenerarItemsHielo()
    {
        if(spawnPoints.Count == 0)
        {
            Debug.LogWarning("No hay puntos de aparición asignados.");
            return;
        }

        int objetosAGenerar = Mathf.Min(cantidadDeObjetos, spawnPoints.Count);

        List<GameObject> puntosDisponibles = new List<GameObject>(spawnPoints);

        for(int i = 0; i < objetosAGenerar; i++)
        {
            if (puntosDisponibles.Count == 0) break;

            int indiceAleatorio = Random.Range(0, puntosDisponibles.Count);
            GameObject puntoDeSeleccionado = puntosDisponibles[indiceAleatorio];

            float numeroAleatorio = Random.value;
            GameObject prefabSeleccionado;
            if (numeroAleatorio < probCristal)
            {
                prefabSeleccionado = cristal;
            }
            else if (numeroAleatorio < probCristal + probVallas)
            {
                prefabSeleccionado = vallas;
            }
            else if (numeroAleatorio < probCristal + probVallas + probGotaHelada)
            {
                prefabSeleccionado = gotaHelada;
            }
            else
            {
                prefabSeleccionado = personita;
            }

            Instantiate(prefabSeleccionado, puntoDeSeleccionado.transform.position, puntoDeSeleccionado.transform.rotation);
            puntosDisponibles.RemoveAt(indiceAleatorio);


        }



    }







}
