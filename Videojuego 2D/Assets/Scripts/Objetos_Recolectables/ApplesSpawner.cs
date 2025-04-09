using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject redApplePrefab;
    [SerializeField] private GameObject greenApplePrefab;

    [SerializeField] private int cantidadManzanas = 7;

    [SerializeField] private List<GameObject> spawnPoints;

    [SerializeField] private float probabilidadVerde = 0.7f; // 70% verde, 30% roja

    // Start is called before the first frame update
    void Start()
    {
        GenerarManzanas();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void GenerarManzanas()
    {
        if(spawnPoints.Count == 0)
        {
            Debug.LogWarning("No hay puntos de aparición asignados.");
            return;
        }

        int manzanasAGenerar = Mathf.Min(cantidadManzanas, spawnPoints.Count);

        List<GameObject> puntosDisponibles = new List<GameObject>(spawnPoints);

        for(int i = 0; i < manzanasAGenerar; i++)
        {
            if (puntosDisponibles.Count == 0) break;
            
            int indiceAleatorio = Random.Range(0, puntosDisponibles.Count);
            GameObject puntoDeSeleccionado = puntosDisponibles[indiceAleatorio];

            GameObject prefabSeleccionado = Random.value < probabilidadVerde ? greenApplePrefab : redApplePrefab;


            Instantiate(prefabSeleccionado, puntoDeSeleccionado.transform.position, puntoDeSeleccionado.transform.rotation);
            puntosDisponibles.RemoveAt(indiceAleatorio);
        }
    }

}
