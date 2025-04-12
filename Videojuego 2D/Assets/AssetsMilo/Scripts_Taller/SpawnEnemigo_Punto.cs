using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnEnemigo_Punto : MonoBehaviour
{
    // Start is called before the first frame update
    
    private float minX, maxX, minY, maxY;
    public Transform player;

    [SerializeField] private Transform[] puntosSpawn;
    [SerializeField] private GameObject enemigoPrefab;
    [SerializeField] private float tiempoSpawn;
    [SerializeField] private float attackRange = 1.0f;

    private float tiempoSiguienteEnemigo;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("El objeto 'player' no está asignado en el Inspector.");
            return;
        } 

        maxX = puntosSpawn.Max(p => p.position.x);
        minX = puntosSpawn.Min(p => p.position.x);
        maxY = puntosSpawn.Max(p => p.position.y);
        minY = puntosSpawn.Min(p => p.position.y);

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < attackRange)
        {
            tiempoSiguienteEnemigo += Time.deltaTime;
            if (tiempoSiguienteEnemigo >= tiempoSpawn)
            {

                tiempoSiguienteEnemigo = 0;
                crearEnemigo();

            }
        }
    }

    private void crearEnemigo()
    {
        if (puntosSpawn.Length == 0)
        {
            Debug.LogError("No hay puntos de spawn asignados.");
            return;
        }
        Transform spawnPoint = puntosSpawn[Random.Range(0, puntosSpawn.Length)];
        //Vector2 posSpawn = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        GameObject enemigo = Instantiate(enemigoPrefab, spawnPoint.position, Quaternion.identity);
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;  // Color rojo para las esferas
        
            // Dibuja una esfera en la posición de cada punto de spawn
            Gizmos.DrawWireSphere(transform.position, attackRange);

            
    }
}
