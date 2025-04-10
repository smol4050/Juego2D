using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float speed = 5.0f;
    public float attackRange = 1.0f;

    float xinicial, yinicial;
    //private Rigidbody2D rb;
    //private Vector2 movement;

    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();

        xinicial = transform.position.x;
        yinicial = transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            //Vector2 direction = (player.position - transform.position).normalized;

            //movement = new Vector2(direction.x, direction.y);
        }
        else
        {
            // If the player is outside the attack range, move back to the initial position
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(xinicial, yinicial), speed * Time.deltaTime);
            //movement = Vector2.zero;
        }

        //rb.MovePosition(rb.position + speed * Time.deltaTime * movement);


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
