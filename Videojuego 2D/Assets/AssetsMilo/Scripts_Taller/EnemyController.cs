using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float speed = 5.0f;
    public float attackRange = 1.0f;
    [SerializeField] private float attackCooldown = 1.0f; // Tiempo de recarga del ataque
    [SerializeField] private float minDistance; 
    private Animator Animator;

    //public int maxHealth = 100;
    //public int currentHealth;

    float xinicial, yinicial;
    //private Rigidbody2D rb;
    //private Vector2 movement;

    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();

        xinicial = transform.position.x;
        yinicial = transform.position.y;

        //currentHealth = maxHealth;
        Animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            if(distanceToPlayer < minDistance)
            {
                // Attack the player
                Animator.SetTrigger("Attack");
                // Implement attack logic here
                Debug.Log("Attacking the player!");
            }

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

    //public void TakeDamage(int damage)
    //{
    //    currentHealth -= damage;
    //    Debug.Log("Enemy took damage: " + damage);
    //    if (currentHealth <= 0)
    //    {
    //        Die();
    //    }
    //}

    //void Die()
    //{
    //    Debug.Log("Enemy died!");
    //    gameObject.SetActive(false); // Desactiva el objeto enemigo
    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
