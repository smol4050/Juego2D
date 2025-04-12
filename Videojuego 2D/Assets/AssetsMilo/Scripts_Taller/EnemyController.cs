using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EnemyController : Enemy
{
    [SerializeField] controladorSonidoEnemigo controladorSE;
    public Transform player;
    public float speed = 5.0f;
    public float attackRange = 1.0f;
    public int attackDamage = 10; // Daño del ataque
    [SerializeField] private float attackCooldown = 1.0f; // Tiempo de recarga del ataque
    [SerializeField] private float minDistance; 
    private Animator Animator;
    private float lastAttackTime = -Mathf.Infinity;

    //public int maxHealth = 100;
    //public int currentHealth;

    float xinicial, yinicial;
    //private Rigidbody2D rb;
    //private Vector2 movement;

    protected override void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        base.Start();
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

        if (distanceToPlayer < minDistance && Time.time >= lastAttackTime + attackCooldown)
        {
            // Ataque solo si ya pasó el cooldown
            Animator.SetTrigger("Attack");
            
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(attackDamage);
            }

            Debug.Log("Attacking the player!");
            lastAttackTime = Time.time; // Actualiza el tiempo del último ataque
        }
    }
    else
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(xinicial, yinicial), speed * Time.deltaTime);
    }

        //rb.MovePosition(rb.position + speed * Time.deltaTime * movement);

    }

    public override void TakeDamage(int damage)
    {
        controladorSE.selectAudioDamageReceived();
        Animator.SetTrigger("hit"); // Llama al trigger de daño en el Animator
        base.TakeDamage(damage); // Usa la lógica base (restar salud y morir si <= 0)
         // Llama al sonido de daño recibido
    }

    protected override void Die()
    {
        controladorSE.selectAudioDied(); 
        Debug.Log("Enemy died!");
        gameObject.SetActive(false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.DrawWireSphere(transform.position, minDistance);
    }
}
