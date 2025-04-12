using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrullaje_Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;  // Los puntos de patrullaje
    [SerializeField] private float waitTime = 2f;    // Tiempo de espera en cada waypoint
    [SerializeField] private float speed = 2f;       // Velocidad de movimiento
    public Transform player;  // Referencia al jugador

    private int currentWaypoint = 0;  // Índice del waypoint actual
    private bool isWaiting = false;   // Para evitar que el enemigo se mueva mientras espera
    private float distanceThreshold = 0.1f;  // Umbral de distancia para considerar que se llegó al waypoint

    void Update()
    {
        MoveToWaypoint();
    }

    private void MoveToWaypoint()
    {
        // Si el enemigo no ha llegado al waypoint
        if (Vector2.Distance(transform.position, waypoints[currentWaypoint].position) > distanceThreshold)
        {
            // Mover hacia el waypoint actual
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypoint].position, speed * Time.deltaTime);

            // Hacer que el enemigo mire hacia la dirección en la que se mueve
            Flip();
        }
        else if (!isWaiting)
        {
            // Si ha llegado, empezar la espera
            StartCoroutine(WaitAtWaypoint());
        }
    }

    private IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);  // Espera antes de ir al siguiente waypoint

        // Avanzar al siguiente waypoint
        currentWaypoint++;

        if (currentWaypoint == waypoints.Length)  // Si llegó al final de los waypoints, vuelve al primero
        {
            currentWaypoint = 0;
        }

        isWaiting = false;
    }

    // Método para rotar el enemigo hacia el siguiente waypoint
    private void Flip()
    {
        // Obtener la dirección hacia el siguiente waypoint
        Vector2 direction = (waypoints[currentWaypoint].position - transform.position).normalized;

        // Rotar el enemigo para que mire hacia el siguiente waypoint
        if (direction.x > 0)  // Si el siguiente waypoint está a la derecha
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction.x < 0)  // Si está a la izquierda
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (collision.CompareTag("Player"))
        {
            playerController.TakeDamage(300);
        }
    }
}
