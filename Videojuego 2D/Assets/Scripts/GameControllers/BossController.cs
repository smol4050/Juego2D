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
    [SerializeField] public BossHealthBarUI healthBarUI;
    public float dashSpeed = 3f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 5f;
    public float attackCooldown = 2f;
    public float attackSpeed = 3f;
    public float predictionTime = 1f;
    private bool isRetreating = false;
    public float spellCooldown = 4f;
    private bool canCastSpell = true;
    private int currentHealth;
    private bool isDead = false;
    private bool isInvulnerable = false;
    private bool isDashing = false;
    private bool canAttack = true;
    private bool canDash = true;
    public GameObject skeletonWarrior;
    private GameObject attackHitbox;

    [SerializeField] public SonidoBoss sonidoBoss;

    private void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attackHitbox = transform.Find("AttackHitbox")?.gameObject;

        if (attackHitbox != null)
        {
            attackHitbox.SetActive(false);
        }

        if (healthBarUI != null)
        {
            healthBarUI.SetMaxHealth(maxHealth);
            healthBarUI.Hide();
        }

        if (sonidoBoss != null)
        {
            sonidoBoss.IniciarSonidosRandom(2f);
        }
        else
        {
            Debug.LogWarning("Componente SonidoBoss no asignado.");
        }

        
        GameObject[] skeletons = GameObject.FindGameObjectsWithTag("Skeleton");
        foreach (GameObject skeleton in skeletons)
        {
            Destroy(skeleton);
        }
    }




    private void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (currentHealth < maxHealth * 0.5f)
        {
            isRetreating = true;
        }

        if (isRetreating)
        {
            RetreatFromPlayer();
            return;
        }

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
            sonidoBoss.selectAudioAtack();
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
        if (isDead) return;
        rb.velocity = Vector2.zero;
        sonidoBoss.DetenerSonidosRandom();
        isDead = true;
        animator.SetTrigger("Die");

        StartCoroutine(HandleDeath());
    }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(3f);

        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }

        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    private void RetreatFromPlayer()
    {
        animator.SetBool("isWalking", true);
        Vector2 retreatDirection = (transform.position - player.position).normalized;
        rb.velocity = new Vector2(retreatDirection.x * moveSpeed, rb.velocity.y);
        Vector2 playerFuturePosition = (Vector2)player.position + player.GetComponent<Rigidbody2D>().velocity * predictionTime;

        if (canAttack)
        {
            AttackTowards(playerFuturePosition);
        }
    }

    private void AttackTowards(Vector2 targetPosition)
    {
        if (canAttack)
        {
            animator.SetBool("isWalking", false);
            animator.SetTrigger("isAttacking");
            canAttack = false;

            Vector2 attackDirection = (targetPosition - (Vector2)transform.position).normalized;
            GameObject attackInstance = Instantiate(spellPrefab, transform.position, Quaternion.identity);
            attackInstance.GetComponent<Rigidbody2D>().velocity = attackDirection * attackSpeed;

            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    private IEnumerator CastSpell()
    {
        if (!canCastSpell) yield break;

        canCastSpell = false;
        animator.SetTrigger("isCasting");

        
        float castAnimationDuration = 1.5f;
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
        if (healthBarUI != null)
        {
            healthBarUI.gameObject.SetActive(true);
            healthBarUI.Show();
        }
        else
        {
            Debug.LogWarning("healthBarUI no está asignado en el Inspector.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisión con: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Skeleton"))
        {
            Destroy(collision.gameObject);
        }
    }
}
