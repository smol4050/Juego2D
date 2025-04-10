using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private bool isInvulnerable = false;
    private Animator animator;
    private Rigidbody2D rb;
    public Transform player;
    public float moveSpeed = 3f;
    public float attackRange = 2f;
    public float detectionRange = 10f;
    private bool isDead = false;
    public int maxHealth = 1000;
    public int currentHealth;
    public BossHealthBarUI healthBarUI;
    private bool isDashing = false;
    public float dashSpeed = 20f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 1f;
    private GameObject attackHitbox;
    public float attackCooldown = 2f;
    private bool canAttack = true;



    private void Start()
    {
        currentHealth = maxHealth;
        healthBarUI.SetMaxHealth(maxHealth);
        healthBarUI.Hide();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attackHitbox = transform.Find("AttackHitbox").gameObject;
        attackHitbox.SetActive(false);

    }


    private void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            Attack();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            if (!isDashing)
            {
                StartCoroutine(Dash((player.position - transform.position).normalized));
            }
            else
            {
                ChasePlayer();
            }
        }
        else
        {
            Idle();
        }
    }

    public void EnableHitbox()
    {
        attackHitbox.SetActive(true);
    }

    public void DisableHitbox()
    {
        attackHitbox.SetActive(false);
    }

    public void Appear()
    {
        
        healthBarUI.Show();
    }

    private void ChasePlayer()
    {
        animator.SetBool("isWalking", true);
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
    }

    private void Attack()
    {
        if (canAttack)
        {
            animator.SetBool("isWalking", false);
            animator.SetTrigger("isAttacking");
            canAttack = false;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    private void ResetAttack()
    {
        canAttack = true;
    }

    public void OnAttackAnimationEnd()
    {
        animator.SetBool("isAttacking", false);
    }

    private void Idle()
    {
        animator.SetBool("isWalking", false);
        rb.velocity = Vector2.zero;
    }

    private IEnumerator Dash(Vector2 direction)
    {
        if (isDashing)
            yield break;

        isDashing = true;
        SetInvulnerable(true);
        animator.SetTrigger("DashTrigger");
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = direction.normalized * dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        rb.gravityScale = originalGravity;
        rb.velocity = Vector2.zero;
        SetInvulnerable(false);
        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
    }





    public void TakeDamage(int damage)
    {
        if (isInvulnerable){
            return;
        }

        currentHealth -= damage;
        animator.SetTrigger("isHurt");
        healthBarUI.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        rb.velocity = Vector2.zero;

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        healthBarUI.Hide();

    }

    public void SetInvulnerable(bool state)
    {
        isInvulnerable = state;
    }
}

