using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public Transform player;
    public GameObject spellPrefab;
    public float moveSpeed = 1f;
    public float attackRange = 2f;
    public float detectionRange = 10f;
    public int maxHealth = 1000;
    public BossHealthBarUI healthBarUI;
    public float dashSpeed = 3f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 5f;
    public float attackCooldown = 2f;

    public float spellCooldown = 4f;
    private bool canCastSpell = true;
    private int currentHealth;
    private bool isDead = false;
    private bool isInvulnerable = false;
    private bool isDashing = false;
    private bool canAttack = true;
    private bool canDash = true;
    private GameObject attackHitbox;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attackHitbox = transform.Find("AttackHitbox").gameObject;
        attackHitbox.SetActive(false);

        currentHealth = maxHealth;
        healthBarUI.SetMaxHealth(maxHealth);
        healthBarUI.Hide();
    }

    private void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        switch (currentState)
        {
            case BossState.Idle:
                if (distanceToPlayer <= detectionRange)
                {
                    currentState = BossState.Chasing;
                }
                break;

            case BossState.Chasing:
                if (distanceToPlayer <= attackRange)
                {
                    currentState = BossState.Attacking;
                }
                else if (canCastSpell)
                {
                    currentState = BossState.Casting;
                }
                else if (canDash)
                {
                    currentState = BossState.Dashing;
                }
                else
                {
                    ChasePlayer();
                }
                break;

            case BossState.Attacking:
                if (canAttack)
                {
                    Attack();
                }
                currentState = BossState.Chasing;
                break;

            case BossState.Dashing:
                StartCoroutine(Dash((player.position - transform.position).normalized));
                currentState = BossState.Chasing;
                break;

            case BossState.Casting:
                StartCoroutine(CastSpell());
                currentState = BossState.Chasing;
                break;

            case BossState.Retreating:
                RetreatFromPlayer();
                if (distanceToPlayer >= detectionRange)
                {
                    currentState = BossState.Idle;
                }
                break;
        }
    }




    private enum BossState
    {
        Idle,
        Chasing,
        Attacking,
        Dashing,
        Casting,
        Retreating
    }

    private BossState currentState = BossState.Idle;


    private void ChasePlayer()
    {
        animator.SetBool("isWalking", true);
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
    }

    private void Idle()
    {
        animator.SetBool("isWalking", false);
        rb.velocity = Vector2.zero;
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

    private IEnumerator Dash(Vector2 direction)
    {
        if (!canDash || isDashing)
            yield break;

        isDashing = true;
        canDash = false;
        SetInvulnerable(true);
        animator.SetTrigger("isDashing");

        float dashTime = 0.5f;
        float dashDistance = 6f;
        float elapsedTime = 0f;
        Vector2 startPosition = rb.position;
        Vector2 targetPosition = startPosition + direction.normalized * dashDistance;

        while (elapsedTime < dashTime)
        {
            rb.MovePosition(Vector2.Lerp(startPosition, targetPosition, elapsedTime / dashTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.MovePosition(targetPosition);
        SetInvulnerable(false);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable || isDead)
            return;

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

    private void RetreatFromPlayer()
    {
        animator.SetBool("isWalking", true);
        Vector2 direction = (transform.position - player.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
    }

    private IEnumerator CastSpell()
    {
        if (!canCastSpell) yield break;

        canCastSpell = false;
        animator.SetTrigger("isCasting");

        
        float castAnimationDuration = 1f;
        yield return new WaitForSeconds(castAnimationDuration);

        
        Vector3 spellPosition = new Vector3(player.position.x, -1.25f, player.position.z);
        GameObject spellInstance = Instantiate(spellPrefab, spellPosition, Quaternion.identity);

        
        Animator spellAnimator = spellInstance.GetComponent<Animator>();
        if (spellAnimator != null)
        {
            yield return new WaitForSeconds(spellAnimator.GetCurrentAnimatorStateInfo(0).length);
        }

        Destroy(spellInstance);

        yield return new WaitForSeconds(spellCooldown);
        canCastSpell = true;
    }




    public void SetInvulnerable(bool state)
    {
        isInvulnerable = state;
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
}
